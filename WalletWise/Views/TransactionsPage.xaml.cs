// in WalletWise/Views/TransactionsPage.xaml.cs
using WalletWise.ViewModels;

namespace WalletWise.Views;

public partial class TransactionsPage : ContentPage
{
    public TransactionsPage(TransactionsViewModel viewModel)
    {
        InitializeComponent();
        // Questa è la riga fondamentale che collega la View al ViewModel
        BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is TransactionsViewModel vm)
        {
            // Usiamo Execute invece di ExecuteAsync per evitare warning
            // in un metodo sincrono come OnAppearing.
            vm.LoadTransactionsCommand.Execute(null);
        }
    }
}