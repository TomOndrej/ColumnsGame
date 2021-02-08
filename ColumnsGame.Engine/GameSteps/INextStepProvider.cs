using ColumnsGame.Engine.Constants;

namespace ColumnsGame.Engine.GameSteps
{
    internal interface INextStepProvider
    {
        IGameStep GetNextGameStep(GameStageEnum gameStage);
    }
}