namespace WalletWise.Services;

public interface IAlertService
{
    Task<bool> ShowConfirmationAsync(string title, string message);
}

