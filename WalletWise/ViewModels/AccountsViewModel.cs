// in ViewModels/AccountsViewModel.cs
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;
using WalletWise.Messages;
using WalletWise.Persistence.Models;
using WalletWise.Services;
using WalletWise.Views;

namespace WalletWise.ViewModels;

public partial class AccountsViewModel : ObservableObject
{
    private readonly IAccountService _accountService;
    private readonly IMessenger _messenger;

    private ObservableCollection<Account> _accounts = [];
    public ObservableCollection<Account> Accounts
    {
        get => _accounts;
        set => SetProperty(ref _accounts, value);
    }

    public AccountsViewModel(IAccountService accountService, IMessenger messenger)
    {
        _accountService = accountService;
        _messenger = messenger;

        // Registra il ViewModel per ricevere il messaggio
        messenger.Register<TransactionAddedMessage>(this, (r, m) =>
        {
            // Messaggio ricevuto! Aggiorniamo il saldo del conto specifico.
            var accountToUpdate = Accounts.FirstOrDefault(a => a.Id == m.Value.AccountId);
            if (accountToUpdate != null)
            {
                // Ricarichiamo TUTTI i conti per avere i saldi aggiornati
                // Questa è la soluzione più semplice e affidabile per ora
                if (LoadAccountsCommand.CanExecute(null))
                {
                    LoadAccountsCommand.Execute(null);
                }
            }
        });
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
