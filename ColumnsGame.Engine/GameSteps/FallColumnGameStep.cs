using System.Threading.Tasks;
using ColumnsGame.Engine.Drivers;
using ColumnsGame.Engine.Interfaces;
using ColumnsGame.Engine.Ioc;

namespace ColumnsGame.Engine.GameSteps
{
    internal class FallColumnGameStep : GameStepBase
    {
        public FallColumnGameStep(IGameSettings gameSettings) : base(gameSettings)
        {
        }

        protected override async Task ProcessStep()
        {
            ContainerProvider.Resolve<IColumnDriver>().MoveColumnDown();

            await Task.Delay(this.GameSettings.GameSpeed).ConfigureAwait(false);
        }
    }
}