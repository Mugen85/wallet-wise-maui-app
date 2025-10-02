namespace WalletWise.Services;

public class AlertService : IAlertService
{
    public async Task<bool> ShowConfirmationAsync(string title, string message)
    {
        if (App.Current?.MainPage == null)
        {
            // Non possiamo mostrare un alert se non c'è una pagina visibile.
            return false;
        }

        return await App.Current.MainPage.DisplayAlert(title, message, "Sì", "No");
    }
}