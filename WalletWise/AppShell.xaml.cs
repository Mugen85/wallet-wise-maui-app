using WalletWise.Views;

namespace WalletWise
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            // Registriamo la rotta per la pagina di aggiunta conto
            Routing.RegisterRoute(nameof(AddAccountPage), typeof(AddAccountPage));
        }
    }
}
