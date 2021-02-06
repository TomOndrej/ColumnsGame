using System.Collections.Generic;
using System.Threading.Tasks;
using ColumnsGame.Engine.Bricks;
using ColumnsGame.Engine.Field;
using ColumnsGame.Engine.Positions;

namespace ColumnsGame.Engine.Drivers
{
    internal interface IFieldDriver : IDriver<GameField>
    {
        MoveResult TryMoveBricks(List<KeyValuePair<IBrick, BrickPosition>> bricks);

        void ChangeKindOfBricks();

        Task RemoveBrickPatterns();
    }
}