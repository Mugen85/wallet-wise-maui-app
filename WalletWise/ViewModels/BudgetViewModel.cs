// in WalletWise/ViewModels/BudgetViewModel.cs
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Globalization;
using WalletWise.Services;
using WalletWise.Views;

namespace WalletWise.ViewModels;

public partial class BudgetViewModel(IBudgetService budgetService, IAlertService alertService) : ObservableObject
{
    // L'attributo [ObservableProperty] genera una proprietà pubblica "BudgetItems"
    // a partire da questo campo privato "_budgetItems".
    [ObservableProperty]
    private ObservableCollection<BudgetStatus> _budgetItems = [];

    [ObservableProperty]
    private string _currentMonthDisplay = DateTime.Now.ToString("MMMM yyyy", new CultureInfo("it-IT"));

    [RelayCommand]
    private async Task LoadBudgetsAsync()
    {
        var now = DateTime.Now;
        var budgets = await budgetService.GetBudgetStatusForMonthAsync(now.Year, now.Month);

        for (int i = 0; i < budgets.Count; i++)
        {
            budgets[i].CategoryColor = ColorData.GetColorByIndex(i);
        }

        // Questa è la sintassi CORRETTA. Assegniamo alla proprietà pubblica
        // generata, scatenando la notifica per aggiornare la UI.
        BudgetItems = new ObservableCollection<BudgetStatus>(budgets);
    }

    [RelayCommand]
    private async Task GoToAddBudgetAsync()
    {
        await Shell.Current.GoToAsync(nameof(AddBudgetPage));
    }

    [RelayCommand]
    private async Task GoToEditBudgetAsync(BudgetStatus budgetStatus)
    {
        if (budgetStatus == null) return;
        await Shell.Current.GoToAsync($"{nameof(AddBudgetPage)}?Category={budgetStatus.Category}&Amount={budgetStatus.BudgetedAmount}");
    }

    [RelayCommand]
    private async Task DeleteBudgetAsync(BudgetStatus budgetStatus)
    {
        if (budgetStatus == null) return;

        bool userConfirmed = await alertService.ShowConfirmationAsync("ATTENZIONE",
            $"Stai per eliminare il budget '{budgetStatus.Category}'.\n\nQuesta azione cancellerà anche TUTTE LE TRANSAZIONI associate a questa categoria per il mese corrente. L'operazione è irreversibile.\n\nSei sicuro di voler procedere?");

        if (userConfirmed)
        {
            var now = DateTime.Now;
            await budgetService.DeleteBudgetAsync(budgetStatus.Category, now.Year, now.Month);
            await LoadBudgetsAsync(); // Ricarica la lista
        }
    }
}