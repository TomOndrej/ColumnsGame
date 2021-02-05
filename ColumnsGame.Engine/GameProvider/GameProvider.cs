using System;

namespace ColumnsGame.Engine.GameProvider
{
    internal class GameProvider : IGameProvider
    {
        private WeakReference<Game> gameInstance;

        public Game GetGameInstance()
        {
            this.gameInstance.TryGetTarget(out var game);

            return game;
        }

        public void SetGameInstance(Game game)
        {
            if (this.gameInstance == null)
            {
                this.gameInstance = new WeakReference<Game>(game);
            }
            else
            {
                this.gameInstance.SetTarget(game);
            }
        }
    }
}