// ViewModels/DashboardViewModel.cs
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using WalletWise.Services;
using WalletWise.Graphics;

namespace WalletWise.ViewModels;

public class ChartLegendItem
{
    public string Name { get; set; } = string.Empty;
    public decimal Value { get; set; }
    public string Percentage { get; set; } = string.Empty;
    public Color Color { get; set; } = Colors.Transparent;
}

public partial class DashboardViewModel(IAccountService accountService) : ObservableObject
{
    private readonly IAccountService _accountService = accountService;

    private decimal _totalBalance;
    public decimal TotalBalance { get => _totalBalance; set => SetProperty(ref _totalBalance, value); }

    // --- INIZIO MODIFICA: Proprietà semplice e Action per l'aggiornamento ---
    public PieChartDrawable PieChart { get; } = new();
    public Action? InvalidateChartRequest;
    // --- FINE MODIFICA ---

    public ObservableCollection<ChartLegendItem> LegendItems { get; } = [];

    [RelayCommand]
    private async Task LoadDataAsync()
    {
        TotalBalance = await _accountService.GetTotalBalanceAsync();
        var accounts = await _accountService.GetAccountsAsync();

        LegendItems.Clear();
        var chartItems = new List<ChartLegendItem>();
        var colors = new[] { "#266489", "#68B9C0", "#90D585", "#F3C151", "#F37F64", "#424856", "#8F97A4" };
        int colorIndex = 0;

        foreach (var account in accounts)
        {
            if (account.InitialBalance > 0)
            {
                var percentage = TotalBalance > 0 ? (account.InitialBalance / TotalBalance) : 0;
                var legendItem = new ChartLegendItem
                {
                    Name = account.Name ?? string.Empty,
                    Value = account.InitialBalance,
                    Percentage = percentage.ToString("P1"),
                    Color = Color.FromArgb(colors[colorIndex])
                };
                chartItems.Add(legendItem);
                LegendItems.Add(legendItem);
                colorIndex = (colorIndex + 1) % colors.Length;
            }
        }

        PieChart.Items = chartItems;

        // --- INIZIO MODIFICA: Invia il segnale di "ridisegna!" ---
        InvalidateChartRequest?.Invoke();
        // --- FINE MODIFICA ---
    }
}
