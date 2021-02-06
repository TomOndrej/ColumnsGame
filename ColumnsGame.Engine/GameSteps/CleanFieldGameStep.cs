using System.Threading.Tasks;
using ColumnsGame.Engine.Drivers;
using ColumnsGame.Engine.Interfaces;
using ColumnsGame.Engine.Ioc;

namespace ColumnsGame.Engine.GameSteps
{
    internal class CleanFieldGameStep : GameStepBase
    {
        public CleanFieldGameStep(IGameSettings gameSettings) : base(gameSettings)
        {
        }

        protected override Task ProcessStep()
        {
            return ContainerProvider.Resolve<IFieldDriver>().RemoveBrickPatterns();
        }
    }
}