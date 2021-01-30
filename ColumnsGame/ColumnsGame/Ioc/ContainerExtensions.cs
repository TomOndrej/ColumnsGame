using ColumnsGame.Views;
using Prism.Ioc;

namespace ColumnsGame.Ioc
{
    public static class ContainerExtensions
    {
        public static void RegisterPagesForNavigation(this IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<MainNavigationPage>();
            containerRegistry.RegisterForNavigation<MenuPage>();
        }
    }
}
