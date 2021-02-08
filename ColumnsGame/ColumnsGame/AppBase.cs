using ColumnsGame.Ioc;
using ColumnsGame.Navigation;
using ColumnsGame.Views;
using Prism;
using Prism.Ioc;
using Prism.Unity;

namespace ColumnsGame
{
    public abstract class AppBase : PrismApplication
    {
        protected AppBase(IPlatformInitializer platformInitializer) : base(platformInitializer)
        {
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterNavigations(typeof(App).Assembly);
        }

        protected override async void OnInitialized()
        {
            await this.NavigationService.NavigateToAsync($"/{nameof(MainNavigationPage)}/{nameof(MenuPage)}");
        }

        protected override IContainerExtension CreateContainerExtension()
        {
            return new UnityContainerExtension(ContainerProvider.Container);
        }

        protected override void Initialize()
        {
            ContainerProvider.Container.RegisterDependencies(typeof(App).Assembly);

            base.Initialize();
        }
    }
}