// in WalletWise/ViewModels/BudgetViewModel.cs
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using WalletWise.Services;
using WalletWise.Views;

namespace WalletWise.ViewModels;

public partial class BudgetViewModel(IBudgetService budgetService) : ObservableObject
{
    public ObservableCollection<BudgetStatus> BudgetItems { get; } = [];

    [ObservableProperty]
    private string _currentMonthDisplay = string.Empty;

    [RelayCommand]
    private async Task LoadBudgetsAsync()
    {
        var now = DateTime.Now;
        var monthName = now.ToString("MMMM yyyy");
        // Assicura che la prima lettera sia maiuscola
        CurrentMonthDisplay = char.ToUpper(monthName[0]) + monthName[1..];

        BudgetItems.Clear();
        var statusList = await budgetService.GetBudgetStatusForMonthAsync(now.Year, now.Month);
        foreach (var item in statusList)
        {
            BudgetItems.Add(item);
        }
    }
    [RelayCommand]
    private async Task GoToAddBudgetAsync()
    {
        await Shell.Current.GoToAsync(nameof(AddBudgetPage));
    }

}
