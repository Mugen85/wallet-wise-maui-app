// Views/DashboardPage.xaml.cs
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

    protected override void OnAppearing()
    {
        base.OnAppearing();
        // Ci "iscriviamo" all'evento di navigazione quando la pagina appare
        Shell.Current.Navigated += Shell_Navigated;
        // Carichiamo i dati la prima volta
        LoadData();
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        // Ci "disiscriviamo" per evitare problemi di memoria
        Shell.Current.Navigated -= Shell_Navigated;
    }

    private void Shell_Navigated(object? sender, ShellNavigatedEventArgs e)
    {
        // Questo codice viene eseguito OGNI volta che la navigazione cambia.
        // Controlliamo se la pagina di destinazione è la nostra dashboard.
        if (e.Current.Location.OriginalString.Contains(nameof(DashboardPage)))
        {
            // Se sì, ricarichiamo i dati.
            LoadData();
        }
    }

    private void LoadData()
    {
        if (_viewModel.LoadDataCommand.CanExecute(null))
        {
            _viewModel.LoadDataCommand.Execute(null);
        }
    }
}
