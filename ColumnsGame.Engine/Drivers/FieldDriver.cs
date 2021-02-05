using System;
using System.Collections.Generic;
using System.Linq;
using ColumnsGame.Engine.Bricks;
using ColumnsGame.Engine.EventArgs;
using ColumnsGame.Engine.Field;
using ColumnsGame.Engine.GameProvider;
using ColumnsGame.Engine.Ioc;
using ColumnsGame.Engine.Positions;

namespace ColumnsGame.Engine.Drivers
{
    internal class FieldDriver : DriverBase<GameField>, IFieldDriver
    {
        public MoveResult TryMoveBricksDown(List<KeyValuePair<IBrick, BrickPosition>> bricks)
        {
            if (bricks == null || !bricks.Any())
            {
                throw new ArgumentException("Must be non-empty list", nameof(bricks));
            }

            if (IsRequestedBrickPositionOccupiedByAnotherBrick(bricks[0]))
            {
                return new MoveResult(false);
            }

            if (IsRequestedBrickYCoordinateOutsideField(bricks[0]))
            {
                return new MoveResult(false);
            }

            foreach (var brick in bricks)
            {
                RemoveBrickFromOldPosition(brick);
                SetBrickToNewPosition(brick);
            }

            var newGameFieldData = CreateEmptyGameFieldData();

            FillGameFieldData(newGameFieldData);

            ContainerProvider.Resolve<IGameProvider>().GetGameInstance()
                ?.RaiseGameFieldChanged(new GameFieldChangedEventArgs(newGameFieldData));

            return new MoveResult(true);
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

        private bool IsRequestedBrickYCoordinateOutsideField(KeyValuePair<IBrick, BrickPosition> brick)
        {
            return brick.Value.YCoordinate >= this.Settings.FieldHeight;
        }

        private void RemoveBrickFromOldPosition(KeyValuePair<IBrick, BrickPosition> brick)
        {
            BrickPosition? keyToRemove = null;

            foreach (var pair in this.DrivenEntity)
            {
                if (pair.Value == brick.Key)
                {
                    keyToRemove = pair.Key;
                    break;
                }
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