using System;
using ColumnsGame.Engine.Constants;
using ColumnsGame.Engine.Interfaces;

namespace ColumnsGame.Engine.GameSteps
{
    internal class NextStepProvider : INextStepProvider
    {
        public IGameStep GetNextGameStep(GameStageEnum gameStage, IGameSettings gameSettings)
        {
            return gameStage switch
            {
                GameStageEnum.CreateColumn => new CreateColumnGameStep(gameSettings),
                GameStageEnum.FallColumn => new FallColumnGameStep(gameSettings),
                GameStageEnum.CleanField => new CleanFieldGameStep(gameSettings),
                GameStageEnum.CheckGameOver => new CheckGameOverGameStep(gameSettings),

                _ => throw new NotImplementedException($"{nameof(GameStageEnum)}.{gameStage:G} is not implemented.")
            };
        }
    }
}