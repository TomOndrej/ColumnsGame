using ColumnsGame.Engine.Ioc;
using ColumnsGame.Engine.Providers;

namespace ColumnsGame.Engine.Field
{
    internal class GameFieldFactory : IGameFieldFactory
    {
        public GameField CreateEmptyField()
        {
            var settings = ContainerProvider.Resolve<ISettingsProvider>().GetSettingsInstance();

            return new GameField(settings.FieldWidth, settings.FieldHeight);
        }
    }
}