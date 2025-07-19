// ViewModels/AddAccountViewModel.cs
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using WalletWise.Models;
using WalletWise.Services;

namespace WalletWise.ViewModels;

public class AccountTypeDisplay
{
    public AccountType Value { get; set; }
    public string Name { get; set; } = string.Empty;
}

public partial class AddAccountViewModel(IAccountService accountService) : ObservableObject
{
    private readonly IAccountService _accountService = accountService;

    private string _name = string.Empty;
    public string Name { get => _name; set => SetProperty(ref _name, value); }

    private decimal _initialBalance;
    public decimal InitialBalance { get => _initialBalance; set => SetProperty(ref _initialBalance, value); }

    private AccountTypeDisplay? _selectedAccountType;
    public AccountTypeDisplay? SelectedAccountType { get => _selectedAccountType; set => SetProperty(ref _selectedAccountType, value); }

    public ObservableCollection<AccountTypeDisplay> AccountTypes { get; } = [];

    // --- MODIFICA CHIAVE: Carichiamo i dati in un comando separato ---
    [RelayCommand]
    private void LoadAccountTypes()
    {
        if (AccountTypes.Any()) return; // Esegui solo una volta

        var types = System.Enum.GetValues(typeof(AccountType))
                                 .Cast<AccountType>()
                                 .Select(at => new AccountTypeDisplay { Value = at, Name = at.ToString() });

        foreach (var type in types)
        {
            AccountTypes.Add(type);
        }
    }

    [RelayCommand]
    private async Task SaveAccountAsync()
    {
        if (string.IsNullOrWhiteSpace(Name) || SelectedAccountType is null)
        {
            return;
        }

        var newAccount = new Account
        {
            Name = this.Name,
            InitialBalance = this.InitialBalance,
            Type = this.SelectedAccountType.Value
        };

        await _accountService.AddAccountAsync(newAccount);
        await Shell.Current.GoToAsync("..");
    }
}