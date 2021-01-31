using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Prism.Navigation;

namespace ColumnsGame.UnitTests.Mocks
{
    internal class SpyNavigationService : INavigationService
    {
        public List<string> NavigateAsyncCalls = new List<string>();

        private INavigationResult GetDummyNavigationResult()
        {
            return new DummyNavigationResult();
        }

        public Task<INavigationResult> GoBackAsync()
        {
            throw new NotImplementedException();
        }

        public Task<INavigationResult> GoBackAsync(INavigationParameters parameters)
        {
            throw new NotImplementedException();
        }

        public Task<INavigationResult> GoBackAsync(INavigationParameters parameters, bool? useModalNavigation, bool animated)
        {
            throw new NotImplementedException();
        }

        public Task<INavigationResult> GoBackToRootAsync(INavigationParameters parameters)
        {
            throw new NotImplementedException();
        }

        public Task<INavigationResult> NavigateAsync(Uri uri)
        {
            throw new NotImplementedException();
        }

        public Task<INavigationResult> NavigateAsync(Uri uri, INavigationParameters parameters)
        {
            throw new NotImplementedException();
        }

        public Task<INavigationResult> NavigateAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<INavigationResult> NavigateAsync(string name, INavigationParameters parameters)
        {
            throw new NotImplementedException();
        }

        public Task<INavigationResult> NavigateAsync(string name, INavigationParameters parameters, bool? useModalNavigation, bool animated)
        {
            this.NavigateAsyncCalls.Add(name);

            return Task.FromResult(GetDummyNavigationResult());
        }

        public Task<INavigationResult> NavigateAsync(Uri uri, INavigationParameters parameters, bool? useModalNavigation, bool animated)
        {
            throw new NotImplementedException();
        }
    }
}
