using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace WalletWise.Persistence.Models;

/// <summary>
/// Definisce la "sostanza" della nostra logica di ricorrenza.
/// Semplice, solido e a prova di bug.
/// </summary>
public enum RecurrenceFrequency
{
    [Description("Mensile")]
    Mensile,

    [Description("Settimanale")]
    Settimanale,

    [Description("Annuale")]
    Annuale
}

/// <summary>
/// Questo è il nostro "disegno tecnico".
/// Rappresenta il "contratto" di un Pilota Automatico nel database.
/// </summary>
public class RecurringTransaction
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public TransactionType Type { get; set; } // Entrata o Uscita

    // La "schedulazione"
    public RecurrenceFrequency Frequency { get; set; }

    // Il giorno del mese (es. 1, 15, 27)
    // Usato solo se la Frequenza è Mensile.
    public int DayOfMonth { get; set; }

    public DateTime StartDate { get; set; } = DateTime.Today;

    // La data di fine è "nullable" (DateTime?).
    // Questo è un costrutto solido: se è null, significa "per sempre".
    public DateTime? EndDate { get; set; }

    // Il collegamento solido al conto
    public int AccountId { get; set; }
    [ForeignKey(nameof(AccountId))]
    public virtual Account Account { get; set; } = null!;
}