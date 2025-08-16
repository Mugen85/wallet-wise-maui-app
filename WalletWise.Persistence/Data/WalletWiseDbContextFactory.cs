// Data/WalletWiseDbContextFactory.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using WalletWise.Persistence.Data;

namespace WalletWise.Data;

public class WalletWiseDbContextFactory : IDesignTimeDbContextFactory<WalletWiseDbContext>
{
    public WalletWiseDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<WalletWiseDbContext>();

        // Usa un percorso temporaneo per il database durante la creazione della migrazione.
        // Questo non sarà il percorso usato dall'app reale.
        string dbPath = Path.Combine(Path.GetTempPath(), "walletwise-design.db3");

        optionsBuilder.UseSqlite($"Filename={dbPath}");

        return new WalletWiseDbContext(optionsBuilder.Options);
    }
}