// in WalletWise/ViewModels/AddBudgetViewModel.cs
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using WalletWise.Persistence.Models;
using WalletWise.Services;
using System.Globalization;

namespace WalletWise.ViewModels;

[QueryProperty(nameof(IncomingCategory), "Category")]
[QueryProperty(nameof(IncomingAmount), "Amount")]
public partial class AddBudgetViewModel(IBudgetService budgetService) : ObservableObject
{
    [ObservableProperty]
    private decimal? _amount;

    [ObservableProperty]
    private string? _selectedCategory;

    public ObservableCollection<string> Categories { get; } = new(CategoryData.GetExpenseCategories());

    public string IncomingCategory { set => PreselectCategory(value); }

    public string IncomingAmount
    {
        set => ParseAmount(value);
    }

    private void ParseAmount(string amountString)
    {
        // Usiamo il nostro C# per convertire la stringa in modo sicuro,
        // specificando che il formato è universale (InvariantCulture).
        if (decimal.TryParse(amountString, NumberStyles.Any, CultureInfo.InvariantCulture, out var amountValue))
        {
            Amount = amountValue;
        }
    }

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
