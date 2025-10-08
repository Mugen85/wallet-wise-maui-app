// in WalletWise/Services/IBudgetService.cs
using System.Globalization;
using WalletWise.Persistence.Models;

namespace WalletWise.Services;

// Questo è un DTO (Data Transfer Object), un modello "usa e getta"
// per trasportare i dati calcolati al ViewModel.
public class BudgetStatus
{
    public string Category { get; set; } = string.Empty;
    public decimal BudgetedAmount { get; set; }
    public decimal SpentAmount { get; set; }
    public decimal RemainingAmount => BudgetedAmount - SpentAmount;
    public double Progress => BudgetedAmount > 0 ? (double)(SpentAmount / BudgetedAmount) : 0.0;

    // --- INIZIO MODIFICA CHIAVE ---
    // Nuove proprietà che restituiscono le stringhe già formattate.
    // Usiamo una CultureInfo specifica per essere sicuri di avere sempre il simbolo dell'Euro.
    private static readonly CultureInfo Culture = new("it-IT");
    public string SpentAmountDisplay => SpentAmount.ToString("C", Culture);
    public string BudgetedAmountDisplay => BudgetedAmount.ToString("C", Culture);
    // --- FINE MODIFICA CHIAVE ---
}

public interface IBudgetService
{
    Task<List<BudgetStatus>> GetBudgetStatusForMonthAsync(int year, int month);
    Task SaveBudgetAsync(Budget budget);
    Task DeleteBudgetAsync(string category, int year, int month);
}
