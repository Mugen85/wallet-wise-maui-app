// ViewModels/AccountsViewModel.cs
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
        // Naviga alla pagina di aggiunta conto.
        // Usiamo nameof per evitare di scrivere stringhe a mano ("magic strings")
        await Shell.Current.GoToAsync(nameof(AddAccountPage));
    }

    [RelayCommand]
    private async Task DeleteAccountAsync(Account account)
    {
        if (account == null) return;

        await _accountService.DeleteAccountAsync(account.Id);

        // Rimuovi l'account dalla collezione per aggiornare la UI istantaneamente
        Accounts.Remove(account);
    }
}
