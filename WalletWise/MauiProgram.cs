// MauiProgram.cs
using Microsoft.EntityFrameworkCore;
using WalletWise.Data;
using WalletWise.Services;
using WalletWise.ViewModels;
using WalletWise.Views;
using CommunityToolkit.Mvvm.Messaging;

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

        // Registrazione dei Service
        builder.Services.AddSingleton<IAccountService, AccountService>();
        builder.Services.AddSingleton<ITransactionService, TransactionService>();
        builder.Services.AddSingleton<IMessenger>(WeakReferenceMessenger.Default);
        builder.Services.AddTransient<AddTransactionViewModel>();
        builder.Services.AddTransient<AddTransactionPage>();

        // Registrazione dei ViewModel
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
        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<WalletWiseDbContext>();
            dbContext.Database.Migrate();
        }

        return app;
    }
}