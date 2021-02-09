using ColumnsGame.Services;
using Prism;
using Prism.Ioc;

namespace ColumnsGame.UnitTests.Mocks
{
    internal class TestPlatformInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register(typeof(ISaveGameService), typeof(DummySaveGameService));
        }
    }
}