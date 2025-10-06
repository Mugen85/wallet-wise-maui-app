// in WalletWise/Services/BudgetService.cs
using Microsoft.EntityFrameworkCore;
using System.Diagnostics; // Puoi lasciarlo per futuri debug
using WalletWise.Persistence;
using WalletWise.Persistence.Data;
using WalletWise.Persistence.Models;

namespace WalletWise.Services;

public class BudgetService(IDbContextFactory<WalletWiseDbContext> contextFactory) : IBudgetService
{
    public async Task<List<BudgetStatus>> GetBudgetStatusForMonthAsync(int year, int month)
    {
        await using var context = await contextFactory.CreateDbContextAsync();

        var budgetsForMonth = await context.Budgets
            .Where(b => b.Year == year && b.Month == month)
            .ToListAsync();

        var expensesForMonth = await context.Transactions
            .Where(t => t.Date.Year == year && t.Date.Month == month && t.Type == TransactionType.Expense)
            .ToListAsync();

        // Raggruppa le spese per categoria (ignorando maiuscole/minuscole)
        var spentByCategory = expensesForMonth
            .Where(t => !string.IsNullOrEmpty(t.Category))
            .GroupBy(t => t.Category, StringComparer.OrdinalIgnoreCase)
            .ToDictionary(g => g.Key, g => g.Sum(t => t.Amount), StringComparer.OrdinalIgnoreCase);

        var statusList = new List<BudgetStatus>();
        foreach (var budget in budgetsForMonth)
        {
            spentByCategory.TryGetValue(budget.Category, out var spentAmount);

            statusList.Add(new BudgetStatus
            {
                Category = budget.Category,
                BudgetedAmount = budget.Amount,
                SpentAmount = spentAmount
            });
        }
        return statusList;
    }

    public async Task SaveBudgetAsync(Budget budget)
    {
        await using var context = await contextFactory.CreateDbContextAsync();
        var upperCategory = budget.Category.ToUpper();
        var existingBudget = await context.Budgets
            .FirstOrDefaultAsync(b =>
                b.Year == budget.Year &&
                b.Month == budget.Month &&
                b.Category != null &&
                b.Category.ToUpper() == upperCategory);

        if (existingBudget != null)
        {
            existingBudget.Amount = budget.Amount;
        }
        else
        {
            context.Budgets.Add(budget);
        }
        await context.SaveChangesAsync();
    }

    public async Task DeleteBudgetAsync(string category, int year, int month)
    {
        await using var context = await contextFactory.CreateDbContextAsync();

        // 1. Trova il budget da eliminare
        var budgetToDelete = await context.Budgets
            .FirstOrDefaultAsync(b =>
                b.Year == year &&
                b.Month == month &&
                b.Category == category);

        if (budgetToDelete != null)
        {
            context.Budgets.Remove(budgetToDelete);
        }

        // 2. Trova TUTTE le transazioni corrispondenti (ignorando maiuscole/minuscole)
        var transactionsToDelete = await context.Transactions
            .Where(t =>
                t.Date.Year == year &&
                t.Date.Month == month &&
                !string.IsNullOrEmpty(t.Category) &&
                t.Category.ToUpper() == category.ToUpper())
            .ToListAsync();

        if (transactionsToDelete.Any())
        {
            context.Transactions.RemoveRange(transactionsToDelete);
        }

        // 3. Salva tutte le modifiche (cancellazione budget + transazioni) in un colpo solo
        if (budgetToDelete != null || transactionsToDelete.Any())
        {
            await context.SaveChangesAsync();
        }
    }
}
