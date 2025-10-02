using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using WalletWise.Persistence.Models;
using WalletWise.Services;

namespace WalletWise.ViewModels;

[QueryProperty(nameof(IncomingCategory), "Category")]
[QueryProperty(nameof(Amount), "Amount")]
public partial class AddBudgetViewModel(IBudgetService budgetService) : ObservableObject
{
    [ObservableProperty]
    private decimal _amount;

    [ObservableProperty]
    private string? _selectedCategory;

    public ObservableCollection<string> Categories { get; } = new(CategoryData.GetDefaultCategories());

    // Proprietà "temporanea" per ricevere il dato dalla navigazione in modifica
    public string IncomingCategory { set => PreselectCategory(value); }

    private void PreselectCategory(string categoryName)
    {
        if (!string.IsNullOrEmpty(categoryName))
        {
            _selectedCategory = Categories.FirstOrDefault(c => c.Equals(categoryName, StringComparison.OrdinalIgnoreCase));
        }
    }

    [RelayCommand]
    private async Task SaveBudgetAsync()
    {
        if (string.IsNullOrWhiteSpace(_selectedCategory) || Amount <= 0) return;

        var now = DateTime.Now;
        await budgetService.SaveBudgetAsync(new Budget
        {
            Category = _selectedCategory,
            Amount = Amount,
            Month = now.Month,
            Year = now.Year
        });

        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    private static async Task CancelAsync()
    {
        await Shell.Current.GoToAsync("..");
    }
}