// in WalletWise/Services/TransactionService.cs
using Microsoft.EntityFrameworkCore;
using WalletWise.Data;
using WalletWise.Persistence.Models;

namespace WalletWise.Services
{
    public class TransactionService(IDbContextFactory<WalletWiseDbContext> contextFactory) : ITransactionService
    {
        public async Task<List<Transaction>> GetTransactionsAsync()
        {
            await using var context = await contextFactory.CreateDbContextAsync();
            // Carichiamo le transazioni, includendo il conto associato,
            // e le ordiniamo per data e poi per Id, per garantire l'ordine corretto.
            return await context.Transactions
                                .Include(t => t.Account)
                                .OrderByDescending(t => t.Date)
                                .ThenByDescending(t => t.Id) // <-- Modifica per ordine corretto transazioni stesso giorno
                                .ToListAsync();
        }

        public async Task AddTransactionAsync(Transaction transaction)
        {
            await using var context = await contextFactory.CreateDbContextAsync();
            context.Transactions.Add(transaction);
            await context.SaveChangesAsync();
        }
    }

}