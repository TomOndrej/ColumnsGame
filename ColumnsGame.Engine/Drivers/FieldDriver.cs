using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ColumnsGame.Engine.Bricks;
using ColumnsGame.Engine.EventArgs;
using ColumnsGame.Engine.Field;
using ColumnsGame.Engine.Ioc;
using ColumnsGame.Engine.Positions;
using ColumnsGame.Engine.Providers;
using ColumnsGame.Engine.RemovablePatterns;

namespace ColumnsGame.Engine.Drivers
{
    internal class FieldDriver : DriverBase<GameField>, IFieldDriver
    {
        public MoveResult TryMoveBricks(List<KeyValuePair<IBrick, BrickPosition>> bricks)
        {
            if (bricks == null || !bricks.Any())
            {
                throw new ArgumentException("Must be non-empty list", nameof(bricks));
            }

            if (IsRequestedBrickPositionOccupiedByAnotherBrick(bricks[0]))
            {
                return new MoveResult(false);
            }

            if (bricks[0].Value.IsOutsideField(this.Settings))
            {
                return new MoveResult(false);
            }

            foreach (var brick in bricks)
            {
                RemoveBrickFromOldPosition(brick);
                SetBrickToNewPosition(brick);
            }

            CreateAndNotifyNewGameFieldData();

            return new MoveResult(true);
        }

        public void ChangeKindOfBricks()
        {
            CreateAndNotifyNewGameFieldData();
        }

        public async Task RemoveBrickPatterns()
        {
            var removablePatterns =
                ContainerProvider.Resolve<IRemovablePatternsProvider>().GetRemovablePatterns().ToList();

            while (true)
            {
                foreach (var removablePattern in removablePatterns)
                {
                    removablePattern.MarkBricksInPatternToDestroy(this.DrivenEntity);
                }

                var positionsToRemove =
                    GetPositionsWithBricksMarkedToDestroy();

                if (!positionsToRemove.Any())
                {
                    return;
                }

                RemoveBricksFromPositions(positionsToRemove);

                CreateAndNotifyNewGameFieldData();

                await Task.Delay(500).ConfigureAwait(false);

                var gravityAffectedBricks = GravitateGameField();

                if (!gravityAffectedBricks)
                {
                    return;
                }

                CreateAndNotifyNewGameFieldData();

                await Task.Delay(500).ConfigureAwait(false);
            }
        }

        public void StopGameIfGameIsOver()
        {
            if (this.DrivenEntity.Any(pair => pair.Key.YCoordinate == 0))
            {
                Debug.WriteLine("Game over.");

                ContainerProvider.Resolve<IGameProvider>().GetGameInstance()?.GameOver();
            }
        }

        public GameField GetFieldState()
        {
            return this.DrivenEntity;
        }

        public void CreateAndNotifyNewGameFieldData()
        {
            var newGameFieldData = CreateEmptyGameFieldData();

            FillGameFieldData(newGameFieldData);

            ContainerProvider.Resolve<IGameProvider>().GetGameInstance()
                ?.RaiseGameFieldChanged(new GameFieldChangedEventArgs(newGameFieldData));
        }

        private List<BrickPosition> GetPositionsWithBricksMarkedToDestroy()
        {
            return this.DrivenEntity.Where(pair => pair.Value.Destroy).Select(pair => pair.Key).ToList();
        }

        private bool GravitateGameField()
        {
            var brickAffectedByGravity = false;

            var indexOfLastButOneRow = this.Settings.FieldHeight - 2;

            for (var i = 0; i < this.Settings.FieldWidth; i++)
            {
                for (var j = indexOfLastButOneRow; j >= 0; j--)
                {
                    var brickPosition = new BrickPosition {XCoordinate = i, YCoordinate = j};

                    if (!this.DrivenEntity.TryGetValue(brickPosition, out var brick))
                    {
                        continue;
                    }

                    var positionUnderBrick = brickPosition.IncrementYCoordinate();
                    BrickPosition? targetBrickPosition = null;

                    while (positionUnderBrick.YCoordinate < this.Settings.FieldHeight)
                    {
                        if (!this.DrivenEntity.ContainsKey(positionUnderBrick))
                        {
                            targetBrickPosition = positionUnderBrick;
                        }

                        positionUnderBrick = positionUnderBrick.IncrementYCoordinate();
                    }

                    if (!targetBrickPosition.HasValue)
                    {
                        continue;
                    }

                    this.DrivenEntity.Remove(brickPosition);
                    this.DrivenEntity.Add(targetBrickPosition.Value, brick);

                    brickAffectedByGravity = true;
                }
            }

            return brickAffectedByGravity;
        }

        private void RemoveBricksFromPositions(List<BrickPosition> positionsToRemove)
        {
            foreach (var brickPosition in positionsToRemove)
            {
                this.DrivenEntity.Remove(brickPosition);
            }
        }

        private int[,] CreateEmptyGameFieldData()
        {
            var newGameFieldData = new int[this.Settings.FieldWidth, this.Settings.FieldHeight];

            for (var i = 0; i < this.Settings.FieldWidth; i++)
            {
                for (var j = 0; j < this.Settings.FieldHeight; j++)
                {
                    newGameFieldData[i, j] = -1;
                }
            }

            return newGameFieldData;
        }

        private void FillGameFieldData(int[,] newGameFieldData)
        {
            foreach (var pair in this.DrivenEntity)
            {
                newGameFieldData[pair.Key.XCoordinate, pair.Key.YCoordinate] = pair.Value.BrickKind;
            }
        }

        private bool IsRequestedBrickPositionOccupiedByAnotherBrick(KeyValuePair<IBrick, BrickPosition> brick)
        {
            return this.DrivenEntity.ContainsKey(brick.Value);
        }

        private void RemoveBrickFromOldPosition(KeyValuePair<IBrick, BrickPosition> brick)
        {
            BrickPosition? keyToRemove = null;

            foreach (var pair in this.DrivenEntity)
            {
                if (pair.Value != brick.Key)
                {
                    continue;
                }

                keyToRemove = pair.Key;
                break;
            }

            if (keyToRemove.HasValue)
            {
                this.DrivenEntity.Remove(keyToRemove.Value);
            }
        }

        private void SetBrickToNewPosition(KeyValuePair<IBrick, BrickPosition> brick)
        {
            this.DrivenEntity.Add(brick.Value, brick.Key);
        }
    }
}