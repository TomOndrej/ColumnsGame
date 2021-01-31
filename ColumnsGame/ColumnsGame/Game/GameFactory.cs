using ColumnsGame.Engine.Interfaces;
using ColumnsGame.Ioc.Attributes;

namespace ColumnsGame.Game
{
    [IocRegisterImplementation]
    public class GameFactory : IGameFactory
    {
        public Engine.Game Create(IGameSettings gameSettings)
        {
            return new Engine.Game(gameSettings);
        }
    }
}
