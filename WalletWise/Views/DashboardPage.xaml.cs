// Views/DashboardPage.xaml.cs
using WalletWise.ViewModels;

namespace WalletWise.Views;

public partial class DashboardPage : ContentPage
{
    private readonly DashboardViewModel _viewModel;

    public DashboardPage(DashboardViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;

        // --- INIZIO MODIFICA: Colleghiamo il "cavo" per l'aggiornamento ---
        _viewModel.InvalidateChartRequest += () => PieChartGraphicsView.Invalidate();
        // --- FINE MODIFICA ---
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (_viewModel.LoadDataCommand.CanExecute(null))
        {
            await _viewModel.LoadDataCommand.ExecuteAsync(null);
        }
    }
}
