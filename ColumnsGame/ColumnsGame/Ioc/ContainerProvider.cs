using Unity;

namespace ColumnsGame.Ioc
{
    internal static class ContainerProvider
    {
        private static IUnityContainer container;

        internal static IUnityContainer Container => container ??= new UnityContainer();
    }
}
