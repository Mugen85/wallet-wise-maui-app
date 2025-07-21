// Services/IAccountService.cs
using WalletWise.Models;

namespace WalletWise.Services;

public interface IAccountService
{
    Task<List<Account>> GetAccountsAsync();
    Task AddAccountAsync(Account account);
    Task<decimal> GetTotalBalanceAsync();
}
