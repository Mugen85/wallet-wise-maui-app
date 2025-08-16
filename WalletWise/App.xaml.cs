using System.Globalization;
using WalletWise.Views;

namespace WalletWise;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        // --- INIZIO MODIFICA: Forziamo la cultura italiana ---
        var culture = new CultureInfo("it-IT");
        Thread.CurrentThread.CurrentCulture = culture;
        Thread.CurrentThread.CurrentUICulture = culture;

        // --- FINE MODIFICA ---

        // routing per la registrazione delle rotte
        Routing.RegisterRoute(nameof(AddBudgetPage), typeof(Views.AddBudgetPage));

        //Per debugging, mostra il percorso del database
        //System.Diagnostics.Debug.WriteLine($"Percorso DB: {FileSystem.AppDataDirectory}");

        MainPage = new AppShell();
    }
}
