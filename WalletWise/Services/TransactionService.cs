// in WalletWise/Services/TransactionService.cs
using Microsoft.EntityFrameworkCore;
using WalletWise.Data;
using WalletWise.Persistence.Models;

namespace WalletWise.Services
{
    public class TransactionService(IDbContextFactory<WalletWiseDbContext> contextFactory) : ITransactionService
    {
        public async Task AddTransactionAsync(Transaction transaction)
        {
            await using var context = await contextFactory.CreateDbContextAsync();
            context.Transactions.Add(transaction);
            await context.SaveChangesAsync();
        }
    }
}