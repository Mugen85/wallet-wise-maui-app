// in WalletWise/Services/AccountService.cs
using Microsoft.EntityFrameworkCore;
using WalletWise.Data;
using WalletWise.Persistence.Models;

namespace WalletWise.Services;

public class AccountService(WalletWiseDbContext context) : IAccountService
{

    public async Task<List<Account>> GetAccountsAsync()
    {
        var accounts = await context.Accounts.Include(a => a.Transactions).ToListAsync();

        // Calcoliamo il saldo attuale per ogni conto prima di restituirlo.
        foreach (var account in accounts)
        {
            decimal income = account.Transactions
                .Where(t => t.Type == TransactionType.Income)
                .Sum(t => t.Amount);

            decimal expense = account.Transactions
                .Where(t => t.Type == TransactionType.Expense)
                .Sum(t => t.Amount);

            account.CurrentBalance = account.InitialBalance + income - expense;
        }

        return accounts;
    }

    public async Task AddAccountAsync(Account account)
    {
        context.Accounts.Add(account);
        await context.SaveChangesAsync();
    }

    public async Task<decimal> GetTotalBalanceAsync()
    {
        var accounts = await context.Accounts.Include(a => a.Transactions).ToListAsync();

        decimal totalBalance = 0;
        foreach (var account in accounts)
        {
            decimal income = account.Transactions
                .Where(t => t.Type == TransactionType.Income)
                .Sum(t => t.Amount);

            decimal expense = account.Transactions
                .Where(t => t.Type == TransactionType.Expense)
                .Sum(t => t.Amount);

            totalBalance += account.InitialBalance + income - expense;
        }
        return totalBalance;
    }

    public async Task DeleteAccountAsync(int accountId)
    {
        var account = await context.Accounts.FindAsync(accountId);
        if (account != null)
        {
            context.Accounts.Remove(account);
            await context.SaveChangesAsync();
        }
    }
}