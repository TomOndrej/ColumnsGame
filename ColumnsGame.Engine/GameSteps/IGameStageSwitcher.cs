using ColumnsGame.Engine.Constants;

namespace ColumnsGame.Engine.GameSteps
{
    internal interface IGameStageSwitcher
    {
        GameStageEnum SwitchStage(GameStageEnum currentGameStage);
    }
}