// in WalletWise/Views/AddRecurringTransactionPage.xaml.cs
using WalletWise.ViewModels;

namespace WalletWise.Views;

// Il nome della classe "partial" combacia perfettamente
// con il nome nel file XAML. Questo è il "ponte".
public partial class AddRecurringTransactionPage : ContentPage
{
    private readonly AddRecurringTransactionViewModel _viewModel;

    // Costrutto solido: riceviamo il "motore" (ViewModel)
    // dalla nostra "officina" (DI container).
    public AddRecurringTransactionPage(AddRecurringTransactionViewModel viewModel)
    {
        // Questa chiamata ora "salderà" il ponte.
        // Gli errori XLS0413 spariranno (dopo il Rebuild finale).
        InitializeComponent();

        // 1. Salviamo il "motore"
        _viewModel = viewModel;

        // 2. Colleghiamo il "cablaggio" solido.
        // Ora il telaio XAML può parlare con il motore C#.
        BindingContext = _viewModel;
    }

    // 3. Costrutto solido e usabile.
    // Diciamo al motore di caricare i dati (es. i conti)
    // solo quando la pagina sta per apparire.
    protected override void OnAppearing()
    {
        base.OnAppearing();
        // Usiamo il comando che abbiamo già definito nel ViewModel.
        if (_viewModel.LoadDataCommand.CanExecute(null))
        {
            _viewModel.LoadDataCommand.Execute(null);
        }
    }
}