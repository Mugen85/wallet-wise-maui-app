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

    // Proprietà per la formattazione sicura in XAML
    private static readonly CultureInfo Culture = new("it-IT");
    public string SpentAmountDisplay => SpentAmount.ToString("C", Culture);
    public string BudgetedAmountDisplay => BudgetedAmount.ToString("C", Culture);

    // Proprietà per il colore dinamico della UI
    public Color CategoryColor { get; set; } = Colors.Gray; // Colore di default
}


public interface IBudgetService
{
    Task<List<BudgetStatus>> GetBudgetStatusForMonthAsync(int year, int month);
    Task SaveBudgetAsync(Budget budget);
    Task DeleteBudgetAsync(string category, int year, int month);

    // --- INIZIO MODIFICA ---
    // Aggiungiamo la "promessa" che il nostro servizio saprà clonare i budget.
    Task CloneLastMonthBudgetsAsync(int currentYear, int currentMonth);
    // --- FINE MODIFICA ---
}
