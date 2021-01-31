﻿using ColumnsGame.Ioc;
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
        { }

        protected override void OnStart()
        { }

        protected override void OnSleep()
        { }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterPagesForNavigation();
        }

        protected override async void OnInitialized()
        {
            await this.NavigationService.NavigateToAsync($"/{nameof(MainNavigationPage)}/{nameof(MenuPage)}");
        }

        protected override void OnResume()
        { }

        protected override IContainerExtension CreateContainerExtension()
        {
            return new UnityContainerExtension(ContainerProvider.Container);
        }
    }
}
