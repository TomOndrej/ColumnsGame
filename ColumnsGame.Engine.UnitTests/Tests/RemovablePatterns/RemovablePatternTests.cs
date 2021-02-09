using System.Collections.Generic;
using System.Linq;
using ColumnsGame.Engine.Bricks;
using ColumnsGame.Engine.Field;
using ColumnsGame.Engine.Positions;
using ColumnsGame.Engine.RemovablePatterns;
using NUnit.Framework;

namespace ColumnsGame.Engine.UnitTests.Tests.RemovablePatterns
{
    [TestFixture]
    internal abstract class RemovablePatternTestsBase<TPattern>
        where TPattern : RemovablePatternBase, new()
    {
        protected abstract List<BrickPosition> PositionsInRow { get; }

        [Test]
        public void PatternMarksThreeSameBricksInRowToDestroy()
        {
            var gameField = new GameField(10, 10)
            {
                {this.PositionsInRow[0], new Brick {BrickKind = 1}},
                {this.PositionsInRow[1], new Brick {BrickKind = 1}},
                {this.PositionsInRow[2], new Brick {BrickKind = 1}}
            };

            var pattern = new TPattern();
            pattern.MarkBricksInPatternToDestroy(gameField);

            foreach (var pair in gameField)
            {
                Assert.IsTrue(pair.Value.Destroy);
            }
        }

        [Test]
        public void PatternMarksFiveSameBricksInRowToDestroy()
        {
            var gameField = new GameField(10, 10)
            {
                {this.PositionsInRow[0], new Brick {BrickKind = 1}},
                {this.PositionsInRow[1], new Brick {BrickKind = 1}},
                {this.PositionsInRow[2], new Brick {BrickKind = 1}},
                {this.PositionsInRow[3], new Brick {BrickKind = 1}},
                {this.PositionsInRow[4], new Brick {BrickKind = 1}}
            };

            var pattern = new TPattern();
            pattern.MarkBricksInPatternToDestroy(gameField);

            foreach (var pair in gameField)
            {
                Assert.IsTrue(pair.Value.Destroy);
            }
        }

        [Test]
        public void PatternDoesNotMarkTwoSameBricksInRowToDestroy()
        {
            var gameField = new GameField(10, 10)
            {
                {this.PositionsInRow[0], new Brick {BrickKind = 1}},
                {this.PositionsInRow[1], new Brick {BrickKind = 1}}
            };

            var pattern = new TPattern();
            pattern.MarkBricksInPatternToDestroy(gameField);

            Assert.IsTrue(gameField.All(pair => !pair.Value.Destroy));
        }

        [Test]
        public void PatternDoesNotMarkDifferentBricksInRowToDestroy()
        {
            var gameField = new GameField(10, 10)
            {
                {this.PositionsInRow[0], new Brick {BrickKind = 1}},
                {this.PositionsInRow[1], new Brick {BrickKind = 1}},
                {this.PositionsInRow[2], new Brick {BrickKind = 2}},
                {this.PositionsInRow[3], new Brick {BrickKind = 1}},
                {this.PositionsInRow[4], new Brick {BrickKind = 1}}
            };

            var pattern = new TPattern();
            pattern.MarkBricksInPatternToDestroy(gameField);

            Assert.IsTrue(gameField.All(pair => !pair.Value.Destroy));
        }

        [Test]
        public void PatternMarksOnlyFirstThreeBricksInRowToDestroy()
        {
            var gameField = new GameField(10, 10)
            {
                {this.PositionsInRow[0], new Brick {BrickKind = 1}},
                {this.PositionsInRow[1], new Brick {BrickKind = 1}},
                {this.PositionsInRow[2], new Brick {BrickKind = 1}},
                {this.PositionsInRow[3], new Brick {BrickKind = 2}}
            };

            var pattern = new TPattern();
            pattern.MarkBricksInPatternToDestroy(gameField);

            Assert.IsTrue(gameField.ElementAt(0).Value.Destroy);
            Assert.IsTrue(gameField.ElementAt(1).Value.Destroy);
            Assert.IsTrue(gameField.ElementAt(2).Value.Destroy);
            Assert.IsFalse(gameField.ElementAt(3).Value.Destroy);
        }

        [Test]
        public void PatternMarksOnlyLastThreeBricksInRowToDestroy()
        {
            var gameField = new GameField(10, 10)
            {
                {this.PositionsInRow[0], new Brick {BrickKind = 2}},
                {this.PositionsInRow[1], new Brick {BrickKind = 1}},
                {this.PositionsInRow[2], new Brick {BrickKind = 1}},
                {this.PositionsInRow[3], new Brick {BrickKind = 1}}
            };

            var pattern = new TPattern();
            pattern.MarkBricksInPatternToDestroy(gameField);

            Assert.IsFalse(gameField.ElementAt(0).Value.Destroy);
            Assert.IsTrue(gameField.ElementAt(1).Value.Destroy);
            Assert.IsTrue(gameField.ElementAt(2).Value.Destroy);
            Assert.IsTrue(gameField.ElementAt(3).Value.Destroy);
        }
    }
}