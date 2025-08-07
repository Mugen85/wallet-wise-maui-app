// in ViewModels/AccountsViewModel.cs
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using WalletWise.Persistence.Models;
using WalletWise.Services;
using WalletWise.Views;

namespace WalletWise.ViewModels;

public partial class AccountsViewModel(IAccountService accountService) : ObservableObject
{
    private readonly IAccountService _accountService = accountService;

    private ObservableCollection<Account> _accounts = [];
    public ObservableCollection<Account> Accounts
    {
        get => _accounts;
        set => SetProperty(ref _accounts, value);
    }

    [RelayCommand]
    private async Task LoadAccountsAsync()
    {
        var accountsList = await _accountService.GetAccountsAsync();
        Accounts.Clear();
        foreach (var account in accountsList)
        {
            Accounts.Add(account);
        }
    }

    [RelayCommand]
    private async Task AddAccountAsync()
    {
        await Shell.Current.GoToAsync(nameof(AddAccountPage));
    }

    [RelayCommand]
    private async Task DeleteAccountAsync(Account account)
    {
        if (account == null) return;
        await _accountService.DeleteAccountAsync(account.Id);
        Accounts.Remove(account);
    }
}
