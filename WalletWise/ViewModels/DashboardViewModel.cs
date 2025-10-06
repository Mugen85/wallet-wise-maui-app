using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using WalletWise.Services;

namespace WalletWise.ViewModels;

public partial class DashboardViewModel(IAccountService accountService, IBudgetService budgetService) : ObservableObject
{
    [ObservableProperty]
    private decimal _totalBalance;

    // Nuova collection per i budget
    public ObservableCollection<BudgetStatus> BudgetSummary { get; } = [];

    [RelayCommand]
    private async Task LoadDataAsync()
    {
        // Carica il bilancio totale (logica esistente)
        var accounts = await accountService.GetAccountsAsync();
        TotalBalance = accounts.Sum(a => a.CurrentBalance);

        // Carica la situazione dei budget
        BudgetSummary.Clear();
        var now = DateTime.Now;
        var budgetStatusList = await budgetService.GetBudgetStatusForMonthAsync(now.Year, now.Month);
        foreach (var status in budgetStatusList)
        {
            BudgetSummary.Add(status);
        }
    }
}