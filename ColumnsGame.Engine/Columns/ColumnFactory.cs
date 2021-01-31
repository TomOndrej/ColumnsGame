using ColumnsGame.Engine.Bricks;
using ColumnsGame.Engine.Interfaces;
using ColumnsGame.Engine.Ioc;

namespace ColumnsGame.Engine.Columns
{
    internal class ColumnFactory : IColumnFactory
    {
        public Column CreateColumn(IGameSettings gameSettings)
        {
            var brickFactory = ContainerProvider.Resolve<IBrickFactory>();

            var newColumn = new Column();

            for (int i = 0; i < gameSettings.ColumnLength; i++)
            {
                newColumn.Add(brickFactory.CreateBrick(gameSettings));
            }

            return newColumn;
        }
    }
}
