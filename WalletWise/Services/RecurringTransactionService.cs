using Microsoft.EntityFrameworkCore;
using WalletWise.Persistence.Data;

namespace WalletWise.Persistence.Models;

/// <summary>
/// Questo è il "motore" solido e a prova of bug.
/// Implementa il contratto IRecurringTransactionService.
/// </summary>
public class RecurringTransactionService(IDbContextFactory<WalletWiseDbContext> contextFactory) : IRecurringTransactionService
{
    /// <summary>
    /// Aggiunge una nuova transazione ricorrente.
    /// Usa il nostro costrutto collaudato: la Factory.
    /// Garantisce che l'operazione sia pulita e sicura.
    /// </summary>
    public async Task AddRecurringTransactionAsync(RecurringTransaction transaction)
    {
        await using var context = await contextFactory.CreateDbContextAsync();
        await context.RecurringTransactions.AddAsync(transaction);
        await context.SaveChangesAsync();
    }

    /// <summary>
    /// Recupera tutti i "Piloti Automatici" salvati.
    /// </summary>
    public async Task<List<RecurringTransaction>> GetRecurringTransactionsAsync()
    {
        await using var context = await contextFactory.CreateDbContextAsync();

        // Usiamo "Include" per caricare anche i dati del conto
        // a cui la transazione è collegata.
        // Questo è un costrutto solido ("eager loading") che ci dà
        // tutta la sostanza di cui avremo bisogno per la UI, in una sola
        // chiamata al database.
        return await context.RecurringTransactions
            .Include(t => t.Account)
            .AsNoTracking()
            .ToListAsync();
    }
}