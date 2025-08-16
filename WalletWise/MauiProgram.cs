// MauiProgram.cs
using Microsoft.EntityFrameworkCore;
using WalletWise.Persistence.Data;
using WalletWise.Persistence.Models;
using WalletWise.Services;
using WalletWise.ViewModels;
using WalletWise.Views;

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
        // --- INIZIO MODIFICA 1: Usiamo la DbContextFactory ---
        builder.Services.AddDbContextFactory<WalletWiseDbContext>(options =>
            options.UseSqlite($"Filename={dbPath}"));

        // Registrazione dei Service
        // --- INIZIO MODIFICA 2: I servizi ora possono essere Singleton ---
        IServiceCollection serviceCollection = builder.Services.AddSingleton<IAccountService, AccountService>();
        builder.Services.AddSingleton<ITransactionService, TransactionService>();
        builder.Services.AddTransient<AddTransactionViewModel>();
        builder.Services.AddTransient<AddTransactionPage>();
        builder.Services.AddTransient<TransactionsViewModel>();
        builder.Services.AddTransient<TransactionsPage>();

        // Registrazione dei ViewModel
        builder.Services.AddSingleton<IBudgetService, BudgetService>();
        builder.Services.AddTransient<BudgetViewModel>();
        builder.Services.AddTransient<BudgetPage>();
        builder.Services.AddTransient<AccountsViewModel>();
        builder.Services.AddTransient<AddAccountViewModel>();
        builder.Services.AddTransient<DashboardViewModel>(); //

        // Registrazione delle View
        builder.Services.AddTransient<AccountsPage>();
        builder.Services.AddTransient<AddAccountPage>();
        builder.Services.AddTransient<DashboardPage>();

        var app = builder.Build();

        // 3. Applica le migrazioni DOPO aver costruito l'app
        // Questo è il punto più sicuro per interagire con i servizi.
        // --- INIZIO MODIFICA 3: Aggiorniamo la logica di migrazione ---
        var dbContextFactory = app.Services.GetRequiredService<IDbContextFactory<WalletWiseDbContext>>();
        using (var dbContext = dbContextFactory.CreateDbContext())
        {
            dbContext.Database.Migrate();

            // --- INIZIO MODIFICA: Aggiungiamo i dati di prova ---
            // Controlliamo se ci sono già dei budget per evitare di aggiungerli ogni volta
            if (!dbContext.Budgets.Any())
            {
                var now = DateTime.Now;
                dbContext.Budgets.AddRange(
                    new Budget { Category = "Spesa Alimentare", Amount = 400, Month = now.Month, Year = now.Year },
                    new Budget { Category = "Trasporti", Amount = 150, Month = now.Month, Year = now.Year },
                    new Budget { Category = "Svago", Amount = 100, Month = now.Month, Year = now.Year }
                );
                dbContext.SaveChanges();
            }
            // --- FINE MODIFICA ---

        }

        return app;
    }
}