using System;
using ColumnsGame.Engine.Interfaces;

namespace ColumnsGame.Engine.GameSteps
{
    internal class NextStepProvider : INextStepProvider
    {
        public IGameStep GetNextGameStep(GameStageEnum gameStage, IGameSettings gameSettings)
        {
            return gameStage switch
            {
                GameStageEnum.Empty => new EmptyGameStep(gameSettings),
                GameStageEnum.CreateColumn => new CreateColumnGameStep(gameSettings),
                GameStageEnum.FallColumn => new FallColumnGameStep(gameSettings),

                _ => throw new NotImplementedException($"{nameof(GameStageEnum)}.{gameStage:G} is not implemented.")
            };
        }
    }
}
