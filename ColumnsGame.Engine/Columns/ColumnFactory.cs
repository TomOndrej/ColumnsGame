using ColumnsGame.Engine.Bricks;
using ColumnsGame.Engine.Ioc;
using ColumnsGame.Engine.Providers;

namespace ColumnsGame.Engine.Columns
{
    internal class ColumnFactory : IColumnFactory
    {
        public Column CreateColumn()
        {
            var brickFactory = ContainerProvider.Resolve<IBrickFactory>();

            var newColumn = new Column();

            var settings = ContainerProvider.Resolve<ISettingsProvider>().GetSettingsInstance();

            for (var i = 0; i < settings.ColumnLength; i++)
            {
                newColumn.Add(brickFactory.CreateBrick());
            }

            return newColumn;
        }
    }
}