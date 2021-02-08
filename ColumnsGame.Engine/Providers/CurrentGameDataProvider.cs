using System.Collections.Generic;
using System.Linq;
using ColumnsGame.Engine.Bricks;
using ColumnsGame.Engine.Drivers;
using ColumnsGame.Engine.Field;
using ColumnsGame.Engine.Interfaces;
using ColumnsGame.Engine.Ioc;
using ColumnsGame.Engine.Positions;

namespace ColumnsGame.Engine.Providers
{
    internal class CurrentGameDataProvider : ICurrentGameDataProvider
    {
        public void FillCurrentGameData(ICurrentGameData currentGameData)
        {
            var game = ContainerProvider.Resolve<IGameProvider>().GetGameInstance();

            if (game == null)
            {
                return;
            }

            if (!game.IsRunning && !game.IsPaused)
            {
                return;
            }

            FillColumn(currentGameData);

            FillGameField(currentGameData);
        }

        public void RestoreGameFieldAndColumnFromGameData(ICurrentGameData currentGameData, out GameField gameField,
            out Dictionary<BrickPosition, IBrick> column)
        {
            gameField = RestoreGameFieldFromGameData(currentGameData);

            column = RestoreColumnFromGameDataAndMergeWithGameField(currentGameData, gameField);
        }

        private void FillGameField(ICurrentGameData currentGameData)
        {
            var fieldState = ContainerProvider.Resolve<IFieldDriver>().GetFieldState();

            if (fieldState != null)
            {
                currentGameData.GameField = fieldState
                    .ToDictionary(pair => pair.Key.ToTuple(), pair => pair.Value.BrickKind);
            }
        }

        private void FillColumn(ICurrentGameData currentGameData)
        {
            var columnState = ContainerProvider.Resolve<IColumnDriver>().GetColumnState();

            if (columnState != null)
            {
                currentGameData.Column =
                    columnState.ToDictionary(pair => pair.Key.ToTuple(), pair => pair.Value.BrickKind);
            }
        }

        private GameField RestoreGameFieldFromGameData(ICurrentGameData currentGameData)
        {
            var settings = ContainerProvider.Resolve<ISettingsProvider>().GetSettingsInstance();

            var gameField = new GameField(settings.FieldWidth, settings.FieldHeight);

            foreach (var pair in currentGameData.GameField)
            {
                gameField.Add(BrickPosition.FromTuple(pair.Key), new Brick {BrickKind = pair.Value});
            }

            return gameField;
        }

        private Dictionary<BrickPosition, IBrick> RestoreColumnFromGameDataAndMergeWithGameField(
            ICurrentGameData currentGameData,
            GameField gameField)
        {
            Dictionary<BrickPosition, IBrick> column;

            if (currentGameData.Column == null)
            {
                column = null;
            }
            else
            {
                column = new Dictionary<BrickPosition, IBrick>();

                foreach (var pair in currentGameData.Column)
                {
                    var brickPosition = BrickPosition.FromTuple(pair.Key);

                    if (gameField.TryGetValue(brickPosition, out var brick))
                    {
                        column.Add(brickPosition, brick);
                    }
                }
            }

            return column;
        }
    }
}