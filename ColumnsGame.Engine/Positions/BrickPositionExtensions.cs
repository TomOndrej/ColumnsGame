using ColumnsGame.Engine.Ioc;
using ColumnsGame.Engine.Providers;

namespace ColumnsGame.Engine.Positions
{
    internal static class BrickPositionExtensions
    {
        internal static bool IsOutsideField(this BrickPosition brickPosition)
        {
            if (brickPosition.YCoordinate < 0)
            {
                return true;
            }

            if (brickPosition.XCoordinate < 0)
            {
                return true;
            }

            var settings = ContainerProvider.Resolve<ISettingsProvider>().GetSettingsInstance();

            if (brickPosition.YCoordinate >= settings.FieldHeight)
            {
                return true;
            }

            if (brickPosition.XCoordinate >= settings.FieldWidth)
            {
                return true;
            }

            return false;
        }
    }
}