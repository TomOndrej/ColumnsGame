using System.Threading.Tasks;

namespace ColumnsGame.Engine.GameSteps
{
    internal abstract class GameStepBase : IGameStep
    {
        protected abstract Task ProcessStep();

        public Task ExecuteStep()
        {
            return ProcessStep();
        }
    }
}