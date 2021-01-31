using ColumnsGame.Engine.Interfaces;

namespace ColumnsGame.Engine.Field
{
    internal interface IGameFieldFactory
    {
        GameField CreateEmptyField(IGameSettings gameSettings);
    }
}
