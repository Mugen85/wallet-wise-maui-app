// in WalletWise/Views/BudgetPage.xaml.cs
using WalletWise.ViewModels;

namespace WalletWise.Views;

public partial class BudgetPage : ContentPage
{
    public BudgetPage(BudgetViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is BudgetViewModel vm)
        {
            vm.LoadBudgetsCommand.Execute(null);
        }
    }
}
