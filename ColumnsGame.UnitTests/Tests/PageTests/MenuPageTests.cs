using ColumnsGame.UnitTests.Mocks;
using ColumnsGame.ViewModels;
using ColumnsGame.Views;
using NUnit.Framework;

namespace ColumnsGame.UnitTests.Tests.PageTests
{
    class MenuPageTests : BasicPageTests<MenuPage, MenuPageViewModel>
    {
        [Test]
        public void NavigationToGameOccursAfterStartCommandExecution()
        {
            var navigationService = new SpyNavigationService();
            var viewModel = new MenuPageViewModel(navigationService);
            
            viewModel.StartGameCommand.Execute();

            Assert.AreEqual(1, navigationService.NavigateAsyncCalls.Count);
            Assert.AreEqual(nameof(GamePage), navigationService.NavigateAsyncCalls[0]);
        }
    }
}
