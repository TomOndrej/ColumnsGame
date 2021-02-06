using ColumnsGame.Engine.Columns;
using ColumnsGame.Engine.Constants;

namespace ColumnsGame.Engine.Drivers
{
    internal interface IColumnDriver : IDriver<Column>
    {
        bool IsColumnInFinalPosition { get; }

        void MoveColumnDown();

        void EnqueuePlayerRequest(PlayerRequestEnum playerRequest);
    }
}