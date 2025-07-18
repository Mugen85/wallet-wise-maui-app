// ViewModels/AccountsViewModel.cs
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using WalletWise.Models;
using WalletWise.Services;

namespace WalletWise.ViewModels;

public partial class AccountsViewModel(IAccountService accountService) : ObservableObject
{
    private readonly IAccountService _accountService = accountService;

    // --- MODIFICA INIZIO ---
    // Abbiamo reso la proprietà "Accounts" esplicita per risolvere
    // un potenziale problema con il source generator [ObservableProperty].
    // Invece di usare [ObservableProperty], definiamo la proprietà completa.
    // Il funzionamento logico non cambia, ma questo approccio è più robusto.
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
        // Per ora, aggiungiamo un conto di prova.
        // In futuro, questo aprirà una nuova pagina.
        var newAccount = new Account
        {
            Name = $"Nuovo Conto {DateTime.Now:T}",
            InitialBalance = 100,
            Type = AccountType.Checking
        };
        await _accountService.AddAccountAsync(newAccount);

        // Ricarica la lista per mostrare il nuovo conto
        await LoadAccountsAsync();
    }
}
