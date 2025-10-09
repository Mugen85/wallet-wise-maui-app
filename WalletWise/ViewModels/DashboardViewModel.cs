// in WalletWise/ViewModels/DashboardViewModel.cs
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Diagnostics; // Aggiungi questo namespace
using WalletWise.Services;

namespace WalletWise.ViewModels;

public partial class DashboardViewModel(IAccountService accountService, IBudgetService budgetService) : ObservableObject
{
    [ObservableProperty]
    private decimal _totalBalance;

    [ObservableProperty]
    private ObservableCollection<BudgetStatus> _budgetSummary = [];

    [RelayCommand]
    private async Task LoadDataAsync()
    {
        var accounts = await accountService.GetAccountsAsync();
        TotalBalance = accounts.Sum(a => a.CurrentBalance);

        var now = DateTime.Now;
        var budgetStatusList = await budgetService.GetBudgetStatusForMonthAsync(now.Year, now.Month);

        // --- INIZIO MODIFICA CHIAVE ---
        // Assegniamo un colore a ogni budget prima di passarlo alla UI.
        for (int i = 0; i < budgetStatusList.Count; i++)
        {
            budgetStatusList[i].CategoryColor = ColorData.GetColorByIndex(i);
        }
        // --- FINE MODIFICA CHIAVE ---

        BudgetSummary = new ObservableCollection<BudgetStatus>(budgetStatusList);
    }
}
