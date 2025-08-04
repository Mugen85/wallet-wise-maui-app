using System.Globalization;

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

        System.Diagnostics.Debug.WriteLine($"Percorso DB: {FileSystem.AppDataDirectory}");

        MainPage = new AppShell();
    }
}
