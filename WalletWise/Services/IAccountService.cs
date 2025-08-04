// Services/IAccountService.cs
using WalletWise.Persistence.Models;

namespace WalletWise.Services;

public interface IAccountService
{
    Task<List<Account>> GetAccountsAsync();
    Task AddAccountAsync(Account account);
    Task<decimal> GetTotalBalanceAsync();
    Task DeleteAccountAsync(int accountId);
}
