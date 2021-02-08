using System.Collections.Generic;
using ColumnsGame.Engine.Bricks;
using ColumnsGame.Engine.Field;
using ColumnsGame.Engine.Interfaces;
using ColumnsGame.Engine.Positions;

namespace ColumnsGame.Engine.Providers
{
    internal interface ICurrentGameDataProvider
    {
        void FillCurrentGameData(ICurrentGameData currentGameData);

        void RestoreGameFieldAndColumnFromGameData(ICurrentGameData currentGameData, out GameField gameField,
            out Dictionary<BrickPosition, IBrick> column);
    }
}