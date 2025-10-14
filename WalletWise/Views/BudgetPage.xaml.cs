using System.ComponentModel;
using WalletWise.Services;
using WalletWise.ViewModels;

namespace WalletWise.Views;

public partial class BudgetPage : ContentPage
{
    private readonly BudgetViewModel _viewModel;

    public BudgetPage(BudgetViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.PropertyChanged += ViewModel_PropertyChanged;
        // Carichiamo i dati la prima volta
        RefreshData();
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        _viewModel.PropertyChanged -= ViewModel_PropertyChanged;
    }

    private void ViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        // Reagiamo solo quando la lista di budget cambia
        if (e.PropertyName == nameof(_viewModel.BudgetItems))
        {
            BuildBudgetList();
        }
    }

    private void RefreshData()
    {
        if (_viewModel.LoadBudgetsCommand.CanExecute(null))
        {
            _viewModel.LoadBudgetsCommand.Execute(null);
        }
    }

    private void BuildBudgetList()
    {
        BudgetListContainer.Clear();
        if (_viewModel.BudgetItems == null) return;

        foreach (var budgetStatus in _viewModel.BudgetItems)
        {
            BudgetListContainer.Children.Add(CreateBudgetView(budgetStatus));
        }
    }

    private Frame CreateBudgetView(BudgetStatus budgetData)
    {
        // Contenitore principale (Frame)
        var frame = new Frame
        {
            Style = (Style)Application.Current!.Resources["InfoCard"],
            Padding = 15
        };

        // Aggiungiamo il gesto del TAP per la modifica
        var tapGesture = new TapGestureRecognizer();
        tapGesture.Tapped += (s, e) =>
        {
            if (_viewModel.GoToEditBudgetCommand.CanExecute(budgetData))
            {
                _viewModel.GoToEditBudgetCommand.Execute(budgetData);
            }
        };
        frame.GestureRecognizers.Add(tapGesture);

        // Layout interno
        var mainLayout = new VerticalStackLayout { Spacing = 8 };

        // Riga 1: Categoria, Valori, Tasto Elimina
        var topRowGrid = new Grid
        {
            ColumnDefinitions =
            {
                new ColumnDefinition { Width = GridLength.Star },
                new ColumnDefinition { Width = GridLength.Auto }
            }
        };

        topRowGrid.Add(new Label
        {
            Text = budgetData.Category,
            FontAttributes = FontAttributes.Bold,
            VerticalOptions = LayoutOptions.Center
        }, 0, 0);

        var rightSideLayout = new HorizontalStackLayout { Spacing = 10, VerticalOptions = LayoutOptions.Center };

        var valuesLayout = new HorizontalStackLayout { Spacing = 5 };
        valuesLayout.Children.Add(new Label { Text = budgetData.SpentAmountDisplay, FontAttributes = FontAttributes.Bold, TextColor = budgetData.CategoryColor });
        valuesLayout.Children.Add(new Label { Text = " / ", TextColor = (Color)Application.Current.Resources["SecondaryText"] });
        valuesLayout.Children.Add(new Label { Text = budgetData.BudgetedAmountDisplay, TextColor = (Color)Application.Current.Resources["SecondaryText"] });

        var deleteButton = new Button
        {
            Text = "X",
            Style = (Style)Application.Current.Resources["DeleteButton"],
            Command = _viewModel.DeleteBudgetCommand,
            CommandParameter = budgetData
        };

        rightSideLayout.Children.Add(valuesLayout);
        rightSideLayout.Children.Add(deleteButton);
        topRowGrid.Add(rightSideLayout, 1, 0);

        // Riga 2: Barra di Progresso
        var progressBar = new ProgressBar
        {
            Progress = budgetData.Progress,
            ProgressColor = budgetData.CategoryColor,
            HeightRequest = 8
        };

        mainLayout.Children.Add(topRowGrid);
        mainLayout.Children.Add(progressBar);

        frame.Content = mainLayout;
        return frame;
    }
}
