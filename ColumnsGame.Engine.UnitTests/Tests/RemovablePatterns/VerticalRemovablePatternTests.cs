using System.Collections.Generic;
using ColumnsGame.Engine.Positions;
using ColumnsGame.Engine.RemovablePatterns;

namespace ColumnsGame.Engine.UnitTests.Tests.RemovablePatterns
{
    internal class VerticalRemovablePatternTests : RemovablePatternTestsBase<VerticalRemovablePattern>
    {
        protected override List<BrickPosition> PositionsInRow => new List<BrickPosition>
        {
            new BrickPosition {XCoordinate = 0, YCoordinate = 0},
            new BrickPosition {XCoordinate = 0, YCoordinate = 1},
            new BrickPosition {XCoordinate = 0, YCoordinate = 2},
            new BrickPosition {XCoordinate = 0, YCoordinate = 3},
            new BrickPosition {XCoordinate = 0, YCoordinate = 4}
        };
    }
}