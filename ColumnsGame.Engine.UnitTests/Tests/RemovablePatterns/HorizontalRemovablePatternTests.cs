using System.Collections.Generic;
using ColumnsGame.Engine.Positions;
using ColumnsGame.Engine.RemovablePatterns;

namespace ColumnsGame.Engine.UnitTests.Tests.RemovablePatterns
{
    internal class HorizontalRemovablePatternTests : RemovablePatternTestsBase<HorizontalRemovablePattern>
    {
        protected override List<BrickPosition> PositionsInRow => new List<BrickPosition>
        {
            new BrickPosition {XCoordinate = 0, YCoordinate = 0},
            new BrickPosition {XCoordinate = 1, YCoordinate = 0},
            new BrickPosition {XCoordinate = 2, YCoordinate = 0},
            new BrickPosition {XCoordinate = 3, YCoordinate = 0},
            new BrickPosition {XCoordinate = 4, YCoordinate = 0}
        };
    }
}