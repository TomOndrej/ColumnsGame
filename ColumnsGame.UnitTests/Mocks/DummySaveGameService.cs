using ColumnsGame.Game;
using ColumnsGame.Services;

namespace ColumnsGame.UnitTests.Mocks
{
    internal class DummySaveGameService : ISaveGameService
    {
        public CurrentGameData CreateEmptyGameData()
        {
            return new CurrentGameData();
        }

        public void SaveGameData(CurrentGameData gameData)
        {
        }

        public CurrentGameData LoadGameData()
        {
            return null;
        }

        public void DeleteGameData()
        {
        }
    }
}