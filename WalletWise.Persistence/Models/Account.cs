// Models/Account.cs

using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
namespace WalletWise.Persistence.Models
{
    public class Account
    {
        public int Id { get; set; } // Chiave primaria per il database
        public string? Name { get; set; } // Es. "Conto Intesa Sanpaolo", "Fondi di Riserva"
        public decimal InitialBalance { get; set; } // Saldo iniziale
        public AccountType Type { get; set; } // Tipo di conto

        // Aggiungiamo la relazione con le transazioni
        public virtual ICollection<Transaction> Transactions { get; set; } = [];

        // Questa proprietà non viene salvata nel DB, ma calcolata dal service.
        [NotMapped]
        public decimal CurrentBalance { get; set; }
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
