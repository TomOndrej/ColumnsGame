using System;
using System.Threading.Tasks;
using ColumnsGame.UnitTests.Mocks;
using ColumnsGame.ViewModels;
using NUnit.Framework;
using Xamarin.Forms;

// ReSharper disable ObjectCreationAsStatement

namespace ColumnsGame.UnitTests.Tests.PageTests
{
    abstract class BasicPageTests<TPage, TPageViewModel> : TestsWithInitializedApp
        where TPage : Page, new()
        where TPageViewModel : ViewModelBase
    {
        [Test]
        public void PageInstanceIsCreated()
        {
            Assert.DoesNotThrow(() =>
            {
                new TPage();
            });
        }

        [Test]
        public void PageViewModelInstanceIsCreated()
        {
            var navigationService = new DummyNavigationService();

            Assert.DoesNotThrow(() =>
            {
                Activator.CreateInstance(typeof(TPageViewModel), navigationService);
            });
        }

        [Test]
        public void PageHasCorrectViewModelAfterInitialization()
        {
            var page = new TPage();

            Assert.IsInstanceOf<TPageViewModel>(page.BindingContext);
        }

        [Test]
        public async Task PageIsRegisteredForNavigation()
        {
            var navigationResult = await this.App.NavService.NavigateAsync($"/{typeof(TPage).Name}");

            Assert.IsTrue(navigationResult.Success);
        } 
    }
}
