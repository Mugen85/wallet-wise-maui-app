// in WalletWise/Views/DashboardPage.xaml.cs
using System.Diagnostics; // Aggiungi questo namespace
using WalletWise.ViewModels;

namespace WalletWise.Views;

public partial class DashboardPage : ContentPage
{
    private readonly DashboardViewModel _viewModel;

    public DashboardPage(DashboardViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        _viewModel = viewModel;
    }

    // Questo metodo viene chiamato da MAUI ogni volta che la pagina appare sullo schermo.
    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (_viewModel.LoadDataCommand.CanExecute(null))
        {
            _viewModel.LoadDataCommand.Execute(null);
        }
    }
}