using ColumnsGame.Engine.GameSteps;
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

            return newContainer;
        }
    }
}
