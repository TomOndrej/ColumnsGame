using System.Threading.Tasks;
using ColumnsGame.ViewModels;
using ColumnsGame.Views;
using NUnit.Framework;
using Prism.Navigation;

namespace ColumnsGame.UnitTests.Tests.PageTests
{
    internal class GamePageTests : BasicPageTests<GamePage, GamePageViewModel>
    {
        [Test]
        public void GameInstanceIsCreatedAfterNavigatedTo()
        {
            var viewModel = new GamePageViewModel(null);

            viewModel.OnNavigatedTo(new NavigationParameters());

            Assert.IsNotNull(viewModel.Game);
        }

        [Test]
        public void GameIsRunningAfterNavigatedTo()
        {
            var viewModel = new GamePageViewModel(null);

            viewModel.OnNavigatedTo(new NavigationParameters());

            Assert.IsTrue(viewModel.Game.IsRunning);
        }

        [Test]
        public async Task GameIsNotRunningAfterNavigatedFrom()
        {
            var viewModel = new GamePageViewModel(null);

            viewModel.OnNavigatedTo(new NavigationParameters());
            viewModel.OnNavigatedFrom(null);

            await Task.Delay(600);

            Assert.IsFalse(viewModel.Game.IsRunning);
        }
    }
}