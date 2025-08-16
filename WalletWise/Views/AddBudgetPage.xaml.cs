// in WalletWise/Views/AddBudgetPage.xaml.cs
using WalletWise.ViewModels;

namespace WalletWise.Views;

public partial class AddBudgetPage : ContentPage
{
    public AddBudgetPage(AddBudgetViewModel viewModel)
    {
        InitializeComponent();
        // Collega la View al suo ViewModel. Senza questa riga, i comandi non funzionano.
        BindingContext = viewModel;
    }
}
