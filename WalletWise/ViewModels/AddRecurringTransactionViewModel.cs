using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;
using WalletWise.Persistence.Models;
using WalletWise.Services;

namespace WalletWise.ViewModels;

/// <summary>
/// Questo è il "motore" per la pagina di aggiunta del Pilota Automatico.
/// È una centralina solida, moderna e focalizzata sull'usabilità.
/// </summary>
public partial class AddRecurringTransactionViewModel : ObservableObject
{
    private readonly IRecurringTransactionService _recurringService;
    private readonly IAccountService _accountService;

    // Collezioni per i nostri Picker
    public ObservableCollection<Account> Accounts { get; } = [];
    public ObservableCollection<TransactionTypeDisplay> TransactionTypes { get; } = [];
    public ObservableCollection<string> Categories { get; } = [];
    public ObservableCollection<FrequencyDisplay> Frequencies { get; } = [];
    public ObservableCollection<int> DaysOfMonth { get; } = [];

    // "Sensori" (Proprietà) collegati al "Telaio" (XAML)
    [ObservableProperty]
    private Account? _selectedAccount;

    [ObservableProperty]
    private TransactionTypeDisplay? _selectedTransactionType;

    // Costrutto solido e collaudato: decimal? a prova di bug di parse.
    [ObservableProperty]
    private decimal? _amount;

    [ObservableProperty]
    private string _description = string.Empty;

    [ObservableProperty]
    private string? _selectedCategory;

    [ObservableProperty]
    private FrequencyDisplay? _selectedFrequency;

    [ObservableProperty]
    private int _selectedDayOfMonth;

    [ObservableProperty]
    private DateTime _startDate = DateTime.Today;

    // Costrutto solido: il "sensore" per la nostra UI intelligente.
    [ObservableProperty]
    private bool _isMonthly;

    // Costruttore solido: riceve i motori (Services) dalla
    // centralina (DI) e prepara il cruscotto.
    public AddRecurringTransactionViewModel(
        IRecurringTransactionService recurringService,
        IAccountService accountService)
    {
        _recurringService = recurringService;
        _accountService = accountService;
        PopulateStaticPickers();
    }

    // Costrutto moderno: logica "reattiva" solida.
    // Si attiva quando l'utente sceglie il tipo (Income/Expense).
    partial void OnSelectedTransactionTypeChanged(TransactionTypeDisplay? value)
    {
        Categories.Clear();
        SelectedCategory = null;
        if (value?.Value == TransactionType.Income) // Corretto
        {
            CategoryData.GetIncomeCategories().ForEach(c => Categories.Add(c));
        }
        else if (value?.Value == TransactionType.Expense) // Corretto
        {
            CategoryData.GetExpenseCategories().ForEach(c => Categories.Add(c));
        }
    }

    // Costrutto moderno: logica "reattiva" solida.
    // Si attiva quando l'utente sceglie la frequenza.
    partial void OnSelectedFrequencyChanged(FrequencyDisplay? value)
    {
        IsMonthly = value?.Value == RecurrenceFrequency.Mensile;
    }

    /// <summary>
    /// Comando solido per caricare i dati che dipendono dal database (i conti).
    /// </summary>
    [RelayCommand]
    private async Task LoadDataAsync()
    {
        Accounts.Clear();
        var accountsList = await _accountService.GetAccountsAsync();
        foreach (var acc in accountsList)
        {
            Accounts.Add(acc);
        }
        // Impostiamo un default usabile
        SelectedAccount = Accounts.FirstOrDefault();
    }

    /// <summary>
    /// Comando solido per salvare il "Pilota Automatico".
    /// </summary>
    [RelayCommand]
    private async Task SaveAsync()
    {
        // Validazione solida. Sostanza, non apparenza.
        if (SelectedAccount == null ||
            SelectedTransactionType == null ||
            SelectedFrequency == null ||
            !Amount.HasValue ||
            Amount.Value <= 0 ||
            string.IsNullOrWhiteSpace(Description) ||
            string.IsNullOrWhiteSpace(SelectedCategory))
        {
            // In futuro, qui useremo il nostro IAlertService collaudato.
            // Per ora, un blocco solido è la scelta giusta.
            return;
        }

        var newRecurringTransaction = new RecurringTransaction
        {
            AccountId = SelectedAccount.Id,
            Type = SelectedTransactionType.Value,
            Amount = Amount.Value, // .Value è sicuro grazie alla validazione
            Description = Description,
            Category = SelectedCategory,
            Frequency = SelectedFrequency.Value,
            DayOfMonth = IsMonthly ? SelectedDayOfMonth : 0, // Logica solida
            StartDate = StartDate
        };

        await _recurringService.AddRecurringTransactionAsync(newRecurringTransaction);
        await AddRecurringTransactionViewModel.CancelAsync(); // Riusiamo il comando di uscita. DRY.
    }

    [RelayCommand]
    private static async Task CancelAsync()
    {
        await Shell.Current.GoToAsync(".."); // Costrutto collaudato
    }

    /// <summary>
    /// Metodo solido per popolare i nostri menu a tendina.
    /// </summary>
    private void PopulateStaticPickers()
    {
        // --- INIZIO MODIFICA CHIAVE ---
        // Costrutto solido: riutilizziamo la logica collaudata
        // per leggere gli attributi [Description] dagli Enum.
        var types = Enum.GetValues<TransactionType>()
                        .Select(tt => new TransactionTypeDisplay { Value = tt, Name = GetEnumDescription(tt) });
        foreach (var type in types)
        {
            TransactionTypes.Add(type);
        }

        var frequencies = Enum.GetValues<RecurrenceFrequency>()
                              .Select(f => new FrequencyDisplay { Value = f, Name = GetEnumDescription(f) });
        foreach (var freq in frequencies)
        {
            Frequencies.Add(freq);
        }
        // --- FINE MODIFICA CHIAVE ---

        // Categorie (ora si popola in OnSelectedTransactionTypeChanged)

        // Giorni del mese
        foreach (var day in Enumerable.Range(1, 31))
        {
            DaysOfMonth.Add(day);
        }

        // Impostiamo dei default solidi e usabili
        SelectedFrequency = Frequencies.FirstOrDefault(f => f.Value == RecurrenceFrequency.Mensile);
        SelectedDayOfMonth = 1;
        // Costrutto collaudato: impostiamo il default a Expense
        SelectedTransactionType = TransactionTypes.FirstOrDefault(t => t.Value == TransactionType.Expense);
    }

    /// <summary>
    /// Il nostro "attrezzo" solido e riutilizzabile per leggere le
    /// descrizioni dagli Enum.
    /// </summary>
    private static string GetEnumDescription(Enum enumObj)
    {
        FieldInfo? fieldInfo = enumObj.GetType().GetField(enumObj.ToString());
        if (fieldInfo is null) return enumObj.ToString();
        // --- Corretto errore di battitura "fieldTifno" ---
        var attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
        return attributes.Length > 0 ? attributes[0].Description : enumObj.ToString();
    }
}