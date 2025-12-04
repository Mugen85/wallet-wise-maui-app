// in WalletWise/ViewModels/OnboardingViewModel.cs
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WalletWise.Persistence.Models;
using WalletWise.Services;

namespace WalletWise.ViewModels;

// --- MODIFICA CHIRURGICA 1: Iniezione dell'AlertService ---
// Aggiungiamo il nostro servizio collaudato per la
// notifica all'utente. Sostanza e Usabilità.
public partial class OnboardingViewModel(
    IAccountService accountService,
    IAlertService alertService) : ObservableObject
{
    [ObservableProperty]
    private string _accountName = string.Empty;

    [ObservableProperty]
    private decimal? _initialBalance;

    [RelayCommand]
    private async Task StartAsync()
    {
        // --- INIZIO MODIFICA (PEZZO 72.3) ---
        // Usiamo l'attrezzo GIUSTO (ShowAlertAsync)
        // invece di quello sbagliato (ShowConfirmationAsync).
        if (string.IsNullOrWhiteSpace(AccountName) || !InitialBalance.HasValue || InitialBalance.Value < 0)
        {
            await alertService.ShowAlertAsync("Dati Mancanti",
                "Per favore, inserisci un nome valido per il conto e un saldo iniziale (anche 0).");
            return;
        }
        // --- FINE MODIFICA ---

        // 2. Creazione del Conto (Questo era già solido)
        var newAccount = new Account
        {
            Name = AccountName,
            InitialBalance = InitialBalance.Value,
            Type = AccountType.StipendioSpese
        };
        await accountService.AddAccountAsync(newAccount);

        // 3. Impostiamo il "Flag" (Questo era già solido)
        Preferences.Set("has_completed_onboarding", true);

        // --- MODIFICA CHIRURGICA 3: Navigazione Solida ---
        // Sostituiamo l'attrezzo sbagliato (GoToAsync)
        // con l'attrezzo GIUSTO (PopModalAsync).
        // Questo è il costrutto a prova di bug per
        // chiudere una pagina Modale.
        await Shell.Current.Navigation.PopModalAsync();
    }
}