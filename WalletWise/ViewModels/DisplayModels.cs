// in WalletWise/ViewModels/DisplayModels.cs
using WalletWise.Persistence.Models;

namespace WalletWise.ViewModels;

// Questo file è il nostro "magazzino" dei pezzi comuni.
// Applichiamo il principio DRY (Don't Repeat Yourself).
// Invece di duplicare queste classi in più ViewModel,
// le definiamo una sola volta, qui. Solido e a prova di bug.

/// <summary>
/// Un "DTO" (Data Transfer Object) solido per il nostro Picker "Tipo".
/// Disaccoppia la logica (l'enum) dal display (il nome).
/// </summary>
public class TransactionTypeDisplay
{
    public TransactionType Value { get; set; }
    public string Name { get; set; } = string.Empty;
}

/// <summary>
/// Un DTO solido per il nostro Picker "Frequenza".
/// </summary>
public class FrequencyDisplay
{
    public RecurrenceFrequency Value { get; set; }
    public string Name { get; set; } = string.Empty;
}