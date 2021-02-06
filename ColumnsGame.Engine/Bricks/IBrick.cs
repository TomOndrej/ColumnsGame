namespace ColumnsGame.Engine.Bricks
{
    internal interface IBrick
    {
        int BrickKind { get; set; }

        bool Destroy { get; set; }
    }
}