using System.ComponentModel;
using System.Globalization;
using WalletWise.Persistence.Models;
using WalletWise.ViewModels;

namespace WalletWise.Views;

/// <summary>
/// Questo è il "cablaggio" che collega il "telaio" (XAML)
/// al "motore" (ViewModel).
/// Applica la nostra architettura solida e a prova di bug.
/// </summary>
public partial class RecurringTransactionsPage : ContentPage
{
    private readonly RecurringTransactionsViewModel _viewModel;

    /// <summary>
    /// Costrutto solido: riceviamo il "motore" (ViewModel)
    /// dalla nostra "officina" (DI container).
    /// </summary>
	public RecurringTransactionsPage(RecurringTransactionsViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    /// <summary>
    /// Costrutto solido e usabile.
    /// Si attiva ogni volta che l'utente guarda questa pagina.
    /// </summary>
    protected override void OnAppearing()
    {
        base.OnAppearing();

        // 1. Ci "abboniamo" al sensore del motore.
        _viewModel.PropertyChanged += ViewModel_PropertyChanged;

        // 2. Diciamo al motore di caricare i dati.
        if (_viewModel.LoadDataCommand.CanExecute(null))
        {
            _viewModel.LoadDataCommand.Execute(null);
        }
    }

    /// <summary>
    /// Costrutto solido: ci "disabboniamo" per
    /// evitare perdite di memoria.
    /// </summary>
    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        _viewModel.PropertyChanged -= ViewModel_PropertyChanged;
    }

    /// <summary>
    /// Il nostro "reattore" a prova di bug.
    /// </summary>
    private void ViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        // Se il "sensore" del motore (la lista) cambia...
        if (e.PropertyName == nameof(_viewModel.RecurringItems))
        {
            // ...ricostruiamo il cruscotto "a mano".
            BuildRecurringList();
        }
    }

    /// <summary>
    /// Il nostro "metodo di assemblaggio" solido e a prova di bug.
    /// Bypassa i problemi di rendering XAML.
    /// </summary>
    private void BuildRecurringList()
    {
        // 1. Puliamo il "banco di lavoro"
        RecurringListContainer.Clear();
        if (_viewModel.RecurringItems == null) return;

        // 2. Costrutto usabile: se non c'è nulla, lo diciamo.
        if (!_viewModel.RecurringItems.Any())
        {
            RecurringListContainer.Children.Add(new Label
            {
                Text = "Nessun 'Pilota Automatico' impostato.",
                HorizontalOptions = LayoutOptions.Center,
                Margin = new Thickness(0, 20),
                TextColor = (Color)Application.Current!.Resources["SecondaryText"]
            });
            return;
        }

        // 3. Costruiamo la lista "a mano"
        foreach (var item in _viewModel.RecurringItems)
        {
            RecurringListContainer.Children.Add(RecurringTransactionsPage.CreateRecurringView(item));
        }
    }

    /// <summary>
    /// Pezzo solido: un metodo che costruisce un singolo pezzo.
    /// </summary>
    private static Frame CreateRecurringView(RecurringTransaction item)
    {
        var frame = new Frame
        {
            Style = (Style)Application.Current!.Resources["InfoCard"],
            Padding = 15,
        };

        // Usiamo un costrutto "Grid" semplice e solido
        var grid = new Grid
        {
            ColumnDefinitions = {
                new ColumnDefinition { Width = GridLength.Star },
                new ColumnDefinition { Width = GridLength.Auto }
            }
        };

        // Pezzo 1: Descrizione e Conto (Sostanza)
        var leftStack = new VerticalStackLayout { Spacing = 2 };
        leftStack.Children.Add(new Label
        {
            Text = item.Description,
            FontAttributes = FontAttributes.Bold,
            FontSize = 16
        });
        leftStack.Children.Add(new Label
        {
            Text = $"Conto: {item.Account.Name}",
            FontSize = 12,
            TextColor = (Color)Application.Current!.Resources["SecondaryText"]
        });
        grid.Add(leftStack, 0);

        // Pezzo 2: Importo (con i colori che sappiamo essere affidabili)
        var amountLabel = new Label
        {
            Text = item.Amount.ToString("C", new CultureInfo("it-IT")),
            FontAttributes = FontAttributes.Bold,
            FontSize = 16,
            VerticalOptions = LayoutOptions.Center,
            TextColor = (item.Type == TransactionType.Income) // Corretto
                        ? (Color)Application.Current!.Resources["Primary"]
                        : (Color)Application.Current!.Resources["Red"]
        };
        grid.Add(amountLabel, 1);

        frame.Content = grid;
        return frame;
    }
}