// in WalletWise/Services/BudgetService.cs
using Microsoft.EntityFrameworkCore;
using WalletWise.Persistence.Data;
using WalletWise.Persistence.Models;
using WalletWise.ViewModels;

namespace WalletWise.Services;

public class BudgetService(IDbContextFactory<WalletWiseDbContext> contextFactory) : IBudgetService
{
    public async Task<List<BudgetStatus>> GetBudgetStatusForMonthAsync(int year, int month)
    {
        await using var context = await contextFactory.CreateDbContextAsync();

        // 1. Prendi tutti i budget per il mese corrente
        var budgets = await context.Budgets
            .Where(b => b.Year == year && b.Month == month)
            .ToListAsync();

        // 2. Prendi tutte le uscite per il mese corrente
        var expenses = await context.Transactions
            .Where(t => t.Type == TransactionType.Expense && t.Date.Year == year && t.Date.Month == month)
            .ToListAsync();

        var budgetStatusList = new List<BudgetStatus>();

        // 3. Per ogni budget, calcola la spesa totale
        foreach (var budget in budgets)
        {
            var spentAmount = expenses
                .Where(e => e.Category.Equals(budget.Category, StringComparison.OrdinalIgnoreCase))
                .Sum(e => e.Amount);

            budgetStatusList.Add(new BudgetStatus
            {
                Category = budget.Category,
                BudgetedAmount = budget.Amount,
                SpentAmount = spentAmount
            });
        }

        return budgetStatusList;
    }
}