namespace WalletWise.Persistence.Models;

/// <summary>
/// Questo è il "contratto", il nostro biglietto da visita
/// per la gestione del "Pilota Automatico".
/// Definisce "cosa" il servizio deve fare, non "come".
/// </summary>
public interface IRecurringTransactionService
{
    /// <summary>
    /// La "promessa" di saper aggiungere una nuova transazione ricorrente.
    /// </summary>
    Task AddRecurringTransactionAsync(RecurringTransaction transaction);

    /// <summary>
    /// La "promessa" di saper recuperare tutti i "Piloti Automatici"
    /// salvati dall'utente.
    /// </summary>
    Task<List<RecurringTransaction>> GetRecurringTransactionsAsync();

    // (In Pezzi futuri aggiungeremo qui la
    // "promessa" di modificare ed eliminare)
}