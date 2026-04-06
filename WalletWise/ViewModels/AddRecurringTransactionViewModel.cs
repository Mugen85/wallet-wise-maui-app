using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System;
using System.Linq;
using System.Threading.Tasks;
using WalletWise.Persistence.Models;
using WalletWise.Services;
// Assicurati che Microsoft.Maui.Controls (per Shell) sia visibile nei tuoi global usings, altrimenti aggiungilo.

namespace WalletWise.ViewModels;

// --- I WRAPPER UI (La nostra armatura contro il Trimmer AOT) ---
public class AccountDisplayModel
{
    public Account Value { get; init; } = null!;
    public string Name { get; init; } = string.Empty;
    public override string ToString() => Name;
}

public class TransactionTypeDisplayModel
{
    public TransactionType Value { get; init; }
    public string Name { get; init; } = string.Empty;
    public override string ToString() => Name;
}

public class FrequencyDisplayModel
{
    public RecurrenceFrequency Value { get; init; }
    public string Name { get; init; } = string.Empty;
    public override string ToString() => Name;
}

/// <summary>
/// Questo è il "motore" per la pagina di aggiunta del Pilota Automatico.
/// È una centralina solida, moderna e focalizzata sull'usabilità.
/// Riprogettata per zero-reflection e piena compatibilità AOT .NET 9.
/// </summary>
public partial class AddRecurringTransactionViewModel : ObservableObject
{
    private readonly IRecurringTransactionService _recurringService;
    private readonly IAccountService _accountService;

    // Collezioni per i nostri Picker
    public ObservableCollection<AccountDisplayModel> Accounts { get; } = [];
    public ObservableCollection<TransactionTypeDisplayModel> TransactionTypes { get; } = [];
    public ObservableCollection<string> Categories { get; } = [];
    public ObservableCollection<FrequencyDisplayModel> Frequencies { get; } = [];
    public ObservableCollection<int> DaysOfMonth { get; } = [];

    // "Sensori" (Proprietà) collegati al "Telaio" (XAML)
    [ObservableProperty]
    private AccountDisplayModel? _selectedAccount;

    [ObservableProperty]
    private TransactionTypeDisplayModel? _selectedTransactionType;

    // Collettore crudo per la stringa per aggirare il bug decimal in MAUI
    [ObservableProperty]
    private string _amountText = string.Empty;

    [ObservableProperty]
    private string _description = string.Empty;

    [ObservableProperty]
    private string? _selectedCategory;

    [ObservableProperty]
    private FrequencyDisplayModel? _selectedFrequency;

    [ObservableProperty]
    private int _selectedDayOfMonth;

    [ObservableProperty]
    private DateTime _startDate = DateTime.Today;

    // Costrutto solido: il "sensore" per la nostra UI intelligente.
    [ObservableProperty]
    private bool _isMonthly;

    // Costruttore solido
    public AddRecurringTransactionViewModel(
        IRecurringTransactionService recurringService,
        IAccountService accountService)
    {
        _recurringService = recurringService;
        _accountService = accountService;
        PopulateStaticPickers();
    }

    // Costrutto moderno: logica "reattiva" solida.
    partial void OnSelectedTransactionTypeChanged(TransactionTypeDisplayModel? value)
    {
        Categories.Clear();
        SelectedCategory = null;
        if (value?.Value == TransactionType.Income)
        {
            CategoryData.GetIncomeCategories().ForEach(c => Categories.Add(c));
        }
        else if (value?.Value == TransactionType.Expense)
        {
            CategoryData.GetExpenseCategories().ForEach(c => Categories.Add(c));
        }
    }

    // Costrutto moderno: logica "reattiva" solida.
    partial void OnSelectedFrequencyChanged(FrequencyDisplayModel? value)
    {
        IsMonthly = value?.Value == RecurrenceFrequency.Mensile;
    }

    /// <summary>
    /// Comando solido per caricare i dati che dipendono dal database (i conti).
    /// </summary>
    [RelayCommand]
    private async Task LoadDataAsync()
    {
        try
        {
            Accounts.Clear();
            var accountsList = await _accountService.GetAccountsAsync();
            foreach (var acc in accountsList)
            {
                // Avvolgiamo il conto reale nel display model sicuro
                Accounts.Add(new AccountDisplayModel { Value = acc, Name = acc.Name ?? "Conto Senza Nome" });
            }
            // Impostiamo un default usabile
            SelectedAccount = Accounts.FirstOrDefault();
        }
        catch (Exception ex)
        {
            FileLogger.Log($"AddRecurringTransactionViewModel LoadDataAsync ERRORE: {ex}");
        }
    }

    /// <summary>
    /// Comando solido per salvare il "Pilota Automatico".
    /// </summary>
    [RelayCommand]
    private async Task SaveAsync()
    {
        try
        {
            FileLogger.Log("AddRecurringTransactionViewModel: SaveAsync avviato");

            // --- IL PARSING CORAZZATO ---
            decimal parsedAmount = 0m;
            if (!string.IsNullOrWhiteSpace(AmountText))
            {
                string normalizedInput = AmountText.Replace(",", ".");
                decimal.TryParse(normalizedInput, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out parsedAmount);
            }

            // Validazione solida (Fail-Fast). Sostanza, non apparenza.
            if (SelectedAccount == null ||
                SelectedTransactionType == null ||
                SelectedFrequency == null ||
                parsedAmount <= 0 ||
                string.IsNullOrWhiteSpace(Description) ||
                string.IsNullOrWhiteSpace(SelectedCategory))
            {
                FileLogger.Log("AddRecurringTransactionViewModel: validazione fallita");
                return;
            }

            var newRecurringTransaction = new RecurringTransaction
            {
                AccountId = SelectedAccount.Value.Id,
                Type = SelectedTransactionType.Value,
                Amount = parsedAmount,
                Description = Description.Trim(),
                Category = SelectedCategory,
                Frequency = SelectedFrequency.Value,
                DayOfMonth = IsMonthly ? SelectedDayOfMonth : 0,
                StartDate = StartDate
            };

            await _recurringService.AddRecurringTransactionAsync(newRecurringTransaction);
            FileLogger.Log("AddRecurringTransactionViewModel: Pilota Automatico salvato con successo");

            await CancelAsync();
        }
        catch (Exception ex)
        {
            FileLogger.Log($"AddRecurringTransactionViewModel SaveAsync ERRORE: {ex}");
        }
    }

    [RelayCommand]
    private static async Task CancelAsync()
    {
        await Shell.Current.GoToAsync("..");
    }

    /// <summary>
    /// Metodo solido per popolare i nostri menu a tendina.
    /// </summary>
    private void PopulateStaticPickers()
    {
        // Costrutto solido: bypassiamo la reflection usando gli switch AOT-friendly
        var types = Enum.GetValues<TransactionType>()
                        .Select(tt => new TransactionTypeDisplayModel { Value = tt, Name = GetTransactionTypeDescription(tt) });
        foreach (var type in types)
        {
            TransactionTypes.Add(type);
        }

        var frequencies = Enum.GetValues<RecurrenceFrequency>()
                              .Select(f => new FrequencyDisplayModel { Value = f, Name = GetFrequencyDescription(f) });
        foreach (var freq in frequencies)
        {
            Frequencies.Add(freq);
        }

        // Giorni del mese
        foreach (var day in Enumerable.Range(1, 31))
        {
            DaysOfMonth.Add(day);
        }

        // Impostiamo dei default solidi e usabili
        SelectedFrequency = Frequencies.FirstOrDefault(f => f.Value == RecurrenceFrequency.Mensile);
        SelectedDayOfMonth = 1;
        SelectedTransactionType = TransactionTypes.FirstOrDefault(t => t.Value == TransactionType.Expense);
    }

    // --- I TRADUTTORI STATICI (Sostituiscono gli attributi [Description]) ---
    private static string GetTransactionTypeDescription(TransactionType type) => type switch
    {
        TransactionType.Income => "Entrata",
        TransactionType.Expense => "Uscita",
        _ => type.ToString()
    };

    private static string GetFrequencyDescription(RecurrenceFrequency freq) => freq switch
    {
        RecurrenceFrequency.Settimanale => "Settimanale",
        RecurrenceFrequency.Mensile => "Mensile",
        RecurrenceFrequency.Annuale => "Annuale",
        _ => freq.ToString()
    };
}