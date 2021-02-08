using System.ComponentModel;
using ColumnsGame.Converters.JsonConverters;
using ColumnsGame.Game;
using ColumnsGame.Ioc.Attributes;
using Newtonsoft.Json;
using Xamarin.Essentials;

namespace ColumnsGame.Services
{
    [IocRegisterImplementation]
    internal class SaveGameService : ISaveGameService
    {
        private const string SavedGameKey = "SavedGame";

        public CurrentGameData CreateEmptyGameData()
        {
            return new CurrentGameData();
        }

        public void SaveGameData(CurrentGameData gameData)
        {
            if (gameData.IsEmpty)
            {
                return;
            }

            var serializedGameData = JsonConvert.SerializeObject(gameData);
            Preferences.Set(SavedGameKey, serializedGameData);
        }

        public CurrentGameData LoadGameData()
        {
            if (!Preferences.ContainsKey(SavedGameKey))
            {
                return null;
            }

            var serializedGameData = Preferences.Get(SavedGameKey, null);

            TypeDescriptor.AddAttributes(typeof((int, int)),
                new TypeConverterAttribute(typeof(TupleConverter)));

            return JsonConvert.DeserializeObject<CurrentGameData>(serializedGameData);
        }

        public void DeleteGameData()
        {
            if (!Preferences.ContainsKey(SavedGameKey))
            {
                return;
            }

            Preferences.Remove(SavedGameKey);
        }
    }
}