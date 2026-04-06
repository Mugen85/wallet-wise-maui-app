// in WalletWise/ViewModels/DashboardViewModel.cs
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
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
        try
        {
            FileLogger.Log("DashboardViewModel: LoadData avviato");

            var accounts = await accountService.GetAccountsAsync();
            FileLogger.Log($"DashboardViewModel: {accounts.Count} conti caricati");
            TotalBalance = accounts.Sum(a => a.CurrentBalance);

            var now = DateTime.Now;
            var budgetStatusList = await budgetService.GetBudgetStatusForMonthAsync(now.Year, now.Month);
            FileLogger.Log($"DashboardViewModel: {budgetStatusList.Count} budget caricati");

            for (int i = 0; i < budgetStatusList.Count; i++)
            {
                budgetStatusList[i].CategoryColor = ColorData.GetColorByIndex(i);
            }
            FileLogger.Log("DashboardViewModel: colori assegnati");

            BudgetSummary = new ObservableCollection<BudgetStatus>(budgetStatusList);
            FileLogger.Log("DashboardViewModel: LoadData completato");
        }
        catch (Exception ex)
        {
            FileLogger.Log($"DashboardViewModel ERRORE: {ex}");
        }
    }
}
