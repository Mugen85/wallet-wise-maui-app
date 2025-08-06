// in WalletWise/Services/TransactionService.cs
using WalletWise.Data;
using WalletWise.Persistence.Models;

namespace WalletWise.Services;

public class TransactionService(WalletWiseDbContext context) : ITransactionService
{
    public async Task AddTransactionAsync(Transaction transaction)
    {
        context.Transactions.Add(transaction);
        await context.SaveChangesAsync();
    }
}

