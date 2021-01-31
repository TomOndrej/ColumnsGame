using ColumnsGame.UnitTests.Mocks;
using NUnit.Framework;

namespace ColumnsGame.UnitTests.Tests
{
    internal abstract class TestsWithInitializedApp
    {
        protected TestApp App { get; set; }

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Xamarin.Forms.Mocks.MockForms.Init();
            this.App = new TestApp(new TestPlatformInitializer());
        }
    }
}
