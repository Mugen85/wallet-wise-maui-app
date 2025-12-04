namespace WalletWise.Services;

public interface IAlertService
{
    Task<bool> ShowConfirmationAsync(string title, string message);

    // --- INIZIO MODIFICA (PEZZO 72.1) ---
    // Aggiungiamo il nostro attrezzo "OK" (per le notifiche)
    // È un costrutto solido perché separa le responsabilità.
    Task ShowAlertAsync(string title, string message);
    // --- FINE MODIFICA ---
}

