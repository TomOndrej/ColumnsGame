using System.Threading.Tasks;
using ColumnsGame.Engine.Drivers;
using ColumnsGame.Engine.Ioc;
using ColumnsGame.Engine.Providers;

namespace ColumnsGame.Engine.GameSteps
{
    internal class FallColumnGameStep : GameStepBase
    {
        protected override async Task ProcessStep()
        {
            ContainerProvider.Resolve<IColumnDriver>().MoveColumnDown();

            var settings = ContainerProvider.Resolve<ISettingsProvider>().GetSettingsInstance();

            await Task.Delay(settings.GameSpeed).ConfigureAwait(false);
        }
    }
}