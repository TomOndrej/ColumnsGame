using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ColumnsGame.Engine.Bricks;
using ColumnsGame.Engine.Field;
using ColumnsGame.Engine.Ioc;
using ColumnsGame.Engine.Positions;
using ColumnsGame.Engine.Providers;
using ColumnsGame.Engine.RemovablePatterns;
using ColumnsGame.Engine.Services;

namespace ColumnsGame.Engine.Drivers
{
    internal class FieldDriver : DriverBase<GameField>, IFieldDriver
    {
        public MoveResult TryMoveBricks(List<KeyValuePair<BrickPosition, IBrick>> bricks)
        {
            if (bricks == null || !bricks.Any())
            {
                throw new ArgumentException("Must be non-empty list", nameof(bricks));
            }

            if (IsRequestedBrickPositionOccupiedByAnotherBrick(bricks[0]))
            {
                return new MoveResult(false);
            }

            if (bricks[0].Key.IsOutsideField())
            {
                return new MoveResult(false);
            }

            foreach (var brick in bricks)
            {
                RemoveBrickFromOldPosition(brick);
                SetBrickToNewPosition(brick);
            }

            ContainerProvider.Resolve<INotificationService>().CreateAndNotifyNewGameFieldData();

            return new MoveResult(true);
        }

        public void ChangeKindOfBricks()
        {
            ContainerProvider.Resolve<INotificationService>().CreateAndNotifyNewGameFieldData();
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

                ContainerProvider.Resolve<INotificationService>().CreateAndNotifyNewGameFieldData();

                await Task.Delay(500).ConfigureAwait(false);

                var gravityAffectedBricks = ContainerProvider.Resolve<IGravitationService>()
                    .GravitateGameField(this.DrivenEntity);

                if (!gravityAffectedBricks)
                {
                    return;
                }

                ContainerProvider.Resolve<INotificationService>().CreateAndNotifyNewGameFieldData();

                await Task.Delay(500).ConfigureAwait(false);
            }
        }

        public void StopGameIfGameIsOver()
        {
            if (this.DrivenEntity.Any(pair => pair.Key.YCoordinate == 0))
            {
                ContainerProvider.Resolve<IGameProvider>().GetGameInstance()?.GameOver();
            }
        }

        public GameField GetFieldState()
        {
            return this.DrivenEntity;
        }

        private List<BrickPosition> GetPositionsWithBricksMarkedToDestroy()
        {
            return this.DrivenEntity.Where(pair => pair.Value.Destroy).Select(pair => pair.Key).ToList();
        }

        private void RemoveBricksFromPositions(List<BrickPosition> positionsToRemove)
        {
            foreach (var brickPosition in positionsToRemove)
            {
                this.DrivenEntity.Remove(brickPosition);
            }
        }

        private bool IsRequestedBrickPositionOccupiedByAnotherBrick(KeyValuePair<BrickPosition, IBrick> brick)
        {
            return this.DrivenEntity.ContainsKey(brick.Key);
        }

        private void RemoveBrickFromOldPosition(KeyValuePair<BrickPosition, IBrick> brick)
        {
            BrickPosition? keyToRemove = null;

            foreach (var pair in this.DrivenEntity)
            {
                if (pair.Value != brick.Value)
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

        private void SetBrickToNewPosition(KeyValuePair<BrickPosition, IBrick> brick)
        {
            if (this.DrivenEntity.ContainsKey(brick.Key))
            {
                return;
            }

            this.DrivenEntity.Add(brick.Key, brick.Value);
        }
    }
}