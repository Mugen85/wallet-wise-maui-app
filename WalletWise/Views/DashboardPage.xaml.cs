using WalletWise.ViewModels;
using WalletWise.Services; // Assicurati che questo using ci sia

namespace WalletWise.Views;

public partial class DashboardPage : ContentPage
{
    private readonly DashboardViewModel _viewModel;

    public DashboardPage(DashboardViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        _viewModel = viewModel;

        // Ci mettiamo in ascolto: quando i dati dei budget cambiano,
        // la nostra pagina lo saprà e reagirà.
        _viewModel.PropertyChanged += ViewModel_PropertyChanged;
    }

    private void ViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        // Controlliamo se la proprietà cambiata è proprio la nostra lista di budget.
        if (e.PropertyName == nameof(_viewModel.BudgetSummary))
        {
            // Se sì, aggiorniamo il display.
            RefreshBudgetDisplay();
        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        // Chiediamo i dati ogni volta che la pagina appare.
        if (_viewModel.LoadDataCommand.CanExecute(null))
        {
            _viewModel.LoadDataCommand.Execute(null);
        }
    }

    private void RefreshBudgetDisplay()
    {
        // 1. Svuotiamo il contenitore da eventuali pezzi vecchi.
        BudgetContainer.Clear();

        if (_viewModel.BudgetSummary == null) return;

        // 2. Per ogni budget nei dati, costruiamo il suo "display".
        foreach (var budgetStatus in _viewModel.BudgetSummary)
        {
            // Creiamo il pezzo e lo "leghiamo" ai dati del budget corrente.
            var budgetView = CreateBudgetView(budgetStatus);
            // 3. Montiamo il pezzo finito nel nostro contenitore.
            BudgetContainer.Children.Add(budgetView);
        }
    }

    private Grid CreateBudgetView(BudgetStatus budgetData)
    {
        var grid = new Grid
        {
            RowDefinitions =
            {
                new RowDefinition { Height = GridLength.Auto },
                new RowDefinition { Height = GridLength.Auto }
            },
            ColumnDefinitions =
            {
                new ColumnDefinition { Width = GridLength.Star }, // Categoria
                new ColumnDefinition { Width = GridLength.Auto }  // Valori numerici
            },
            RowSpacing = 5,
            Padding = new Thickness(0, 5)
        };

        // Riga 0, Colonna 0: Nome della Categoria
        var categoryLabel = new Label
        {
            Text = budgetData.Category,
            FontAttributes = FontAttributes.Bold,
            VerticalOptions = LayoutOptions.Center
        };
        grid.Add(categoryLabel, 0, 0);

        // Riga 0, Colonna 1: Valori Numerici (con HorizontalStackLayout)
        var valuesLayout = new HorizontalStackLayout { Spacing = 5, VerticalOptions = LayoutOptions.Center };
        valuesLayout.Children.Add(new Label { Text = budgetData.SpentAmountDisplay, FontAttributes = FontAttributes.Bold, TextColor = budgetData.CategoryColor });
        valuesLayout.Children.Add(new Label { Text = " / ", TextColor = Color.FromArgb("#6e6e6e") }); // Colore secondario
        valuesLayout.Children.Add(new Label { Text = budgetData.BudgetedAmountDisplay, TextColor = Color.FromArgb("#6e6e6e") });
        grid.Add(valuesLayout, 1, 0);

        // Riga 1, su entrambe le colonne: Barra di Progresso
        var progressBar = new ProgressBar
        {
            Progress = budgetData.Progress,
            ProgressColor = budgetData.CategoryColor,
            HeightRequest = 8
        };
        grid.Add(progressBar, 0, 1);
        Grid.SetColumnSpan(progressBar, 2);

        return grid;
    }
}