using System.Collections.Generic;

namespace ColumnsGame.Engine.RemovablePatterns
{
    internal class RemovablePatternsProvider : IRemovablePatternsProvider
    {
        public IEnumerable<IRemovablePattern> GetRemovablePatterns()
        {
            yield return new HorizontalRemovablePattern();
            yield return new BackslashRemovablePattern();
            yield return new VerticalRemovablePattern();
            yield return new SlashRemovablePattern();
        }
    }
}