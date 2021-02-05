namespace ColumnsGame.Engine.Positions
{
    public struct BrickPosition
    {
        public int XCoordinate;

        public int YCoordinate;

        internal BrickPosition IncrementYCoordinate()
        {
            return new BrickPosition
            {
                XCoordinate = this.XCoordinate,
                YCoordinate = this.YCoordinate + 1
            };
        }
    }
}