using ColumnsGame.Engine.Positions;

namespace ColumnsGame.Engine.RemovablePatterns
{
    internal class HorizontalRemovablePattern : RemovablePatternBase
    {
        protected override BrickPosition GetPredecessorPosition(BrickPosition brickPosition)
        {
            return brickPosition.DecrementXCoordinate();
        }
    }
}