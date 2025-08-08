// in WalletWise/ViewModels/TransactionsViewModel.cs
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using WalletWise.Persistence.Models;
using WalletWise.Services;
using WalletWise.Views;

namespace WalletWise.ViewModels;

public partial class TransactionsViewModel(ITransactionService transactionService) : ObservableObject
{
    public ObservableCollection<Transaction> Transactions { get; } = [];

    [RelayCommand]
    private async Task LoadTransactionsAsync()
    {
        Transactions.Clear();
        var transactionsList = await transactionService.GetTransactionsAsync();
        foreach (var transaction in transactionsList)
        {
            Transactions.Add(transaction);
        }
    }

    [RelayCommand]
    private async Task GoToAddTransactionAsync()
    {
        await Shell.Current.GoToAsync(nameof(AddTransactionPage));
    }
}
