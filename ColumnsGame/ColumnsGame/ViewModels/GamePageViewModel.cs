using System.ComponentModel;
using ColumnsGame.Engine.Constants;
using ColumnsGame.Engine.EventArgs;
using ColumnsGame.Game;
using ColumnsGame.Ioc;
using ColumnsGame.Resources;
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
            }
            else if (this.Game.IsPaused)
            {
                this.Game.Continue();
            }
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);

            if (this.Game.IsRunning)
            {
                this.Game.Stop();
            }
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            var gameSettings = ContainerProvider.Resolve<IDefaultGameSettingsFactory>().Create();
            this.Game = ContainerProvider.Resolve<IGameFactory>().Create(gameSettings);

            this.Game.Start();
        }

        public override void Destroy()
        {
            base.Destroy();

            if (this.Game != null)
            {
                this.Game.GameFieldChanged -= OnGameFieldChanged;
                this.Game.PropertyChanged -= OnGamePropertyChanged;
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