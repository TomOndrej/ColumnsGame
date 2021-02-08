using System.Threading.Tasks;
using ColumnsGame.Engine.Drivers;
using ColumnsGame.Engine.Ioc;

namespace ColumnsGame.Engine.GameSteps
{
    internal class CheckGameOverGameStep : GameStepBase
    {
        protected override Task ProcessStep()
        {
            ContainerProvider.Resolve<IFieldDriver>().StopGameIfGameIsOver();

            return Task.CompletedTask;
        }
    }
}