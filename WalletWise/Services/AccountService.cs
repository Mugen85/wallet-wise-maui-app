// Services/AccountService.cs
using Microsoft.EntityFrameworkCore;
using WalletWise.Data;
using WalletWise.Models;

namespace WalletWise.Services;

public class AccountService(WalletWiseDbContext context) : IAccountService
{
    private readonly WalletWiseDbContext _context = context;

    public Task<List<Account>> GetAccountsAsync()
    {
        return _context.Accounts.ToListAsync();
    }

    public async Task AddAccountAsync(Account account)
    {
        _context.Accounts.Add(account);
        await _context.SaveChangesAsync();
    }
}
