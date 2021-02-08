using ColumnsGame.Game;
using ColumnsGame.Ioc;
using ColumnsGame.Navigation;
using ColumnsGame.Services;
using ColumnsGame.Views;
using Prism.Commands;
using Prism.Navigation;

namespace ColumnsGame.ViewModels
{
    public class MenuPageViewModel : ViewModelBase
    {
        private CurrentGameData currentGameData;

        private CurrentGameData CurrentGameData
        {
            get => this.currentGameData;
            set
            {
                this.currentGameData = value;
                RaisePropertyChanged(nameof(this.CurrentGameData));
            }
        }

        public MenuPageViewModel(INavigationService navigationService) : base(navigationService)
        {
        }

        private DelegateCommand startGameCommand;

        public DelegateCommand StartGameCommand =>
            this.startGameCommand ??= new DelegateCommand(ExecuteStartGameCommand);

        private async void ExecuteStartGameCommand()
        {
            ContainerProvider.Resolve<ISaveGameService>().DeleteGameData();
            this.CurrentGameData = null;

            await this.NavigationService.NavigateToAsync(nameof(GamePage));
        }

        private DelegateCommand continueGameCommand;

        public DelegateCommand ContinueGameCommand => this.continueGameCommand ??=
            new DelegateCommand(ExecuteContinueGameCommand, CanExecuteContinueGameCommand)
                .ObservesProperty(() => this.CurrentGameData);

        private async void ExecuteContinueGameCommand()
        {
            await this.NavigationService.NavigateToAsync(nameof(GamePage),
                new NavigationParameters {{NavigationParameterNames.GameDataParam, this.CurrentGameData}});
        }

        private bool CanExecuteContinueGameCommand()
        {
            return this.CurrentGameData != null;
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            this.CurrentGameData = ContainerProvider.Resolve<ISaveGameService>().LoadGameData();
        }
    }
}