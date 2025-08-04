// in ViewModels/AddAccountViewModel.cs
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;
using WalletWise.Persistence.Models;
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

    [RelayCommand]
    private void LoadAccountTypes()
    {
        if (AccountTypes.Any()) return; // Esegui solo una volta

        var types = System.Enum.GetValues(typeof(AccountType))
                                 .Cast<AccountType>()
                                 .Select(at => new AccountTypeDisplay { Value = at, Name = GetEnumDescription(at) });

        foreach (var type in types)
        {
            AccountTypes.Add(type);
        }
    }

    private static string GetEnumDescription(Enum enumObj)
    {
        FieldInfo? fieldInfo = enumObj.GetType().GetField(enumObj.ToString());
        if (fieldInfo == null) return enumObj.ToString();

        var attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
        return attributes.Length > 0 ? attributes[0].Description : enumObj.ToString();
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