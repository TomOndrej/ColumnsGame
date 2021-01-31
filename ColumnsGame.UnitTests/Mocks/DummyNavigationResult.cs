using System;
using Prism.Navigation;

namespace ColumnsGame.UnitTests.Mocks
{
    internal class DummyNavigationResult : INavigationResult
    {
        public bool Success { get; } = true;
        public Exception Exception { get; }
    }
}
