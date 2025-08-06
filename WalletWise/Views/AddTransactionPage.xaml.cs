// in WalletWise/Views/AddTransactionPage.xaml.cs
using WalletWise.ViewModels;

namespace WalletWise.Views;

public partial class AddTransactionPage : ContentPage
{
    // Usiamo un costruttore standard per avere il controllo completo
    public AddTransactionPage(AddTransactionViewModel viewModel)
    {
        InitializeComponent();

        // Questa è la riga fondamentale che collega la View al ViewModel
        BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is AddTransactionViewModel vm)
        {
            await vm.LoadDataCommand.ExecuteAsync(null);
        }
    }
}
