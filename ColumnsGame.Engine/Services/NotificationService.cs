using ColumnsGame.Engine.Drivers;
using ColumnsGame.Engine.EventArgs;
using ColumnsGame.Engine.Ioc;
using ColumnsGame.Engine.Providers;

namespace ColumnsGame.Engine.Services
{
    internal class NotificationService : INotificationService
    {
        public void CreateAndNotifyNewGameFieldData()
        {
            var newGameFieldData = CreateEmptyGameFieldData();

            FillGameFieldData(newGameFieldData);

            ContainerProvider.Resolve<IGameProvider>().GetGameInstance()
                ?.RaiseGameFieldChanged(new GameFieldChangedEventArgs(newGameFieldData));
        }

        private int[,] CreateEmptyGameFieldData()
        {
            var settings = ContainerProvider.Resolve<ISettingsProvider>().GetSettingsInstance();

            var newGameFieldData = new int[settings.FieldWidth, settings.FieldHeight];

            for (var i = 0; i < settings.FieldWidth; i++)
            {
                for (var j = 0; j < settings.FieldHeight; j++)
                {
                    newGameFieldData[i, j] = -1;
                }
            }

            return newGameFieldData;
        }

        private void FillGameFieldData(int[,] newGameFieldData)
        {
            var gameField = ContainerProvider.Resolve<IFieldDriver>().DrivenEntity;

            foreach (var pair in gameField)
            {
                newGameFieldData[pair.Key.XCoordinate, pair.Key.YCoordinate] = pair.Value.BrickKind;
            }
        }
    }
}