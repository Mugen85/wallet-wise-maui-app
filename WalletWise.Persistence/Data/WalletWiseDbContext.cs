// Data/WalletWiseDbContext.cs
using Microsoft.EntityFrameworkCore;
using WalletWise.Persistence.Models;

namespace WalletWise.Persistence.Data;

public partial class WalletWiseDbContext(DbContextOptions<WalletWiseDbContext> options) : DbContext(options)
{
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<Budget> Budgets { get; set; } // Aggiunto DbSet per il modello Budget

    // --- INIZIO MODIFICA ---
    // Aggiungiamo il "cassetto" per il nostro nuovo pezzo.
    public DbSet<RecurringTransaction> RecurringTransactions { get; set; }
    // --- FINE MODIFICA ---
}
