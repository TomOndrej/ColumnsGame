using ColumnsGame.Game;

namespace ColumnsGame.Services
{
    internal interface ISaveGameService
    {
        CurrentGameData CreateEmptyGameData();

        void SaveGameData(CurrentGameData gameData);

        CurrentGameData LoadGameData();

        void DeleteGameData();
    }
}