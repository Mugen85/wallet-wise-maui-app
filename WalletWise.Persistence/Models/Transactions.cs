// Models/Transaction.cs
using System.ComponentModel.DataAnnotations.Schema;

namespace WalletWise.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public decimal Amount { get; set; } // Positivo per entrate, negativo per uscite
        public DateTime Date { get; set; }
        public string? Description { get; set; }
        public TransactionType Type { get; set; }

        // Relazione con il conto
        public int AccountId { get; set; }
        [ForeignKey("AccountId")]
        public Account? Account { get; set; }
    }

    public enum TransactionType
    {
        Income,     // Entrata
        Expense,    // Spesa
        Investment  // Movimento di investimento
    }
}