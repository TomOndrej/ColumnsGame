using System.Collections.Generic;
using ColumnsGame.Engine.Bricks;
using ColumnsGame.Engine.Columns;
using ColumnsGame.Engine.Constants;
using ColumnsGame.Engine.Positions;

namespace ColumnsGame.Engine.Drivers
{
    internal interface IColumnDriver : IDriver<Column>
    {
        bool IsColumnInFinalPosition { get; }

        void MoveColumnDown();

        void EnqueuePlayerRequest(PlayerRequestEnum playerRequest);

        Dictionary<BrickPosition, IBrick> GetColumnState();

        void DriveRestored(Dictionary<BrickPosition, IBrick> restoredColumn);
    }
}