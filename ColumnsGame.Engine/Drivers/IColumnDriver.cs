using ColumnsGame.Engine.Columns;

namespace ColumnsGame.Engine.Drivers
{
    internal interface IColumnDriver : IDriver<Column>
    {
        bool IsColumnInFinalPosition { get; }

        void MoveColumnDown();
    }
}
