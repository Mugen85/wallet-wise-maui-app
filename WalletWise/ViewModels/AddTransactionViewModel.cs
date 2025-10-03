using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;
using WalletWise.Persistence.Models;
using WalletWise.Services;

namespace WalletWise.ViewModels;

public class TransactionTypeDisplay { public TransactionType Value { get; set; } public string Name { get; set; } = string.Empty; }

public partial class AddTransactionViewModel(ITransactionService transactionService, IAccountService accountService) : ObservableObject
{
    [ObservableProperty] private decimal _amount;
    [ObservableProperty] private string _description = string.Empty;
    [ObservableProperty] private DateTime _date = DateTime.Today;

    [ObservableProperty]
    private TransactionTypeDisplay? _selectedTransactionType;

    [ObservableProperty] private Account? _selectedAccount;
    [ObservableProperty] private string? _selectedCategory;

    public ObservableCollection<TransactionTypeDisplay> TransactionTypes { get; } = [];
    public ObservableCollection<Account> Accounts { get; } = [];
    public ObservableCollection<string> Categories { get; } = []; // Ora parte vuota

    // Questo metodo viene chiamato automaticamente ogni volta che l'utente cambia il tipo di transazione.
    partial void OnSelectedTransactionTypeChanged(TransactionTypeDisplay? value)
    {
        Categories.Clear();
        SelectedCategory = null; // Resetta la categoria selezionata

        if (value?.Value == TransactionType.Expense)
        {
            CategoryData.GetExpenseCategories().ForEach(c => Categories.Add(c));
        }
        else if (value?.Value == TransactionType.Income)
        {
            CategoryData.GetIncomeCategories().ForEach(c => Categories.Add(c));
        }
    }

    [RelayCommand]
    private async Task LoadDataAsync()
    {
        // La logica per conti e tipi non cambia
        if (!Accounts.Any())
        {
            var accountsList = await accountService.GetAccountsAsync();
            foreach (var acc in accountsList) Accounts.Add(acc);
        }

        if (!TransactionTypes.Any())
        {
            var types = Enum.GetValues<TransactionType>()
                            .Select(tt => new TransactionTypeDisplay { Value = tt, Name = GetEnumDescription(tt) });
            foreach (var type in types) TransactionTypes.Add(type);
        }

        // Impostiamo uno stato di default (es. Uscita)
        if (SelectedTransactionType == null && TransactionTypes.Any())
        {
            SelectedTransactionType = TransactionTypes.First(t => t.Value == TransactionType.Expense);
        }
    }

    [RelayCommand]
    private async Task SaveTransactionAsync()
    {
        if (SelectedAccount is null || SelectedTransactionType is null || Amount <= 0) return;

        var newTransaction = new Transaction
        {
            Amount = Amount,
            Description = Description,
            Category = SelectedCategory ?? string.Empty,
            Date = Date,
            Type = SelectedTransactionType.Value,
            AccountId = SelectedAccount.Id
        };
        await transactionService.AddTransactionAsync(newTransaction);

        await Shell.Current.GoToAsync("..");
    }

    private static string GetEnumDescription(Enum enumObj)
    {
        FieldInfo? fieldInfo = enumObj.GetType().GetField(enumObj.ToString());
        if (fieldInfo is null) return enumObj.ToString();
        var attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
        return attributes.Length > 0 ? attributes[0].Description : enumObj.ToString();
    }
}
