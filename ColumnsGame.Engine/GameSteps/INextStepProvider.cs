using ColumnsGame.Engine.Interfaces;

namespace ColumnsGame.Engine.GameSteps
{
    internal interface INextStepProvider
    {
        IGameStep GetNextGameStep(GameStageEnum gameStage, IGameSettings gameSettings);
    }
}
