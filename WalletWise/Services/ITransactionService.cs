// in WalletWise/Services/ITransactionService.cs
using WalletWise.Persistence.Models;

namespace WalletWise.Services;

public interface ITransactionService
{
    Task<List<Transaction>> GetTransactionsAsync();
    Task AddTransactionAsync(Transaction transaction);
}
