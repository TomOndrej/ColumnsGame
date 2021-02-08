using ColumnsGame.Engine.Field;
using ColumnsGame.Engine.Ioc;
using ColumnsGame.Engine.Positions;
using ColumnsGame.Engine.Providers;

namespace ColumnsGame.Engine.Services
{
    internal class GravitationService : IGravitationService
    {
        public bool GravitateGameField(GameField gameField)
        {
            var brickAffectedByGravity = false;

            var settings = ContainerProvider.Resolve<ISettingsProvider>().GetSettingsInstance();

            var indexOfLastButOneRow = settings.FieldHeight - 2;

            for (var i = 0; i < settings.FieldWidth; i++)
            {
                for (var j = indexOfLastButOneRow; j >= 0; j--)
                {
                    var brickPosition = new BrickPosition {XCoordinate = i, YCoordinate = j};

                    if (!gameField.TryGetValue(brickPosition, out var brick))
                    {
                        continue;
                    }

                    var positionUnderBrick = brickPosition.IncrementYCoordinate();
                    BrickPosition? targetBrickPosition = null;

                    while (positionUnderBrick.YCoordinate < settings.FieldHeight)
                    {
                        if (!gameField.ContainsKey(positionUnderBrick))
                        {
                            targetBrickPosition = positionUnderBrick;
                        }

                        positionUnderBrick = positionUnderBrick.IncrementYCoordinate();
                    }

                    if (!targetBrickPosition.HasValue)
                    {
                        continue;
                    }

                    gameField.Remove(brickPosition);
                    gameField.Add(targetBrickPosition.Value, brick);

                    brickAffectedByGravity = true;
                }
            }

            return brickAffectedByGravity;
        }
    }
}