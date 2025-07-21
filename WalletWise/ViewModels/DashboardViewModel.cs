// ViewModels/DashboardViewModel.cs
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WalletWise.Services;

namespace WalletWise.ViewModels;

public partial class DashboardViewModel(IAccountService accountService) : ObservableObject
{
    private readonly IAccountService _accountService = accountService;

    private decimal _totalBalance;
    public decimal TotalBalance
    {
        get => _totalBalance;
        set => SetProperty(ref _totalBalance, value);
    }

    [RelayCommand]
    private async Task LoadDataAsync()
    {
        TotalBalance = await _accountService.GetTotalBalanceAsync();
    }
}
