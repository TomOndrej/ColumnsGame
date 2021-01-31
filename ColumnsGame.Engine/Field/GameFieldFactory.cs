using ColumnsGame.Engine.Interfaces;

namespace ColumnsGame.Engine.Field
{
    internal class GameFieldFactory : IGameFieldFactory
    {
        public GameField CreateEmptyField(IGameSettings gameSettings)
        {
            return new GameField(gameSettings.FieldWidth, gameSettings.FieldHeight);
        }
    }
}
