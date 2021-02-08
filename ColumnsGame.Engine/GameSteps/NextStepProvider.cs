using System;
using ColumnsGame.Engine.Constants;

namespace ColumnsGame.Engine.GameSteps
{
    internal class NextStepProvider : INextStepProvider
    {
        public IGameStep GetNextGameStep(GameStageEnum gameStage)
        {
            return gameStage switch
            {
                GameStageEnum.CreateColumn => new CreateColumnGameStep(),
                GameStageEnum.FallColumn => new FallColumnGameStep(),
                GameStageEnum.CleanField => new CleanFieldGameStep(),
                GameStageEnum.CheckGameOver => new CheckGameOverGameStep(),

                _ => throw new NotImplementedException($"{nameof(GameStageEnum)}.{gameStage:G} is not implemented.")
            };
        }
    }
}