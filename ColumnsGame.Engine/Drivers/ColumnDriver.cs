using System.Collections.Generic;
using System.Linq;
using ColumnsGame.Engine.Bricks;
using ColumnsGame.Engine.Columns;
using ColumnsGame.Engine.Ioc;
using ColumnsGame.Engine.Positions;

namespace ColumnsGame.Engine.Drivers
{
    internal class ColumnDriver : DriverBase<Column>, IColumnDriver
    {
        private readonly Dictionary<IBrick, BrickPosition> bricksPositions = new Dictionary<IBrick, BrickPosition>();
        public bool IsColumnInFinalPosition { get; private set; }

        public override void Drive(Column entityToDrive)
        {
            base.Drive(entityToDrive);

            this.IsColumnInFinalPosition = false;

            this.bricksPositions.Clear();

            for (var i = 0; i < this.DrivenEntity.Count; i++)
            {
                this.bricksPositions.Add(
                    this.DrivenEntity[i],
                    new BrickPosition
                    {
                        XCoordinate = GetXCoordinateOfNewlyDrivenColumn(),
                        YCoordinate = GetYCoordinateOfNewlyDrivenColumn(i)
                    });
            }
        }

        public void MoveColumnDown()
        {
            if (this.IsColumnInFinalPosition)
            {
                return;
            }

            var requestedBrickPositions = this.bricksPositions.Select(brick =>
                new KeyValuePair<IBrick, BrickPosition>(brick.Key, brick.Value.IncrementYCoordinate())).ToList();

            var brickPositionsToFieldPush = requestedBrickPositions.Where(pair => pair.Value.YCoordinate >= 0)
                .OrderByDescending(pair => pair.Value.YCoordinate).ToList();

            var moveResult = ContainerProvider.Resolve<IFieldDriver>().TryMoveBricksDown(brickPositionsToFieldPush);

            if (moveResult.Success)
            {
                this.bricksPositions.Clear();

                requestedBrickPositions.ForEach(requestedBrickPosition =>
                    this.bricksPositions.Add(requestedBrickPosition.Key, requestedBrickPosition.Value));
            }
            else
            {
                this.IsColumnInFinalPosition = true;
            }
        }

        private int GetXCoordinateOfNewlyDrivenColumn()
        {
            return this.Settings.FieldWidth / 2;
        }

        private int GetYCoordinateOfNewlyDrivenColumn(int brickIndex)
        {
            return -(this.DrivenEntity.Count - brickIndex);
        }
    }
}