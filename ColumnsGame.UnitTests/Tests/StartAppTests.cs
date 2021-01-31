using ColumnsGame.Views;
using NUnit.Framework;

namespace ColumnsGame.UnitTests.Tests
{
    [TestFixture]
    class StartAppTests : TestsWithInitializedApp
    {
        [Test]
        public void MainNavigationPageIsMainPageAfterAppStart()
        {
            Assert.IsInstanceOf<MainNavigationPage>(this.App.MainPage);
        }

        [Test]
        public void MenuPageIsCurrentPageAfterAppStart()
        {
            var navigationPage = (MainNavigationPage) this.App.MainPage;

            Assert.IsInstanceOf<MenuPage>(navigationPage.CurrentPage);
        }
    }
}
