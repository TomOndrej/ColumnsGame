using System.Collections.Generic;
using ColumnsGame.Engine.Positions;
using ColumnsGame.Engine.RemovablePatterns;

namespace ColumnsGame.Engine.UnitTests.Tests.RemovablePatterns
{
    internal class SlashRemovablePatternTests : RemovablePatternTestsBase<SlashRemovablePattern>
    {
        protected override List<BrickPosition> PositionsInRow => new List<BrickPosition>
        {
            new BrickPosition {XCoordinate = 0, YCoordinate = 4},
            new BrickPosition {XCoordinate = 1, YCoordinate = 3},
            new BrickPosition {XCoordinate = 2, YCoordinate = 2},
            new BrickPosition {XCoordinate = 3, YCoordinate = 1},
            new BrickPosition {XCoordinate = 4, YCoordinate = 0}
        };
    }
}