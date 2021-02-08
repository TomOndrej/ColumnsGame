using System.ComponentModel;
using ColumnsGame.Engine.Constants;
using ColumnsGame.Engine.EventArgs;
using ColumnsGame.Engine.Interfaces;
using ColumnsGame.Game;
using ColumnsGame.Ioc;
using ColumnsGame.Navigation;
using ColumnsGame.Resources;
using ColumnsGame.Services;
using Prism.Commands;
using Prism.Navigation;

namespace ColumnsGame.ViewModels
{
    public class GamePageViewModel : ViewModelBase
    {
        private Engine.Game game;

        public Engine.Game Game
        {
            get => this.game;
            private set
            {
                this.game = value;

                RaisePropertyChanged(nameof(this.Game));
                RaisePropertyChanged(nameof(this.IsPauseContinueButtonEnabled));
                RaisePropertyChanged(nameof(this.PauseContinueButtonText));
                RaisePropertyChanged(nameof(this.IsOverlayVisible));
                RaisePropertyChanged(nameof(this.OverlayText));

                if (this.game != null)
                {
                    this.game.GameFieldChanged += OnGameFieldChanged;
                    this.game.PropertyChanged += OnGamePropertyChanged;
                }
            }
        }

        private int[,] gameFieldMatrix;

        public int[,] GameFieldMatrix
        {
            get => this.gameFieldMatrix;
            set
            {
                if (this.gameFieldMatrix == value)
                {
                    return;
                }

                this.gameFieldMatrix = value;
                RaisePropertyChanged();
            }
        }

        public bool IsOverlayVisible => this.Game != null && (this.Game.IsGameOver || this.Game.IsPaused);

        public string OverlayText
        {
            get
            {
                if (this.Game != null)
                {
                    if (this.Game.IsGameOver)
                    {
                        return Texts.GameOver;
                    }

                    if (this.Game.IsPaused)
                    {
                        return Texts.Pause;
                    }
                }

                return string.Empty;
            }
        }

        public bool IsPauseContinueButtonEnabled => this.Game != null && (this.Game.IsRunning || this.Game.IsPaused);

        public string PauseContinueButtonText => this.Game?.IsPaused == true ? Texts.Continue : Texts.Pause;

        public GamePageViewModel(INavigationService navigationService) : base(navigationService)
        {
        }

        private DelegateCommand<PlayerRequestEnum?> moveColumnCommand;

        public DelegateCommand<PlayerRequestEnum?> MoveColumnCommand => this.moveColumnCommand ??=
            new DelegateCommand<PlayerRequestEnum?>(ExecuteMoveColumnCommand);

        private void ExecuteMoveColumnCommand(PlayerRequestEnum? playerRequest)
        {
            if (!playerRequest.HasValue)
            {
                return;
            }

            this.Game.RequestColumnMove(playerRequest.Value);
        }

        private DelegateCommand pauseContinueCommand;

        public DelegateCommand PauseContinueCommand =>
            this.pauseContinueCommand ??= new DelegateCommand(ExecutePauseContinueCommand);

        private void ExecutePauseContinueCommand()
        {
            if (this.Game.IsRunning)
            {
                this.Game.Pause();
                SaveGame();
            }
            else if (this.Game.IsPaused)
            {
                this.Game.Continue();
            }
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);

            SaveGame();
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            ICurrentGameData currentGameData = null;
            if (parameters.ContainsKey(NavigationParameterNames.GameDataParam))
            {
                currentGameData = (ICurrentGameData) parameters[NavigationParameterNames.GameDataParam];
            }

            var gameSettings = ContainerProvider.Resolve<IDefaultGameSettingsFactory>().Create();
            this.Game = ContainerProvider.Resolve<IGameFactory>().Create(gameSettings, currentGameData);

            this.Game.RequestGameFieldDataNotification();

            if (currentGameData == null)
            {
                this.Game.Start();
            }
        }

        public override void OnSleep()
        {
            base.OnSleep();

            SaveGame();
        }

        public override void Destroy()
        {
            base.Destroy();

            if (this.Game != null)
            {
                this.Game.Stop();

                this.Game.GameFieldChanged -= OnGameFieldChanged;
                this.Game.PropertyChanged -= OnGamePropertyChanged;
            }
        }

        private void SaveGame()
        {
            if (this.Game.IsRunning)
            {
                this.Game.Pause();
            }

            var saveGameService = ContainerProvider.Resolve<ISaveGameService>();

            if (!this.Game.IsGameOver)
            {
                var gameData = saveGameService.CreateEmptyGameData();
                this.Game.FillCurrentGameData(gameData);
                saveGameService.SaveGameData(gameData);
            }
            else
            {
                saveGameService.DeleteGameData();
            }
        }

        private void OnGameFieldChanged(object sender, GameFieldChangedEventArgs e)
        {
            this.GameFieldMatrix = e.NewGameFieldData;
        }

        private void OnGamePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Engine.Game.IsRunning))
            {
                RaisePropertyChanged(nameof(this.IsPauseContinueButtonEnabled));
            }
            else if (e.PropertyName == nameof(Engine.Game.IsGameOver))
            {
                RaisePropertyChanged(nameof(this.OverlayText));
                RaisePropertyChanged(nameof(this.IsOverlayVisible));
            }
            else if (e.PropertyName == nameof(Engine.Game.IsPaused))
            {
                RaisePropertyChanged(nameof(this.PauseContinueButtonText));
                RaisePropertyChanged(nameof(this.IsPauseContinueButtonEnabled));
                RaisePropertyChanged(nameof(this.OverlayText));
                RaisePropertyChanged(nameof(this.IsOverlayVisible));
            }
        }
    }
}