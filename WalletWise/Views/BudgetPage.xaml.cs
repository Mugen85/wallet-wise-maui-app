using WalletWise.ViewModels;

namespace WalletWise.Views;

public partial class BudgetPage : ContentPage
{
    private readonly BudgetViewModel _viewModel;

    public BudgetPage(BudgetViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        _viewModel = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (_viewModel.LoadBudgetsCommand.CanExecute(null))
        {
            _viewModel.LoadBudgetsCommand.Execute(null);
        }
    }
}
