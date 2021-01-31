using System.Threading.Tasks;
using ColumnsGame.Engine.Interfaces;

namespace ColumnsGame.Engine.GameSteps
{
    internal class EmptyGameStep : GameStepBase
    {
        public EmptyGameStep(IGameSettings gameSettings) : base(gameSettings)
        { }

        protected override Task ProcessStep()
        {
            return Task.CompletedTask;
        }
    }
}
