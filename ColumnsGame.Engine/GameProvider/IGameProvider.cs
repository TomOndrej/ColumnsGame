namespace ColumnsGame.Engine.GameProvider
{
    internal interface IGameProvider
    {
        Game GetGameInstance();

        void SetGameInstance(Game game);
    }
}