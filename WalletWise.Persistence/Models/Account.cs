// Models/Account.cs
namespace WalletWise.Models
{
    public class Account
    {
        public int Id { get; set; } // Chiave primaria per il database
        public string? Name { get; set; } // Es. "Conto Intesa Sanpaolo", "Fondi di Riserva"
        public decimal InitialBalance { get; set; } // Saldo iniziale
        public AccountType Type { get; set; } // Tipo di conto
    }

    public enum AccountType
    {
        Checking,   // Conto Corrente per le spese
        Savings,    // Conto di Risparmio
        Investment  // Conto per gli investimenti
    }
}
