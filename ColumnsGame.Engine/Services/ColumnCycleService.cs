using System.Linq;
using ColumnsGame.Engine.Columns;

namespace ColumnsGame.Engine.Services
{
    internal class ColumnCycleService : IColumnCycleService
    {
        public void CycleBricksInColumn(Column column)
        {
            if (!CanCycleColumn(column))
            {
                return;
            }

            CycleColumn(column);
        }

        private bool CanCycleColumn(Column column)
        {
            return column.Select(brick => brick.BrickKind).Distinct().Count() > 1;
        }

        private void CycleColumn(Column column)
        {
            var kindToSet = column.Last().BrickKind;

            foreach (var brick in column)
            {
                var tempKind = brick.BrickKind;
                brick.BrickKind = kindToSet;
                kindToSet = tempKind;
            }
        }
    }
}