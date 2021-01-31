using ColumnsGame.Engine.Interfaces;

namespace ColumnsGame.Game
{
    public interface IGameFactory
    {
        Engine.Game Create(IGameSettings gameSettings);
    }
}
