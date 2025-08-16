// in WalletWise.Persistence/Models/Budget.cs
namespace WalletWise.Persistence.Models;

public class Budget
{
    public int Id { get; set; }
    public string Category { get; set; } = string.Empty;
    public decimal Amount { get; set; } // L'importo del budget mensile
    public int Month { get; set; } // Es. 8 per Agosto
    public int Year { get; set; }
}

