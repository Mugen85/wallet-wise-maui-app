// Models/Account.cs

using System.ComponentModel;
namespace WalletWise.Persistence.Models
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
        [Description("Stipendio / Spese")]
        StipendioSpese,

        [Description("Risparmio")]
        Risparmio,

        [Description("Investimento")]
        Investimento
    }
}
