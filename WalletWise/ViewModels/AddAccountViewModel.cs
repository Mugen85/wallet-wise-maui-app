using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using WalletWise.Persistence.Models;
using WalletWise.Services;
// Assicurati di avere il namespace corretto per Shell (Microsoft.Maui.Controls) se non è globale

namespace WalletWise.ViewModels;

// 1. IL WRAPPER UI (Unica fonte di verità per il Picker)
public class AccountTypeDisplayModel
{
    public AccountType Value { get; init; }
    public string Name { get; init; } = string.Empty;

    // L'override vitale per bypassare il Trimmer in Release Android
    public override string ToString() => Name;
}

// 2. IL VIEWMODEL
public partial class AddAccountViewModel(IAccountService accountService) : ObservableObject
{
    private readonly IAccountService _accountService = accountService;

    // --- PROPRIETÀ BINDATE ALLA UI ---
    // Il Toolkit genererà in automatico le proprietà pubbliche Name, InitialBalance e SelectedAccountType.
    // In questo modo il tuo XAML non deve cambiare di una virgola.

    [ObservableProperty]
    private string _name = string.Empty;

    [ObservableProperty]
    private AccountTypeDisplayModel? _selectedAccountType;

    [ObservableProperty]
    private string _initialBalanceText = string.Empty;

    public ObservableCollection<AccountTypeDisplayModel> AccountTypes { get; } = [];

    [RelayCommand]
    private void LoadAccountTypes()
    {
        try
        {
            FileLogger.Log("AddAccountViewModel: LoadAccountTypes avviato");

            if (AccountTypes.Count > 0) return;

            var types = Enum.GetValues<AccountType>()
                            .Select(at => new AccountTypeDisplayModel
                            {
                                Value = at,
                                Name = GetEnumDescription(at)
                            });

            foreach (var type in types)
            {
                AccountTypes.Add(type);
            }

            FileLogger.Log("AddAccountViewModel: LoadAccountTypes completato");
        }
        catch (Exception ex)
        {
            FileLogger.Log($"AddAccountViewModel LoadAccountTypes ERRORE: {ex}");
        }
    }

    // --- IL MOMENTO IN CUI LA FRIZIONE ATTACCA ---
    [RelayCommand]
    private async Task SaveAccountAsync()
    {
        try
        {
            FileLogger.Log("AddAccountViewModel: SaveAccount avviato");

            // Validazione base
            if (string.IsNullOrWhiteSpace(Name) || SelectedAccountType is null)
            {
                FileLogger.Log("AddAccountViewModel: validazione fallita - Name o Type nulli");
                return;
            }

            // --- IL PARSING CORAZZATO ---
            decimal parsedBalance = 0m;
            if (!string.IsNullOrWhiteSpace(_initialBalanceText))
            {
                // Normalizziamo le virgole in punti, indipendentemente dalla tastiera usata
                string normalizedInput = _initialBalanceText.Replace(",", ".");

                // Tentiamo il parsing. Se fallisce (es. l'utente ha incollato testo a caso), parsedBalance rimane 0
                decimal.TryParse(normalizedInput, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out parsedBalance);
            }

            // Assemblaggio del Modello per il Database
            var newAccount = new Account
            {
                Name = this.Name.Trim(),
                InitialBalance = parsedBalance, // Passiamo il decimale appena calcolato
                Type = this.SelectedAccountType.Value
            };

            // Salvataggio
            await _accountService.AddAccountAsync(newAccount);
            FileLogger.Log("AddAccountViewModel: account salvato, navigazione back");

            await Shell.Current.GoToAsync("..");
        }
        catch (Exception ex)
        {
            FileLogger.Log($"AddAccountViewModel SaveAccount ERRORE: {ex}");
        }
    }

    // Traduttore statico AOT-Friendly
    private static string GetEnumDescription(AccountType type) => type switch
    {
        AccountType.StipendioSpese => "Stipendio / Spese",
        AccountType.Risparmio => "Risparmio",
        AccountType.Investimento => "Investimento",
        _ => type.ToString()
    };
}