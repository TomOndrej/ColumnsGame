using ColumnsGame.Engine.Columns;
using ColumnsGame.Engine.Interfaces;

namespace ColumnsGame.Engine.Drivers
{
    internal interface IColumnDriver
    {
        void Initialize(IGameSettings settings);

        void DriveColumn(Column column);
    }
}
