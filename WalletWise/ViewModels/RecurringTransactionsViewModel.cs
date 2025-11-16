// in WalletWise/ViewModels/RecurringTransactionsViewModel.cs
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using WalletWise.Persistence.Models;
using WalletWise.Views;

namespace WalletWise.ViewModels;

/// <summary>
/// Questo è il "motore" per l'hub del Pilota Automatico.
/// È una centralina pulita e focalizzata sulla sostanza.
/// </summary>
public partial class RecurringTransactionsViewModel(IRecurringTransactionService recurringService) : ObservableObject
{
    private readonly IRecurringTransactionService _recurringService = recurringService;

    /// <summary>
    /// Questo è il nostro "sensore" a prova di bug.
    /// Il code-behind si "abbonerà" a questa proprietà.
    /// Quando la sostituiamo, la UI si aggiornerà "a mano".
    /// </summary>
    [ObservableProperty]
    private ObservableCollection<RecurringTransaction> _recurringItems = [];

    /// <summary>
    /// Comando solido per caricare i dati dal nostro "motore" Service.
    /// </summary>
    [RelayCommand]
    private async Task LoadDataAsync()
    {
        // Chiamiamo il motore che abbiamo già costruito e collaudato.
        var items = await _recurringService.GetRecurringTransactionsAsync();

        // Sostituiamo l'intera lista.
        // Questo è il costrutto solido per scatenare l'aggiornamento
        // della nostra UI "a mano".
        RecurringItems = new ObservableCollection<RecurringTransaction>(items);
    }

    /// <summary>
    /// Comando solido per navigare alla pagina di aggiunta.
    /// </summary>
    [RelayCommand]
    private static async Task GoToAddAsync()
    {
        // Usiamo la "mappa GPS" solida (nameof()) che
        // registreremo in un pezzo successivo.
        await Shell.Current.GoToAsync(nameof(AddRecurringTransactionPage));
    }
}