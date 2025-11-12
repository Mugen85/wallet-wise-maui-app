// in WalletWise/ViewModels/OnboardingViewModel.cs
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WalletWise.Persistence.Models;
using WalletWise.Services;

namespace WalletWise.ViewModels;

public partial class OnboardingViewModel(IAccountService accountService) : ObservableObject
{
    [ObservableProperty]
    private string _accountName = string.Empty;

    // Usiamo decimal? (nullable) per gestire in modo robusto
    // un Entry che può essere vuoto, come abbiamo imparato.
    // È la soluzione solida.
    [ObservableProperty]
    private decimal? _initialBalance;

    [RelayCommand]
    private async Task StartAsync()
    {
        // 1. Validazione (Sostanza prima di tutto)
        // Ci assicuriamo che i dati siano validi prima di procedere.
        if (string.IsNullOrWhiteSpace(AccountName) || !InitialBalance.HasValue || InitialBalance.Value < 0)
        {
            // In futuro, potremmo mostrare un messaggio all'utente.
            // Per ora, un blocco solido è sufficiente.
            return;
        }

        // 2. Creazione del Conto
        var newAccount = new Account
        {
            Name = AccountName,
            InitialBalance = InitialBalance.Value,
            Type = AccountType.StipendioSpese // Impostiamo un tipo di default solido
        };
        await accountService.AddAccountAsync(newAccount);

        // 3. Impostiamo il "Flag"
        // Questo è il pezzo chiave. Diciamo all'app di non mostrare
        // più questa pagina in futuro.
        Preferences.Set("has_completed_onboarding", true);

        // 4. Navigazione
        // Abbiamo finito. Portiamo l'utente al cuore dell'app.
        // --- INIZIO MODIFICA (PEZZO 5) ---
        // Usiamo "//" invece di "//MainPage".
        // Questo è il comando "assoluto" più robusto: significa
        // "vai alla radice dell'app e carica la tua tab di default,
        // qualunque essa sia". È la soluzione a prova di bug.
        await Shell.Current.GoToAsync("//");
        // --- FINE MODIFICA ---
    }
}