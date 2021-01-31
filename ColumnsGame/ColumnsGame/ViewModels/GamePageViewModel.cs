using ColumnsGame.Game;
using ColumnsGame.Ioc;
using Prism.Navigation;

namespace ColumnsGame.ViewModels
{
    public class GamePageViewModel : ViewModelBase
    {
        public Engine.Game Game { get; private set; }

        public GamePageViewModel(INavigationService navigationService) : base(navigationService)
        { }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            var gameSettings = ContainerProvider.Resolve<IDefaultGameSettingsFactory>().Create();
            this.Game = ContainerProvider.Resolve<IGameFactory>().Create(gameSettings);

            this.Game.Start();
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);

            if (this.Game.IsRunning)
            {
                this.Game.Stop();
            }
        }
    }
}
