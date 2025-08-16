// in WalletWise/Services/IBudgetService.cs
using WalletWise.Persistence.Models;
using WalletWise.ViewModels; // Aggiungeremo un DTO qui

namespace WalletWise.Services;

// Questo è un DTO (Data Transfer Object), un modello "usa e getta"
// per trasportare i dati calcolati al ViewModel.
public class BudgetStatus
{
    public string Category { get; set; } = string.Empty;
    public decimal BudgetedAmount { get; set; }
    public decimal SpentAmount { get; set; }
    public decimal RemainingAmount => BudgetedAmount - SpentAmount;
}

public interface IBudgetService
{
    Task<List<BudgetStatus>> GetBudgetStatusForMonthAsync(int year, int month);
    Task SaveBudgetAsync(Budget budget);
}
