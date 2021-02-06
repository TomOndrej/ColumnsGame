using ColumnsGame.Engine.Interfaces;

namespace ColumnsGame.Engine.Positions
{
    internal static class BrickPositionExtensions
    {
        internal static bool IsOutsideField(this BrickPosition brickPosition, IGameSettings gameSettings)
        {
            if (brickPosition.YCoordinate < 0)
            {
                return true;
            }

            if (brickPosition.YCoordinate >= gameSettings.FieldHeight)
            {
                return true;
            }

            if (brickPosition.XCoordinate < 0)
            {
                return true;
            }

            if (brickPosition.XCoordinate >= gameSettings.FieldWidth)
            {
                return true;
            }

            return false;
        }
    }
}