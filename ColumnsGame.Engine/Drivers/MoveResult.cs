namespace ColumnsGame.Engine.Drivers
{
    internal class MoveResult
    {
        internal bool Success { get; }

        internal MoveResult(bool success)
        {
            this.Success = success;
        }
    }
}
