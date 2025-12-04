// in WalletWise/Services/AlertService.cs
namespace WalletWise.Services;

public class AlertService : IAlertService
{
    public async Task<bool> ShowConfirmationAsync(string title, string message)
    {
        if (App.Current?.MainPage == null) return false;
        return await App.Current.MainPage.DisplayAlert(title, message, "Sì", "No");
    }

    // --- INIZIO MODIFICA (PEZZO 72.2) ---
    // Costruiamo il motore per il nostro attrezzo "OK".
    // È solido, usa l'API standard e fa una cosa sola.
    public async Task ShowAlertAsync(string title, string message)
    {
        if (App.Current?.MainPage == null) return;
        await App.Current.MainPage.DisplayAlert(title, message, "OK");
    }
    // --- FINE MODIFICA ---
}