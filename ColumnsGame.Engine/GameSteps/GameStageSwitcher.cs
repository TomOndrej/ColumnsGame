using System;

namespace ColumnsGame.Engine.GameSteps
{
    internal class GameStageSwitcher : IGameStageSwitcher
    {
        public GameStageEnum SwitchStage(GameStageEnum currentGameStage)
        {
            return currentGameStage switch
            {
                GameStageEnum.CreateColumn => GameStageEnum.FallColumn,
                GameStageEnum.FallColumn => GameStageEnum.FallColumn,

                _ => throw new NotImplementedException(
                    $"{nameof(GameStageEnum)}.{currentGameStage:G} is not implemented.")
            };
        }
    }
}
