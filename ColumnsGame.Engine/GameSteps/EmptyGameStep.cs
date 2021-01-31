using System.Threading.Tasks;

namespace ColumnsGame.Engine.GameSteps
{
    internal class EmptyGameStep : GameStepBase
    {
        protected override Task ProcessStep()
        {
            return Task.CompletedTask;
        }
    }
}
