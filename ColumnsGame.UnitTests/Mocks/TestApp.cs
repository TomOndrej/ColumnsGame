using Prism;
using Prism.Navigation;

namespace ColumnsGame.UnitTests.Mocks
{
    internal class TestApp : AppBase
    {
        public INavigationService NavService => this.NavigationService;

        internal TestApp(IPlatformInitializer platformInitializer) : base(platformInitializer)
        { }
    }
}
