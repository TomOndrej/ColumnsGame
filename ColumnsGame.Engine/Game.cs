using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using ColumnsGame.Engine.Constants;
using ColumnsGame.Engine.Drivers;
using ColumnsGame.Engine.EventArgs;
using ColumnsGame.Engine.Field;
using ColumnsGame.Engine.GameSteps;
using ColumnsGame.Engine.Interfaces;
using ColumnsGame.Engine.Ioc;
using ColumnsGame.Engine.Providers;
using ColumnsGame.Engine.Services;

namespace ColumnsGame.Engine
{
    public class Game : INotifyPropertyChanged
    {
        private GameStageEnum gameStage;

        private bool isRunning;

        public bool IsRunning
        {
            get => this.isRunning;
            private set
            {
                if (this.isRunning == value)
                {
                    return;
                }

                this.isRunning = value;
                OnPropertyChanged(nameof(this.IsRunning));
            }
        }

        private bool isPaused;

        public bool IsPaused
        {
            get => this.isPaused;
            private set
            {
                if (this.isPaused == value)
                {
                    return;
                }

                this.isPaused = value;
                OnPropertyChanged(nameof(this.IsPaused));
            }
        }


        private bool isGameOver;

        public bool IsGameOver
        {
            get => this.isGameOver;
            private set
            {
                if (this.isGameOver == value)
                {
                    return;
                }

                this.isGameOver = value;
                OnPropertyChanged(nameof(this.IsGameOver));
            }
        }

        private IGameSettings Settings { get; set; }

        private CancellationTokenSource CancellationTokenSource { get; set; }

        private CancellationToken CancellationToken => this.CancellationTokenSource.Token;

        private INextStepProvider nextStepProvider;

        private INextStepProvider NextStepProvider =>
            this.nextStepProvider ??= ContainerProvider.Resolve<INextStepProvider>();

        private IGameStageSwitcher gameStageSwitcher;

        private IGameStageSwitcher GameStageSwitcher =>
            this.gameStageSwitcher ??= ContainerProvider.Resolve<IGameStageSwitcher>();

        private GameField GameField { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public event EventHandler<GameFieldChangedEventArgs> GameFieldChanged;

        public void Initialize(IGameSettings gameSettings, ICurrentGameData currentGameData = null)
        {
            this.Settings = gameSettings;
            ContainerProvider.Resolve<ISettingsProvider>().SetSettingsInstance(this.Settings);
            ContainerProvider.Resolve<IGameProvider>().SetGameInstance(this);

            if (currentGameData == null)
            {
                InitializeNewGame();
            }
            else
            {
                InitializeRestoredGame(currentGameData);
            }

            ContainerProvider.Resolve<IFieldDriver>().Drive(this.GameField);
        }

        public void Start()
        {
            if (this.IsRunning)
            {
                throw new InvalidOperationException("Game is already running.");
            }

            if (this.IsPaused)
            {
                throw new InvalidOperationException("Game is paused. Call Continue instead.");
            }

            if (this.IsGameOver)
            {
                throw new InvalidOperationException("Game is over.");
            }

#pragma warning disable 4014
            Run();
#pragma warning restore 4014
        }

        public void Stop()
        {
            this.IsRunning = false;
            this.CancellationTokenSource?.Cancel();
        }

        public void Pause()
        {
            if (!this.IsRunning)
            {
                throw new InvalidOperationException("Game is not running.");
            }

            this.CancellationTokenSource.Cancel();

            this.IsPaused = true;
        }

        public void Continue()
        {
            if (!this.IsPaused)
            {
                throw new InvalidOperationException("Game is not paused.");
            }

            this.IsPaused = false;

#pragma warning disable 4014
            Run();
#pragma warning restore 4014
        }

        public void RequestColumnMove(PlayerRequestEnum playerRequest)
        {
            if (!this.IsRunning)
            {
                return;
            }

            ContainerProvider.Resolve<IColumnDriver>().EnqueuePlayerRequest(playerRequest);
        }

        public void RequestGameFieldDataNotification()
        {
            ContainerProvider.Resolve<INotificationService>().CreateAndNotifyNewGameFieldData();
        }

        internal void RaiseGameFieldChanged(GameFieldChangedEventArgs gameFieldChangedEventArgs)
        {
            this.GameFieldChanged?.Invoke(this, gameFieldChangedEventArgs);
        }

        internal void GameOver()
        {
            this.IsGameOver = true;
            Stop();
        }

        public void FillCurrentGameData(ICurrentGameData currentGameData)
        {
            ContainerProvider.Resolve<ICurrentGameDataProvider>().FillCurrentGameData(currentGameData);
        }

        private void InitializeNewGame()
        {
            this.GameField = ContainerProvider.Resolve<IGameFieldFactory>().CreateEmptyField();
            this.gameStage = GameStageEnum.CreateColumn;
        }

        private void InitializeRestoredGame(ICurrentGameData currentGameData)
        {
            this.IsPaused = true;

            var currentGameDataProvider = ContainerProvider.Resolve<ICurrentGameDataProvider>();

            currentGameDataProvider.RestoreGameFieldAndColumnFromGameData(
                currentGameData,
                out var restoredGameField,
                out var restoredColumn);

            this.GameField = restoredGameField;

            if (restoredColumn == null)
            {
                this.gameStage = GameStageEnum.CleanField;
            }
            else
            {
                this.gameStage = GameStageEnum.FallColumn;

                ContainerProvider.Resolve<IColumnDriver>().DriveRestored(restoredColumn);
            }
        }

        private async Task Run()
        {
            InitializeRun();

            try
            {
                await Task.Run(async () =>
                {
                    while (!this.CancellationToken.IsCancellationRequested)
                    {
                        await ExecuteNextGameStep().ConfigureAwait(false);
                        this.gameStage = this.GameStageSwitcher.SwitchStage(this.gameStage);
                    }
                }, this.CancellationTokenSource.Token).ConfigureAwait(false);
            }
            catch (TaskCanceledException)
            {
                // ignore TaskCanceledException
            }
            catch (Exception e)
            {
                ResolveException(e);
            }
            finally
            {
                FinalizeRun();
            }
        }

        private void InitializeRun()
        {
            this.IsRunning = true;
            this.CancellationTokenSource = new CancellationTokenSource();
        }

        private void FinalizeRun()
        {
            this.IsRunning = false;
        }

        private Task ExecuteNextGameStep()
        {
            var stepToExecute = this.NextStepProvider.GetNextGameStep(this.gameStage);
            return stepToExecute.ExecuteStep();
        }

        private void ResolveException(Exception e)
        {
            Debug.WriteLine(e);
            //TODO: implement settable exception handler
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}