// Views/AddAccountPage.xaml.cs
using WalletWise.ViewModels;

namespace WalletWise.Views;

public partial class AddAccountPage : ContentPage
{
    private readonly AddAccountViewModel _viewModel;

    public AddAccountPage(AddAccountViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    // --- MODIFICA CHIAVE: Carichiamo i dati quando la pagina appare ---
    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.LoadAccountTypesCommand.Execute(null);
    }
}
