using System.Collections.Generic;
using ColumnsGame.Engine.Bricks;
using ColumnsGame.Engine.Columns;
using ColumnsGame.Engine.Ioc;
using ColumnsGame.Engine.Services;
using NUnit.Framework;

namespace ColumnsGame.Engine.UnitTests.Tests.ColumnCycleService
{
    [TestFixture]
    internal class ColumnCycleServiceTests
    {
        [Test]
        [TestCaseSource(nameof(GetTestedColumns))]
        public void ColumnHasCorrectBrickKindOrderAfterCycle(Column testedColumn, Column expectedColumn)
        {
            var service = ContainerProvider.Resolve<IColumnCycleService>();

            service.CycleBricksInColumn(testedColumn);

            for (var i = 0; i < expectedColumn.Count; i++)
            {
                Assert.AreEqual(expectedColumn[i].BrickKind, testedColumn[i].BrickKind);
            }
        }

        private static IEnumerable<TestCaseData> GetTestedColumns()
        {
            yield return new TestCaseData(
                new Column
                {
                    new Brick {BrickKind = 1},
                    new Brick {BrickKind = 2},
                    new Brick {BrickKind = 3}
                },
                new Column
                {
                    new Brick {BrickKind = 3},
                    new Brick {BrickKind = 1},
                    new Brick {BrickKind = 2}
                }).SetName($"{nameof(ColumnHasCorrectBrickKindOrderAfterCycle)}_1");

            yield return new TestCaseData(
                new Column
                {
                    new Brick {BrickKind = 1},
                    new Brick {BrickKind = 2},
                    new Brick {BrickKind = 1}
                },
                new Column
                {
                    new Brick {BrickKind = 1},
                    new Brick {BrickKind = 1},
                    new Brick {BrickKind = 2}
                }).SetName($"{nameof(ColumnHasCorrectBrickKindOrderAfterCycle)}_2");

            yield return new TestCaseData(
                new Column
                {
                    new Brick {BrickKind = 1},
                    new Brick {BrickKind = 1},
                    new Brick {BrickKind = 2}
                },
                new Column
                {
                    new Brick {BrickKind = 2},
                    new Brick {BrickKind = 1},
                    new Brick {BrickKind = 1}
                }).SetName($"{nameof(ColumnHasCorrectBrickKindOrderAfterCycle)}_3");

            yield return new TestCaseData(
                new Column
                {
                    new Brick {BrickKind = 1},
                    new Brick {BrickKind = 1},
                    new Brick {BrickKind = 1}
                },
                new Column
                {
                    new Brick {BrickKind = 1},
                    new Brick {BrickKind = 1},
                    new Brick {BrickKind = 1}
                }).SetName($"{nameof(ColumnHasCorrectBrickKindOrderAfterCycle)}_4");

            yield return new TestCaseData(
                new Column
                {
                    new Brick {BrickKind = 1},
                    new Brick {BrickKind = 2},
                    new Brick {BrickKind = 3},
                    new Brick {BrickKind = 4}
                },
                new Column
                {
                    new Brick {BrickKind = 4},
                    new Brick {BrickKind = 1},
                    new Brick {BrickKind = 2},
                    new Brick {BrickKind = 3}
                }).SetName($"{nameof(ColumnHasCorrectBrickKindOrderAfterCycle)}_5");

            yield return new TestCaseData(
                new Column
                {
                    new Brick {BrickKind = 1},
                    new Brick {BrickKind = 2},
                    new Brick {BrickKind = 3},
                    new Brick {BrickKind = 3}
                },
                new Column
                {
                    new Brick {BrickKind = 3},
                    new Brick {BrickKind = 1},
                    new Brick {BrickKind = 2},
                    new Brick {BrickKind = 3}
                }).SetName($"{nameof(ColumnHasCorrectBrickKindOrderAfterCycle)}_6");

            yield return new TestCaseData(
                new Column
                {
                    new Brick {BrickKind = 1},
                    new Brick {BrickKind = 2},
                    new Brick {BrickKind = 1},
                    new Brick {BrickKind = 2}
                },
                new Column
                {
                    new Brick {BrickKind = 2},
                    new Brick {BrickKind = 1},
                    new Brick {BrickKind = 2},
                    new Brick {BrickKind = 1}
                }).SetName($"{nameof(ColumnHasCorrectBrickKindOrderAfterCycle)}_7");
        }
    }
}