using System.Diagnostics;
using System.Threading.Tasks;
using ColumnsGame.Engine.Interfaces;

namespace ColumnsGame.Engine.GameSteps
{
    internal abstract class GameStepBase : IGameStep
    {
        protected IGameSettings GameSettings { get; }

        protected GameStepBase(IGameSettings gameSettings)
        {
            this.GameSettings = gameSettings;
        }

        protected abstract Task ProcessStep();

        public async Task ExecuteStep()
        {
            Debug.WriteLine($"{GetType().Name} execution started.");

            await ProcessStep().ConfigureAwait(false);

            Debug.WriteLine($"{GetType().Name} execution finished.");
        }
    }
}
