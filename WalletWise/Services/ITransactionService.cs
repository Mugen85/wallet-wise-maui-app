// in WalletWise/Services/ITransactionService.cs
using WalletWise.Persistence.Models;

namespace WalletWise.Services;

public interface ITransactionService
{
    Task AddTransactionAsync(Transaction transaction);
}
