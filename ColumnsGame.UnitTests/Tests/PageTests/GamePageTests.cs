using System.Threading.Tasks;
using ColumnsGame.ViewModels;
using ColumnsGame.Views;
using NUnit.Framework;

namespace ColumnsGame.UnitTests.Tests.PageTests
{
    class GamePageTests : BasicPageTests<GamePage, GamePageViewModel>
    {
        [Test]
        public void GameInstanceIsCreatedAfterNavigatedTo()
        {
            var viewModel = new GamePageViewModel(null);
            
            viewModel.OnNavigatedTo(null);

            Assert.IsNotNull(viewModel.Game);
        }

        [Test]
        public void GameIsRunningAfterNavigatedTo()
        {
            var viewModel = new GamePageViewModel(null);
            
            viewModel.OnNavigatedTo(null);

            Assert.IsTrue(viewModel.Game.IsRunning);
        }

        [Test]
        public async Task GameIsNotRunningAfterNavigatedFrom()
        {
            var viewModel = new GamePageViewModel(null);
            
            viewModel.OnNavigatedTo(null);
            viewModel.OnNavigatedFrom(null);

            await Task.Delay(600);

            Assert.IsFalse(viewModel.Game.IsRunning);
        }
    }
}
