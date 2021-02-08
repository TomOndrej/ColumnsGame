using ColumnsGame.Engine.Bricks;
using ColumnsGame.Engine.Columns;
using ColumnsGame.Engine.Drivers;
using ColumnsGame.Engine.Field;
using ColumnsGame.Engine.GameSteps;
using ColumnsGame.Engine.Providers;
using ColumnsGame.Engine.RemovablePatterns;
using ColumnsGame.Engine.Services;
using Unity;

namespace ColumnsGame.Engine.Ioc
{
    internal static class ContainerProvider
    {
        private static IUnityContainer container;

        internal static IUnityContainer Container => container ??= CreateContainerAndRegisterDependencies();

        internal static T Resolve<T>()
        {
            return Container.Resolve<T>();
        }

        private static IUnityContainer CreateContainerAndRegisterDependencies()
        {
            var newContainer = new UnityContainer();

            newContainer.RegisterType(typeof(INextStepProvider), typeof(NextStepProvider));
            newContainer.RegisterType(typeof(IGameStageSwitcher), typeof(GameStageSwitcher));
            newContainer.RegisterType(typeof(IGameFieldFactory), typeof(GameFieldFactory));
            newContainer.RegisterType(typeof(IBrickFactory), typeof(BrickFactory));
            newContainer.RegisterType(typeof(IColumnFactory), typeof(ColumnFactory));
            newContainer.RegisterType(typeof(IRemovablePatternsProvider), typeof(RemovablePatternsProvider));
            newContainer.RegisterType(typeof(ICurrentGameDataProvider), typeof(CurrentGameDataProvider));
            newContainer.RegisterType(typeof(INotificationService), typeof(NotificationService));

            newContainer.RegisterSingleton(typeof(IColumnDriver), typeof(ColumnDriver));
            newContainer.RegisterSingleton(typeof(IFieldDriver), typeof(FieldDriver));
            newContainer.RegisterSingleton(typeof(IGameProvider), typeof(GameProvider));
            newContainer.RegisterSingleton(typeof(ISettingsProvider), typeof(SettingsProvider));

            return newContainer;
        }
    }
}