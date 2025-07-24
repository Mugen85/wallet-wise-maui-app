// Views/DashboardPage.xaml.cs
using WalletWise.ViewModels;

namespace WalletWise.Views;

public partial class DashboardPage : ContentPage
{
    public DashboardPage(DashboardViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is DashboardViewModel vm && vm.LoadDataCommand.CanExecute(null))
        {
            await vm.LoadDataCommand.ExecuteAsync(null);
        }
    }
}