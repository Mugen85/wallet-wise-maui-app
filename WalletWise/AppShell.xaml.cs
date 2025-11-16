// in WalletWise/AppShell.xaml.cs
using WalletWise.Views;

namespace WalletWise;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        // Registriamo solo le rotte che usiamo per la navigazione
        // "standard" (quelle che non hanno costruttori complessi).
        Routing.RegisterRoute(nameof(AddAccountPage), typeof(AddAccountPage));
        Routing.RegisterRoute(nameof(AddTransactionPage), typeof(AddTransactionPage));
        Routing.RegisterRoute(nameof(AddBudgetPage), typeof(AddBudgetPage));
        // NOTA: Non registriamo più OnboardingPage qui.

        Routing.RegisterRoute(nameof(RecurringTransactionsPage), typeof(RecurringTransactionsPage));
        Routing.RegisterRoute(nameof(AddRecurringTransactionPage), typeof(AddRecurringTransactionPage));

    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        bool hasCompletedOnboarding = Preferences.Get("has_completed_onboarding", false);

        if (!hasCompletedOnboarding)
        {
            // Defensive null checks for Handler and MauiContext to avoid CS8602
            var mauiContext = Handler?.MauiContext;
            var services = mauiContext?.Services;
            var onboardingPage = services?.GetService<OnboardingPage>();

            if (onboardingPage != null)
            {
                await Shell.Current.Navigation.PushModalAsync(onboardingPage, false);
            }
        }
    }
}