using System.Threading.Tasks;

namespace ColumnsGame.Engine.GameSteps
{
    internal interface IGameStep
    {
        Task ExecuteStep();
    }
}
