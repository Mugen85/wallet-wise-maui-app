// Data/WalletWiseDbContext.cs
using Microsoft.EntityFrameworkCore;
using WalletWise.Models;

namespace WalletWise.Data;

public partial class WalletWiseDbContext(DbContextOptions<WalletWiseDbContext> options) : DbContext(options)
{
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
}
