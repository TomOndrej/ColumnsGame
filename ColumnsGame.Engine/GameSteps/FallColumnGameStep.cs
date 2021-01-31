using System.Threading.Tasks;
using ColumnsGame.Engine.Interfaces;

namespace ColumnsGame.Engine.GameSteps
{
    internal class FallColumnGameStep : GameStepBase
    {
        public FallColumnGameStep(IGameSettings gameSettings) : base(gameSettings)
        { }

        protected override Task ProcessStep()
        {
            return Task.CompletedTask;
        }
    }
}
