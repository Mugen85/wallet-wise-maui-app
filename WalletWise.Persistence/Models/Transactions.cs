// in WalletWise.Persistence/Models/Transaction.cs
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace WalletWise.Persistence.Models;

public class Transaction
{
    public int Id { get; set; }
    public decimal Amount { get; set; } // L'importo è sempre positivo
    public DateTime Date { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public TransactionType Type { get; set; }

    public int AccountId { get; set; }
    [ForeignKey("AccountId")]
    public Account? Account { get; set; }
}

public enum TransactionType
{
    [Description("Entrata")]
    Income,

    [Description("Uscita")]
    Expense
}