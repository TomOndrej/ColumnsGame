using System;
using ColumnsGame.Engine.Bricks;
using ColumnsGame.Engine.Field;
using ColumnsGame.Engine.Ioc;
using ColumnsGame.Engine.Positions;
using ColumnsGame.Engine.Providers;
using ColumnsGame.Engine.UnitTests.Mocks;
using NUnit.Framework;

namespace ColumnsGame.Engine.UnitTests.Tests.GravitationService
{
    [TestFixture]
    public class GravitationServiceTests
    {
        [Test]
        public void AllBricksAreAtBottomFieldAfterGravitate()
        {
            var gameSettings = new MockGameSettings
            {
                FieldWidth = 10,
                FieldHeight = 10
            };

            ContainerProvider.Resolve<ISettingsProvider>().SetSettingsInstance(gameSettings);

            var gameField = GenerateRandomField(gameSettings.FieldWidth, gameSettings.FieldHeight);
            var gravitationService = new Services.GravitationService();

            gravitationService.GravitateGameField(gameField);

            for (var x = 0; x < gameSettings.FieldWidth; x++)
            {
                var topBrickInColumnFound = false;

                for (var y = 0; y < gameSettings.FieldHeight; y++)
                {
                    var brickPosition = new BrickPosition {XCoordinate = x, YCoordinate = y};

                    if (topBrickInColumnFound)
                    {
                        Assert.IsTrue(gameField.ContainsKey(brickPosition));
                    }
                    else
                    {
                        if (gameField.ContainsKey(brickPosition))
                        {
                            topBrickInColumnFound = true;
                        }
                    }
                }
            }
        }

        private GameField GenerateRandomField(int width, int height)
        {
            var gameField = new GameField(width, height);

            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    var random = new Random();
                    if (random.NextDouble() >= 0.5)
                    {
                        gameField.Add(new BrickPosition {XCoordinate = x, YCoordinate = y}, new Brick());
                    }
                }
            }

            return gameField;
        }
    }
}