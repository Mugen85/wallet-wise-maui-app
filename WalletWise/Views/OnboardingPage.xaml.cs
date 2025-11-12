// in WalletWise/Views/OnboardingPage.xaml.cs
using WalletWise.ViewModels;

namespace WalletWise.Views;

// Il nome della classe "OnboardingPage" e la classe base "ContentPage"
// ora combaciano perfettamente con il file XAML.
public partial class OnboardingPage : ContentPage
{
    // Questo è il costruttore moderno.
    // Riceve il "motore" (il ViewModel) dalla "centralina" (MauiProgram.cs)
    // grazie alla Dependency Injection. È il costrutto solido.
    public OnboardingPage(OnboardingViewModel viewModel)
    {
        // Ora 'InitializeComponent' esisterà, perché i nomi dei file combaciano
        // e il XAML (riparato) può essere compilato.
        InitializeComponent();

        // Colleghiamo il motore (ViewModel) al telaio (View).
        // Questo è il cablaggio fondamentale per far funzionare i Binding.
        BindingContext = viewModel;
    }
}