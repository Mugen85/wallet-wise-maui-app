// in WalletWise.Persistence/Models/RecurringTransaction.cs
using System.ComponentModel.DataAnnotations.Schema;

namespace WalletWise.Persistence.Models;

// Definiamo le opzioni per la ricorrenza.
// Semplice, solido e a prova di bug.
public enum RecurrenceFrequency
{
    Mensile,
    Settimanale,
    Annuale
}

public class RecurringTransaction
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public string? Description { get; set; }
    public string? Category { get; set; }
    public TransactionType Type { get; set; } // Entrata o Uscita

    // La "schedulazione"
    public RecurrenceFrequency Frequency { get; set; }
    public int DayOfMonth { get; set; } // Es. 1 (per il 1° del mese)
    public DateTime StartDate { get; set; } = DateTime.Today;
    public DateTime? EndDate { get; set; } // Nullabile = "per sempre"

    // Il collegamento al conto
    public int AccountId { get; set; }
    [ForeignKey(nameof(AccountId))]
    public virtual Account? Account { get; set; }
}