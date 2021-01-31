using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using ColumnsGame.Engine.Interfaces;

namespace ColumnsGame.Engine
{
    public class Game
    {
        public bool IsRunning { get; private set; }

        private IGameSettings Settings { get; }

        private CancellationTokenSource CancellationTokenSource { get; }

        private CancellationToken CancellationToken => this.CancellationTokenSource.Token;

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

            try
            {
                await Task.Run(async () =>
                {
                    while (!this.CancellationToken.IsCancellationRequested)
                    {
                        Debug.WriteLine("Game is running");

                        await Task.Delay(this.Settings.GameSpeed, this.CancellationToken).ConfigureAwait(false);
                    }

                }, this.CancellationTokenSource.Token).ConfigureAwait(false);
            }
            finally
            {
                this.IsRunning = false;
            }
        }
    }
}
