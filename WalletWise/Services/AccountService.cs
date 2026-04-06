// in WalletWise/Services/AccountService.cs
using Microsoft.EntityFrameworkCore;
using WalletWise.Persistence.Data;
using WalletWise.Persistence.Models;

namespace WalletWise.Services
{
    public class AccountService(IDbContextFactory<WalletWiseDbContext> contextFactory) : IAccountService
    {
        public async Task<List<Account>> GetAccountsAsync()
        {
            await using var context = await contextFactory.CreateDbContextAsync();

            // LINQ to Entities: Il calcolo viene tradotto in una query SQL SUM() 
            // eseguita direttamente dal motore SQLite. 
            // In RAM arriva solo un numero, non migliaia di oggetti Transaction!
            var accounts = await context.Accounts
                .Select(a => new Account
                {
                    Id = a.Id,
                    Name = a.Name,
                    InitialBalance = a.InitialBalance,
                    Type = a.Type,
                    CurrentBalance = a.InitialBalance +
                                     a.Transactions.Where(t => t.Type == TransactionType.Income).Sum(t => (decimal?)t.Amount ?? 0) -
                                     a.Transactions.Where(t => t.Type == TransactionType.Expense).Sum(t => (decimal?)t.Amount ?? 0)
                })
                .ToListAsync();

            return accounts;
        }

        public async Task<decimal> GetTotalBalanceAsync()
        {
            await using var context = await contextFactory.CreateDbContextAsync();
            var accounts = await context.Accounts.Include(a => a.Transactions).ToListAsync();

            // --- INIZIO LOGICA DI CALCOLO ---
            decimal totalBalance = 0;
            foreach (var account in accounts)
            {
                decimal income = account.Transactions.Where(t => t.Type == TransactionType.Income).Sum(t => t.Amount);
                decimal expense = account.Transactions.Where(t => t.Type == TransactionType.Expense).Sum(t => t.Amount);
                totalBalance += account.InitialBalance + income - expense;
            }
            // --- FINE LOGICA DI CALCOLO ---

            return totalBalance;
        }

        public async Task AddAccountAsync(Account account)
        {
            await using var context = await contextFactory.CreateDbContextAsync();
            context.Accounts.Add(account);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAccountAsync(int accountId)
        {
            await using var context = await contextFactory.CreateDbContextAsync();
            var account = await context.Accounts.FindAsync(accountId);
            if (account != null)
            {
                context.Accounts.Remove(account);
                await context.SaveChangesAsync();
            }
        }
    }
}