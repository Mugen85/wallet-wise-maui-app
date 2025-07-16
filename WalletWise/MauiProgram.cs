// MauiProgram.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Wallet_Wise;
using WalletWise.Data;

namespace WalletWise;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        // 1. Definisci il percorso del database
        string dbPath = Path.Combine(FileSystem.AppDataDirectory, "walletwise.db3");

        // 2. Registra il DbContext per la Dependency Injection
        builder.Services.AddDbContext<WalletWiseDbContext>(options =>
            options.UseSqlite($"Filename={dbPath}", b =>
            b.MigrationsAssembly("WalletWise.Persistence")));

        var app = builder.Build();

        // 3. Applica le migrazioni DOPO aver costruito l'app
        // Questo è il punto più sicuro per interagire con i servizi.
        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<WalletWiseDbContext>();
            dbContext.Database.Migrate();
        }

        return app;
    }
}