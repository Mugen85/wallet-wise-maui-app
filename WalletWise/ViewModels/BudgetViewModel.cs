using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Globalization;
using WalletWise.Services;
using WalletWise.Views;

namespace WalletWise.ViewModels;

public partial class BudgetViewModel(IBudgetService budgetService, IAlertService alertService) : ObservableObject
{
    public ObservableCollection<BudgetStatus> BudgetItems { get; } = [];

    [ObservableProperty]
    private string _currentMonthDisplay = DateTime.Now.ToString("MMMM yyyy", new CultureInfo("it-IT"));

    [RelayCommand]
    private async Task LoadBudgetsAsync()
    {
        BudgetItems.Clear();
        var now = DateTime.Now;
        var budgets = await budgetService.GetBudgetStatusForMonthAsync(now.Year, now.Month);
        foreach (var budget in budgets)
        {
            BudgetItems.Add(budget);
        }
    }

    [RelayCommand]
    private static async Task GoToAddBudgetAsync()
    {
        await Shell.Current.GoToAsync("AddBudgetPage");
    }

    // --- INIZIO CODICE MANCANTE ---
    [RelayCommand]
    private async Task GoToEditBudgetAsync(BudgetStatus budgetStatus)
    {
        if (budgetStatus == null) return;

        // Navighiamo alla pagina di modifica passando i dati come parametri
        await Shell.Current.GoToAsync($"{nameof(AddBudgetPage)}?Category={budgetStatus.Category}&Amount={budgetStatus.BudgetedAmount}");
    }
    // --- FINE CODICE MANCANTE ---

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

