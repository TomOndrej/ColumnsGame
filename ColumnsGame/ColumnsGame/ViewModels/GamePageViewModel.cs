﻿using ColumnsGame.Engine.Constants;
using ColumnsGame.Engine.EventArgs;
using ColumnsGame.Game;
using ColumnsGame.Ioc;
using Prism.Commands;
using Prism.Navigation;

namespace ColumnsGame.ViewModels
{
    public class GamePageViewModel : ViewModelBase
    {
        private int[,] gameFieldMatrix;

        private DelegateCommand<PlayerRequestEnum?> moveColumnCommand;

        public GamePageViewModel(INavigationService navigationService) : base(navigationService)
        {
        }

        private Engine.Game Game { get; set; }

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

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            var gameSettings = ContainerProvider.Resolve<IDefaultGameSettingsFactory>().Create();
            this.Game = ContainerProvider.Resolve<IGameFactory>().Create(gameSettings);
            this.Game.GameFieldChanged += OnGameFieldChanged;

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

        public override void Destroy()
        {
            base.Destroy();

            if (this.Game != null)
            {
                this.Game.GameFieldChanged -= OnGameFieldChanged;
            }
        }

        private void OnGameFieldChanged(object sender, GameFieldChangedEventArgs e)
        {
            this.GameFieldMatrix = e.NewGameFieldData;
        }
    }
}