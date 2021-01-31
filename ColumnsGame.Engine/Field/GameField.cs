using System.Collections.Generic;
using ColumnsGame.Engine.Bricks;

namespace ColumnsGame.Engine.Field
{
    internal class GameField : Dictionary<BrickPosition, IBrick>
    {
        private int Width { get; }

        private int Height { get; }

        internal GameField(int width, int height)
        {
            this.Width = width;
            this.Height = height;
        }
    }
}
