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

        // 1. Prendi tutti i budget definiti per il mese/anno corrente
        var budgetsForMonth = await context.Budgets
            .Where(b => b.Year == year && b.Month == month)
            .ToListAsync();

        // 2. Prendi tutte le transazioni di USCITA per lo stesso periodo
        var expensesForMonth = await context.Transactions
            .Where(t => t.Date.Year == year && t.Date.Month == month && t.Type == TransactionType.Expense)
            .ToListAsync();

        // 3. Raggruppa le spese per categoria e calcola il totale per ciascuna
        var spentByCategory = expensesForMonth
            .GroupBy(t => t.Category)
            .ToDictionary(g => g.Key, g => g.Sum(t => t.Amount));

        // 4. Costruisci la lista finale
        var statusList = new List<BudgetStatus>();
        foreach (var budget in budgetsForMonth)
        {
            // Cerca la spesa corrispondente. Se non c'è, la spesa è 0.
            spentByCategory.TryGetValue(budget.Category, out var spentAmount);

            statusList.Add(new BudgetStatus
            {
                Category = budget.Category,
                BudgetedAmount = budget.Amount,
                SpentAmount = spentAmount // <-- Ecco la magia!
            });
        }

        return statusList;
    }

    // in WalletWise/Services/BudgetService.cs
    public async Task SaveBudgetAsync(Budget budget)
    {
        await using var context = await contextFactory.CreateDbContextAsync();

        var upperCategory = budget.Category.ToUpper();

        var existingBudget = await context.Budgets
            .FirstOrDefaultAsync(b =>
                b.Year == budget.Year &&
                b.Month == budget.Month &&
                b.Category != null && // <-- QUESTO CONTROLLO È FONDAMENTALE
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

}