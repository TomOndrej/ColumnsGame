using System.Threading.Tasks;
using ColumnsGame.Engine.Columns;
using ColumnsGame.Engine.Drivers;
using ColumnsGame.Engine.Ioc;

namespace ColumnsGame.Engine.GameSteps
{
    internal class CreateColumnGameStep : GameStepBase
    {
        protected override Task ProcessStep()
        {
            var newColumn = ContainerProvider.Resolve<IColumnFactory>().CreateColumn();
            ContainerProvider.Resolve<IColumnDriver>().Drive(newColumn);

            return Task.CompletedTask;
        }
    }
}