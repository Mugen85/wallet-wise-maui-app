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
    [ObservableProperty]
    private ObservableCollection<BudgetStatus> _budgetItems = [];

    [ObservableProperty]
    private string _currentMonthDisplay = DateTime.Now.ToString("MMMM yyyy", new CultureInfo("it-IT"));

    // --- INIZIO MODIFICA (PEZZO 5) ---
    // 2. Aggiungiamo il "sensore"
    // Questo dirà alla UI se deve mostrare la lista o il prompt di clonazione.
    [ObservableProperty]
    private bool _hasBudgets;
    // --- FINE MODIFICA ---

    [RelayCommand]
    private async Task LoadBudgetsAsync()
    {
        var now = DateTime.Now;
        var budgets = await budgetService.GetBudgetStatusForMonthAsync(now.Year, now.Month);

        for (int i = 0; i < budgets.Count; i++)
        {
            budgets[i].CategoryColor = ColorData.GetColorByIndex(i);
        }

        BudgetItems = new ObservableCollection<BudgetStatus>(budgets);

        // --- INIZIO MODIFICA (PEZZO 5) ---
        // 3. Aggiorniamo il "sensore" ogni volta che carichiamo i dati.
        HasBudgets = BudgetItems.Any();
        // --- FINE MODIFICA ---
    }

    // --- INIZIO MODIFICA (PEZZO 5) ---
    // 4. Creiamo il comando per il nostro nuovo "motore" di clonazione.
    [RelayCommand]
    private async Task CloneLastMonthBudgetsAsync()
    {
        var now = DateTime.Now;
        // Chiamiamo il motore che abbiamo costruito
        await budgetService.CloneLastMonthBudgetsAsync(now.Year, now.Month);

        // Dopo aver clonato, ricarichiamo la lista per aggiornare la UI.
        // Questo è un costrutto solido e a prova di bug.
        await LoadBudgetsAsync();
    }
    // --- FINE MODIFICA ---

    [RelayCommand]
    private async Task GoToAddBudgetAsync()
    {
        await Shell.Current.GoToAsync(nameof(AddBudgetPage));
    }

    [RelayCommand]
    private async Task GoToEditBudgetAsync(BudgetStatus budgetStatus)
    {
        if (budgetStatus == null) return;
        var amountAsString = budgetStatus.BudgetedAmount.ToString(CultureInfo.InvariantCulture);
        await Shell.Current.GoToAsync($"{nameof(AddBudgetPage)}?Category={Uri.EscapeDataString(budgetStatus.Category)}&Amount={amountAsString}");
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