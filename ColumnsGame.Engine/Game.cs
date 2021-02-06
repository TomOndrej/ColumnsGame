using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using ColumnsGame.Engine.Constants;
using ColumnsGame.Engine.Drivers;
using ColumnsGame.Engine.EventArgs;
using ColumnsGame.Engine.Field;
using ColumnsGame.Engine.GameProvider;
using ColumnsGame.Engine.GameSteps;
using ColumnsGame.Engine.Interfaces;
using ColumnsGame.Engine.Ioc;

namespace ColumnsGame.Engine
{
    public class Game : INotifyPropertyChanged
    {
        private GameField gameField;

        private GameStageEnum gameStage;

        private IGameStageSwitcher gameStageSwitcher;

        private bool isGameOver;

        private INextStepProvider nextStepProvider;

        public bool IsRunning { get; private set; }

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

        private INextStepProvider NextStepProvider =>
            this.nextStepProvider ??= ContainerProvider.Resolve<INextStepProvider>();

        private IGameStageSwitcher GameStageSwitcher =>
            this.gameStageSwitcher ??= ContainerProvider.Resolve<IGameStageSwitcher>();

        private GameField GameField =>
            this.gameField ??= ContainerProvider.Resolve<IGameFieldFactory>().CreateEmptyField(this.Settings);

        public event PropertyChangedEventHandler PropertyChanged;

        public event EventHandler<GameFieldChangedEventArgs> GameFieldChanged;

        public void Initialize(IGameSettings gameSettings)
        {
            this.Settings = gameSettings;
            this.CancellationTokenSource = new CancellationTokenSource();

            ContainerProvider.Resolve<IGameProvider>().SetGameInstance(this);
            ContainerProvider.Resolve<IColumnDriver>().Initialize(this.Settings);
            ContainerProvider.Resolve<IFieldDriver>().Initialize(this.Settings);
        }

        public void Start()
        {
            if (this.IsRunning)
            {
                throw new InvalidOperationException("Game is already running.");
            }

            if (this.IsGameOver)
            {
                throw new InvalidOperationException("Game is over.");
            }

#pragma warning disable 4014
            Run();
#pragma warning restore 4014
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

        public void Stop()
        {
            this.IsRunning = false;
            this.CancellationTokenSource?.Cancel();
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

        public void RequestColumnMove(PlayerRequestEnum playerRequest)
        {
            if (!this.IsRunning)
            {
                return;
            }

            ContainerProvider.Resolve<IColumnDriver>().EnqueuePlayerRequest(playerRequest);
        }

        private void InitializeRun()
        {
            this.IsRunning = true;
            this.gameStage = GameStageEnum.CreateColumn;
            ContainerProvider.Resolve<IFieldDriver>().Drive(this.GameField);
        }

        private void FinalizeRun()
        {
            this.IsRunning = false;
        }

        private Task ExecuteNextGameStep()
        {
            var stepToExecute = this.NextStepProvider.GetNextGameStep(this.gameStage, this.Settings);
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