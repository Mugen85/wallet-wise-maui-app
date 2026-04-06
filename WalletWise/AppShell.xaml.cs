using WalletWise.Services;
using WalletWise.Views;

namespace WalletWise;

public partial class AppShell : Shell
{
    // Usiamo una variabile privata per assicurarci
    // che la logica giri una sola volta.
    private bool _isStartupCheckDone = false;

    public AppShell()
    {
        try
        {
            FileLogger.Log("AppShell: costruttore avviato");
            InitializeComponent();
            FileLogger.Log("AppShell: InitializeComponent completato");
            Routing.RegisterRoute(nameof(AddAccountPage), typeof(AddAccountPage));
            Routing.RegisterRoute(nameof(AddTransactionPage), typeof(AddTransactionPage));
            Routing.RegisterRoute(nameof(AddBudgetPage), typeof(AddBudgetPage));
            Routing.RegisterRoute(nameof(RecurringTransactionsPage), typeof(RecurringTransactionsPage));
            Routing.RegisterRoute(nameof(AddRecurringTransactionPage), typeof(AddRecurringTransactionPage));
            FileLogger.Log("AppShell: route registrate");
            this.Loaded += AppShell_Loaded;
            FileLogger.Log("AppShell: costruttore completato");
        }
        catch (Exception ex)
        {
            FileLogger.Log($"AppShell ERRORE: {ex}");
        }
    }

    private async void AppShell_Loaded(object? sender, EventArgs e)
    {
        FileLogger.Log("AppShell_Loaded: evento scattato");
        if (_isStartupCheckDone) return;
        _isStartupCheckDone = true;

        try
        {
            FileLogger.Log("AppShell_Loaded: controllo onboarding");
            bool hasCompletedOnboarding = Preferences.Get("has_completed_onboarding", false);
            FileLogger.Log($"AppShell_Loaded: hasCompletedOnboarding = {hasCompletedOnboarding}");

            if (!hasCompletedOnboarding)
            {
                var onboardingPage = this.Handler?.MauiContext?.Services.GetService<OnboardingPage>();
                FileLogger.Log($"AppShell_Loaded: onboardingPage = {(onboardingPage == null ? "NULL" : "OK")}");
                if (onboardingPage != null)
                {
                    await this.Navigation.PushModalAsync(onboardingPage, false);
                    FileLogger.Log("AppShell_Loaded: onboarding mostrato");
                }
            }
            FileLogger.Log("AppShell_Loaded: completato");
        }
        catch (Exception ex)
        {
            FileLogger.Log($"AppShell_Loaded ERRORE: {ex}");
        }
    }

    // Rimuoviamo il metodo OnAppearing che non usiamo più per questo scopo
}