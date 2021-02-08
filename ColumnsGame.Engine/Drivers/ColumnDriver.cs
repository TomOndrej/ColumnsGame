using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ColumnsGame.Engine.Bricks;
using ColumnsGame.Engine.Columns;
using ColumnsGame.Engine.Constants;
using ColumnsGame.Engine.Ioc;
using ColumnsGame.Engine.Positions;

namespace ColumnsGame.Engine.Drivers
{
    internal class ColumnDriver : DriverBase<Column>, IColumnDriver
    {
        private readonly Dictionary<IBrick, BrickPosition> bricksPositions = new Dictionary<IBrick, BrickPosition>();

        private ConcurrentQueue<PlayerRequestEnum> playerRequests;

        private ConcurrentQueue<PlayerRequestEnum> PlayerRequests =>
            this.playerRequests ??= new ConcurrentQueue<PlayerRequestEnum>();

        private bool IsColumnRotatable { get; set; }

        public bool IsColumnInFinalPosition { get; private set; }

        public override void Drive(Column entityToDrive)
        {
            base.Drive(entityToDrive);

            SetIsColumnRotatable();

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

            this.IsColumnInFinalPosition = false;

            StartListenForPlayerRequests();
        }

        public void DriveRestored(Dictionary<BrickPosition, IBrick> restoredColumn)
        {
            var column = new Column();
            column.AddRange(restoredColumn.Values);

            base.Drive(column);

            SetIsColumnRotatable();

            this.bricksPositions.Clear();

            foreach (var pair in restoredColumn)
            {
                this.bricksPositions.Add(pair.Value, pair.Key);
            }

            this.IsColumnInFinalPosition = false;

            StartListenForPlayerRequests();
        }

        public void EnqueuePlayerRequest(PlayerRequestEnum playerRequest)
        {
            if (this.IsColumnInFinalPosition)
            {
                return;
            }

            this.PlayerRequests.Enqueue(playerRequest);
        }

        public Dictionary<IBrick, BrickPosition> GetColumnState()
        {
            if (this.IsColumnInFinalPosition)
            {
                return null;
            }

            return this.bricksPositions;
        }

        public void MoveColumnDown()
        {
            var success = MoveColumn(brickPosition => brickPosition.IncrementYCoordinate());

            if (!success)
            {
                this.IsColumnInFinalPosition = true;
            }
        }

        private void MoveColumnLeft()
        {
            MoveColumn(brickPosition => brickPosition.DecrementXCoordinate());
        }

        private void MoveColumnRight()
        {
            MoveColumn(brickPosition => brickPosition.IncrementXCoordinate());
        }

        private bool MoveColumn(Func<BrickPosition, BrickPosition> changeBrickPositionFunc)
        {
            if (this.IsColumnInFinalPosition)
            {
                return false;
            }

            var requestedBrickPositions = this.bricksPositions.Select(brick =>
                new KeyValuePair<IBrick, BrickPosition>(brick.Key, changeBrickPositionFunc(brick.Value))).ToList();

            var brickPositionsToFieldPush = requestedBrickPositions
                .Where(pair => !pair.Value.IsOutsideField(this.Settings))
                .OrderByDescending(pair => pair.Value.YCoordinate).ToList();

            if (!brickPositionsToFieldPush.Any())
            {
                return false;
            }

            var moveResult = ContainerProvider.Resolve<IFieldDriver>().TryMoveBricks(brickPositionsToFieldPush);

            if (!moveResult.Success)
            {
                return false;
            }

            this.bricksPositions.Clear();

            requestedBrickPositions.ForEach(requestedBrickPosition =>
                this.bricksPositions.Add(requestedBrickPosition.Key, requestedBrickPosition.Value));

            return true;
        }

        private void RotateColumn()
        {
            if (!this.IsColumnRotatable)
            {
                return;
            }

            if (this.IsColumnInFinalPosition)
            {
                return;
            }

            var firstBrick = this.DrivenEntity.First();
            var lastBrick = this.DrivenEntity.Last();

            var kindOfFirstBrick = firstBrick.BrickKind;
            var kindOfLastBrick = lastBrick.BrickKind;

            firstBrick.BrickKind = kindOfLastBrick;
            lastBrick.BrickKind = kindOfFirstBrick;

            ContainerProvider.Resolve<IFieldDriver>().ChangeKindOfBricks();
        }

        private void StartListenForPlayerRequests()
        {
            this.PlayerRequests.Clear();

            Task.Run(async () =>
            {
                try
                {
                    while (!this.IsColumnInFinalPosition)
                    {
                        if (this.PlayerRequests.TryDequeue(out var playerRequest))
                        {
                            ProcessPlayerRequest(playerRequest);
                        }
                        else
                        {
                            await Task.Delay(10).ConfigureAwait(false);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            });
        }

        private void ProcessPlayerRequest(PlayerRequestEnum playerRequest)
        {
            switch (playerRequest)
            {
                case PlayerRequestEnum.Left:
                    MoveColumnLeft();
                    break;
                case PlayerRequestEnum.Right:
                    MoveColumnRight();
                    break;
                case PlayerRequestEnum.Rotate:
                    RotateColumn();
                    break;

                default:
                    throw new NotImplementedException(
                        $"{nameof(PlayerRequestEnum)}.{playerRequest:G} is not implemented.");
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

        private void SetIsColumnRotatable()
        {
            this.IsColumnRotatable = this.DrivenEntity.First().BrickKind != this.DrivenEntity.Last().BrickKind;
        }
    }
}