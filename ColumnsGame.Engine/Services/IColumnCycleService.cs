using ColumnsGame.Engine.Columns;

namespace ColumnsGame.Engine.Services
{
    internal interface IColumnCycleService
    {
        void CycleBricksInColumn(Column column);
    }
}