namespace ColumnsGame.Engine.Bricks
{
    internal class Brick : IBrick
    {
        public int BrickKind { get; set; }

        public bool Destroy { get; set; }
    }
}