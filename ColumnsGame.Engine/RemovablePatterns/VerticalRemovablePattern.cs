using ColumnsGame.Engine.Positions;

namespace ColumnsGame.Engine.RemovablePatterns
{
    internal class VerticalRemovablePattern : RemovablePatternBase
    {
        protected override BrickPosition GetPredecessorPosition(BrickPosition brickPosition)
        {
            return brickPosition.DecrementYCoordinate();
        }
    }
}