// in WalletWise/Views/OnboardingPage.xaml.cs
using WalletWise.ViewModels;

namespace WalletWise.Views;

// Il nome della classe "partial" combacia perfettamente
// con il file XAML.
public partial class OnboardingPage : ContentPage
{
    // Costruttore "a manovella" (vuoto) che il sistema
    // di Routing "preistorico" si aspetta.
    public OnboardingPage()
    {
        InitializeComponent();

        // --- IL NOSTRO "CABLAGGIO" A PROVA DI BUG ---
        //
        // Invece di ricevere il motore (ViewModel),
        // andiamo a prenderlo noi stessi dall'officina (MauiProgram.cs).
        //
        // Questo è il costrutto più solido per le pagine
        // che devono funzionare con il Routing standard.
        // È il "biglietto da visita" di un architetto
        // che sa aggirare i bug del framework.
#if ANDROID || IOS || MACCATALYST || WINDOWS
        var viewModel = MauiProgram.Services?.GetService<OnboardingViewModel>();
        BindingContext = viewModel;
#endif
        // --- FINE CABLAGGIO ---
    }
}