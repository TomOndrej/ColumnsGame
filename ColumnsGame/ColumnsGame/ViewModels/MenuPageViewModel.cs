using ColumnsGame.Navigation;
using ColumnsGame.Views;
using Prism.Commands;
using Prism.Navigation;

namespace ColumnsGame.ViewModels
{
    public class MenuPageViewModel : ViewModelBase
    {
        public MenuPageViewModel(INavigationService navigationService) : base(navigationService)
        { }

        private DelegateCommand startGameCommand;

        public DelegateCommand StartGameCommand => 
            this.startGameCommand ??= new DelegateCommand(ExecuteStartGameCommand, CanExecuteStartGameCommand);

        private async void ExecuteStartGameCommand()
        {
            await this.NavigationService.NavigateToAsync(nameof(GamePage));
        }

        private bool CanExecuteStartGameCommand()
        {
            return true;
        }
    }
}