using System.Collections.Generic;
using ColumnsGame.Engine.Bricks;
using ColumnsGame.Engine.Field;
using ColumnsGame.Engine.Positions;

namespace ColumnsGame.Engine.Drivers
{
    internal interface IFieldDriver : IDriver<GameField>
    {
        MoveResult TryMoveBricksDown(List<KeyValuePair<IBrick, BrickPosition>> bricks);
    }
}
