using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using ColumnsGame.Engine.GameSteps;
using ColumnsGame.Engine.Interfaces;
using ColumnsGame.Engine.Ioc;

namespace ColumnsGame.Engine
{
    public class Game
    {
        public bool IsRunning { get; private set; }

        private IGameSettings Settings { get; }

        private CancellationTokenSource CancellationTokenSource { get; }

        private CancellationToken CancellationToken => this.CancellationTokenSource.Token;

        private GameStageEnum gameStage;

        private INextStepProvider nextStepProvider;

        private INextStepProvider NextStepProvider =>
            this.nextStepProvider ??= ContainerProvider.Resolve<INextStepProvider>();

        private IGameStageSwitcher gameStageSwitcher;

        private IGameStageSwitcher GameStageSwitcher =>
            this.gameStageSwitcher ??= ContainerProvider.Resolve<IGameStageSwitcher>();

        public Game(IGameSettings gameSettings)
        {
            this.Settings = gameSettings;
            this.CancellationTokenSource = new CancellationTokenSource();
        }

        public void Start()
        {
#pragma warning disable 4014
            Run();
#pragma warning restore 4014
        }

        public void Stop()
        {
            this.CancellationTokenSource?.Cancel();
        }

        private async Task Run()
        {
            this.IsRunning = true;
            this.gameStage = GameStageEnum.CreateColumn;

            try
            {
                await Task.Run(async () =>
                {
                    while (!this.CancellationToken.IsCancellationRequested)
                    {
                        await ExecuteNextGameStep().ConfigureAwait(false);
                        this.gameStage = this.GameStageSwitcher.SwitchStage(this.gameStage);

                        await Task.Delay(this.Settings.GameSpeed, this.CancellationToken).ConfigureAwait(false);
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
                this.IsRunning = false;
            }
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
    }
}
