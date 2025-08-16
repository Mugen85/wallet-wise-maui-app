// in WalletWise/ViewModels/AddBudgetViewModel.cs
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WalletWise.Services;
using WalletWise.Persistence.Models; // Aggiungi questo

namespace WalletWise.ViewModels;

public partial class AddBudgetViewModel(IBudgetService budgetService) : ObservableObject
{
    [ObservableProperty] private string _category = string.Empty;
    [ObservableProperty] private decimal _amount;

    [RelayCommand]
    private async Task SaveBudgetAsync()
    {
        if (string.IsNullOrWhiteSpace(Category) || Amount <= 0) return;

        var now = DateTime.Now;
        await budgetService.SaveBudgetAsync(new Budget
        {
            Category = Category,
            Amount = Amount,
            Month = now.Month,
            Year = now.Year
        });

        await Shell.Current.GoToAsync("..");
    }
}
