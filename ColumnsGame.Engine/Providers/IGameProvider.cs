namespace ColumnsGame.Engine.Providers
{
    internal interface IGameProvider
    {
        Game GetGameInstance();

        void SetGameInstance(Game game);
    }
}