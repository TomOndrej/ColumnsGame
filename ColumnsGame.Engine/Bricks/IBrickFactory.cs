using ColumnsGame.Engine.Interfaces;

namespace ColumnsGame.Engine.Bricks
{
    internal interface IBrickFactory
    {
        IBrick CreateBrick(IGameSettings gameSettings);
    }
}
