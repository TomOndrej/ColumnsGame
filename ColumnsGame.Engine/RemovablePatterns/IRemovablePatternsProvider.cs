using System.Collections.Generic;

namespace ColumnsGame.Engine.RemovablePatterns
{
    internal interface IRemovablePatternsProvider
    {
        IEnumerable<IRemovablePattern> GetRemovablePatterns();
    }
}