using System.Collections.Generic;
using ColumnsGame.Engine.Bricks;
using ColumnsGame.Engine.Field;
using ColumnsGame.Engine.Positions;

namespace ColumnsGame.Engine.RemovablePatterns
{
    internal abstract class RemovablePatternBase : IRemovablePattern
    {
        public void MarkBricksInPatternToDestroy(GameField gameField)
        {
            foreach (var brick in gameField)
            {
                MarkBrickAndItsPredecessorsToDestroy(brick, gameField);
            }
        }

        private void MarkBrickAndItsPredecessorsToDestroy(KeyValuePair<BrickPosition, IBrick> brick,
            GameField gameField)
        {
            var firstPredecessorPosition = GetPredecessorPosition(brick.Key);

            if (!gameField.TryGetValue(firstPredecessorPosition, out var firstPredecessor))
            {
                return;
            }

            if (firstPredecessor.BrickKind != brick.Value.BrickKind)
            {
                return;
            }

            var secondPredecessorPosition = GetPredecessorPosition(firstPredecessorPosition);

            if (!gameField.TryGetValue(secondPredecessorPosition, out var secondPredecessor))
            {
                return;
            }

            if (secondPredecessor.BrickKind != brick.Value.BrickKind)
            {
                return;
            }

            brick.Value.Destroy = true;
            firstPredecessor.Destroy = true;
            secondPredecessor.Destroy = true;
        }

        protected abstract BrickPosition GetPredecessorPosition(BrickPosition brickPosition);
    }
}