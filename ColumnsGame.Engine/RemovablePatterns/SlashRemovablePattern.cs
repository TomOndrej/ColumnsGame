using ColumnsGame.Engine.Positions;

namespace ColumnsGame.Engine.RemovablePatterns
{
    internal class SlashRemovablePattern : RemovablePatternBase
    {
        protected override BrickPosition GetPredecessorPosition(BrickPosition brickPosition)
        {
            return brickPosition.IncrementXCoordinate().DecrementYCoordinate();
        }
    }
}