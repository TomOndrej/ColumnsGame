using System;
using ColumnsGame.Engine.Constants;
using ColumnsGame.Engine.Drivers;
using ColumnsGame.Engine.Ioc;

namespace ColumnsGame.Engine.GameSteps
{
    internal class GameStageSwitcher : IGameStageSwitcher
    {
        public GameStageEnum SwitchStage(GameStageEnum currentGameStage)
        {
            return currentGameStage switch
            {
                GameStageEnum.CreateColumn => GameStageEnum.FallColumn,
                GameStageEnum.FallColumn =>
                    ContainerProvider.Resolve<IColumnDriver>().IsColumnInFinalPosition
                        ? GameStageEnum.CleanField
                        : GameStageEnum.FallColumn,
                GameStageEnum.CleanField => GameStageEnum.CreateColumn,

                _ => throw new NotImplementedException(
                    $"{nameof(GameStageEnum)}.{currentGameStage:G} is not implemented.")
            };
        }
    }
}