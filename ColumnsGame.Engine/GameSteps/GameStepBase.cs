using System.Diagnostics;
using System.Threading.Tasks;

namespace ColumnsGame.Engine.GameSteps
{
    internal abstract class GameStepBase : IGameStep
    {
        protected abstract Task ProcessStep();

        public async Task ExecuteStep()
        {
            Debug.WriteLine($"{GetType().Name} execution started.");

            await ProcessStep().ConfigureAwait(false);

            Debug.WriteLine($"{GetType().Name} execution finished.");
        }
    }
}
