using System.Collections.Generic;
using ColumnsGame.Engine.Bricks;
using ColumnsGame.Engine.Drivers;
using ColumnsGame.Engine.Positions;

namespace ColumnsGame.Engine.Field
{
    internal class GameField : Dictionary<BrickPosition, IBrick>, IDrivable
    {
        internal int Width { get; }

        internal int Height { get; }

        internal GameField(int width, int height)
        {
            this.Width = width;
            this.Height = height;
        }
    }
}
