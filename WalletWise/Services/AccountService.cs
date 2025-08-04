// in WalletWise/Services/AccountService.cs
using Microsoft.EntityFrameworkCore;
using WalletWise.Data;
using WalletWise.Persistence.Models;

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

    // --- INIZIO METODO MODIFICATO ---
    public async Task<decimal> GetTotalBalanceAsync()
    {
        // NOTA: Eseguiamo la somma sul client (in memoria) perché il provider
        // SQLite di EF Core non supporta l'aggregazione 'Sum' su tipi 'decimal'.
        // Per un'app di finanza personale, il numero di conti è basso, quindi le
        // performance di questa operazione sono eccellenti.
        var accounts = await _context.Accounts.ToListAsync();
        return accounts.Sum(a => a.InitialBalance);
    }
    // --- FINE METODO MODIFICATO ---
    public async Task DeleteAccountAsync(int accountId)
    {
        var account = await _context.Accounts.FindAsync(accountId);
        if (account != null)
        {
            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();
        }
    }
}