using System.Threading.Tasks;
using ColumnsGame.Engine.Drivers;
using ColumnsGame.Engine.Ioc;

namespace ColumnsGame.Engine.GameSteps
{
    internal class CleanFieldGameStep : GameStepBase
    {
        protected override Task ProcessStep()
        {
            return ContainerProvider.Resolve<IFieldDriver>().RemoveBrickPatterns();
        }
    }
}