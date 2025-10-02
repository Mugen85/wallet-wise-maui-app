// in ViewModels/DashboardViewModel.cs
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WalletWise.Services;

namespace WalletWise.ViewModels;

public partial class DashboardViewModel(IAccountService accountService) : ObservableObject
{
    private readonly IAccountService _accountService = accountService;

    [ObservableProperty]
    private decimal _totalBalance;

    [RelayCommand]
    private async Task LoadDataAsync()
    {
        TotalBalance = await _accountService.GetTotalBalanceAsync();
    }
    
}
