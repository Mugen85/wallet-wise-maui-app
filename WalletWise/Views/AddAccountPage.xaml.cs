// Views/AddAccountPage.xaml.cs
using WalletWise.Services;
using WalletWise.ViewModels;

namespace WalletWise.Views;

public partial class AddAccountPage : ContentPage
{
    private readonly AddAccountViewModel _viewModel;

    public AddAccountPage(AddAccountViewModel viewModel)
    {
        try
        {
            FileLogger.Log("AddAccountPage: costruttore avviato");
            InitializeComponent();
            FileLogger.Log("AddAccountPage: InitializeComponent completato");
            _viewModel = viewModel;
            BindingContext = _viewModel;
            FileLogger.Log("AddAccountPage: costruttore completato");
        }
        catch (Exception ex)
        {
            FileLogger.Log($"AddAccountPage ERRORE costruttore: {ex}");
        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.LoadAccountTypesCommand.Execute(null);
    }
}
