using System;

namespace ColumnsGame.Engine.GameSteps
{
    internal class NextStepProvider : INextStepProvider
    {
        public IGameStep GetNextGameStep(GameStageEnum gameStage)
        {
            return gameStage switch
            {
                GameStageEnum.Empty => new EmptyGameStep(),
                GameStageEnum.CreateColumn => new CreateColumnGameStep(),
                GameStageEnum.FallColumn => new FallColumnGameStep(),

                _ => throw new NotImplementedException($"{nameof(GameStageEnum)}.{gameStage:G} is not implemented.")
            };
        }
    }
}
