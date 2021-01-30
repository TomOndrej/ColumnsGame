using ColumnsGame.Navigation;
using ColumnsGame.Views;
using Prism.Navigation;

namespace ColumnsGame.ViewModels
{
    public class MainNavigationPageViewModel : ViewModelBase
    {
        public MainNavigationPageViewModel(INavigationService navigationService) : base(navigationService)
        { }
    }
}
