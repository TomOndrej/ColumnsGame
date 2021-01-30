using Prism;
using Prism.Ioc;

namespace ColumnsGame.Droid
{
    internal class AndroidPlatformInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Nothing specific to register on Android yet.
        }
    }
}