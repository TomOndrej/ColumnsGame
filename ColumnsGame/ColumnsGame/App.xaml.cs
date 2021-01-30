using Prism.Ioc;

namespace ColumnsGame
{
    public partial class App
    {
        public App()
        {
            InitializeComponent();
        }

        protected override void OnStart()
        { }

        protected override void OnSleep()
        { }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<MainPage>();
        }

        protected override async void OnInitialized()
        {
            await this.NavigationService.NavigateAsync(nameof(MainPage));
        }

        protected override void OnResume()
        { }
    }
}
