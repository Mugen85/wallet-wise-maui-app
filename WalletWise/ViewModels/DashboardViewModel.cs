// in ViewModels/DashboardViewModel.cs
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using WalletWise.Messages;
using WalletWise.Services;
using WalletWise.Views;

namespace WalletWise.ViewModels;

public partial class DashboardViewModel : ObservableObject
{
    private readonly IAccountService _accountService;
    private readonly IMessenger _messenger;

    [ObservableProperty]
    private decimal _totalBalance;

    public DashboardViewModel(IAccountService accountService, IMessenger messenger)
    {
        _accountService = accountService;
        _messenger = messenger;

        // Registra il ViewModel per ricevere il messaggio
        _messenger.Register<TransactionAddedMessage>(this, (r, m) =>
        {
            // Quando arriva un messaggio, esegui il comando per ricaricare i dati
            if (LoadDataCommand.CanExecute(null))
            {
                LoadDataCommand.Execute(null);
            }
        });
    }

    [RelayCommand]
    private async Task LoadDataAsync()
    {
        TotalBalance = await _accountService.GetTotalBalanceAsync();
    }

    [RelayCommand]
    private async Task AddTransactionAsync()
    {
        await Shell.Current.GoToAsync(nameof(AddTransactionPage));
    }
}
