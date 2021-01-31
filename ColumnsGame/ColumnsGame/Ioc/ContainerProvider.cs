using Unity;

namespace ColumnsGame.Ioc
{
    public static class ContainerProvider
    {
        private static IUnityContainer container;

        public static IUnityContainer Container => container ??= new UnityContainer();

        public static T Resolve<T>()
        {
            return Container.Resolve<T>();
        }
    }
}
