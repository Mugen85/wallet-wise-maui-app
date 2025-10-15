// in WalletWise/ViewModels/AddBudgetViewModel.cs
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
    private decimal? _amount;

    [ObservableProperty]
    private string? _selectedCategory;

    public ObservableCollection<string> Categories { get; } = new(CategoryData.GetExpenseCategories());

    public string IncomingCategory { set => PreselectCategory(value); }

    private void PreselectCategory(string categoryName)
    {
        if (!string.IsNullOrEmpty(categoryName))
        {
            SelectedCategory = Categories.FirstOrDefault(c => c.Equals(categoryName, StringComparison.OrdinalIgnoreCase));
        }
    }

    [RelayCommand]
    private async Task SaveBudgetAsync()
    {
        // --- MODIFICA CHIAVE ---
        // Aggiungiamo il controllo per Amount.HasValue
        if (string.IsNullOrWhiteSpace(SelectedCategory) || !Amount.HasValue || Amount.Value <= 0) return;

        var now = DateTime.Now;
        await budgetService.SaveBudgetAsync(new Budget
        {
            Category = SelectedCategory,
            Amount = Amount.Value, // Usiamo .Value per ottenere il decimal non nullo
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
