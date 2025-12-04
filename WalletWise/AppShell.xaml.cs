using WalletWise.Views;
using Microsoft.Maui.Storage;

namespace WalletWise;

public partial class AppShell : Shell
{
    // Usiamo una variabile privata per assicurarci
    // che la logica giri una sola volta.
    private bool _isStartupCheckDone = false;

    public AppShell()
    {
        InitializeComponent();

        // Mappa GPS Solida
        Routing.RegisterRoute(nameof(AddAccountPage), typeof(AddAccountPage));
        Routing.RegisterRoute(nameof(AddTransactionPage), typeof(AddTransactionPage));
        Routing.RegisterRoute(nameof(AddBudgetPage), typeof(AddBudgetPage));
        Routing.RegisterRoute(nameof(RecurringTransactionsPage), typeof(RecurringTransactionsPage));
        Routing.RegisterRoute(nameof(AddRecurringTransactionPage), typeof(AddRecurringTransactionPage));

        // --- INIZIO MODIFICA CHIRURGICA ---
        // Invece di usare OnAppearing (troppo presto),
        // ci abboniamo all'evento Loaded.
        // Questo scatta solo quando la Shell è completamente
        // montata e pronta (Handler != null).
        this.Loaded += AppShell_Loaded;
        // --- FINE MODIFICA CHIRURGICA ---
    }

    private async void AppShell_Loaded(object? sender, EventArgs e)
    {
        // Se abbiamo già fatto il controllo, usciamo.
        if (_isStartupCheckDone) return;
        _isStartupCheckDone = true;

        // --- RESET PER COLLAUDO (DA TOGLIERE DOPO) ---
        Preferences.Remove("has_completed_onboarding");
        // ---------------------------------------------

        bool hasCompletedOnboarding = Preferences.Get("has_completed_onboarding", false);

        if (!hasCompletedOnboarding)
        {
            // Ora siamo nell'evento Loaded, quindi Handler NON è null.
            // Possiamo chiamare l'officina con sicurezza.
            var onboardingPage = this.Handler?.MauiContext?.Services.GetService<OnboardingPage>();

            if (onboardingPage != null)
            {
                // Navigazione modale sicura
                await this.Navigation.PushModalAsync(onboardingPage, false);
            }
        }
    }

    // Rimuoviamo il metodo OnAppearing che non usiamo più per questo scopo
}