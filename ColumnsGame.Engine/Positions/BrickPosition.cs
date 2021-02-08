namespace ColumnsGame.Engine.Positions
{
    internal struct BrickPosition
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

        internal BrickPosition DecrementYCoordinate()
        {
            return new BrickPosition
            {
                XCoordinate = this.XCoordinate,
                YCoordinate = this.YCoordinate - 1
            };
        }

        internal BrickPosition IncrementXCoordinate()
        {
            return new BrickPosition
            {
                XCoordinate = this.XCoordinate + 1,
                YCoordinate = this.YCoordinate
            };
        }

        internal BrickPosition DecrementXCoordinate()
        {
            return new BrickPosition
            {
                XCoordinate = this.XCoordinate - 1,
                YCoordinate = this.YCoordinate
            };
        }

        internal (int X, int Y) ToTuple()
        {
            return (this.XCoordinate, this.YCoordinate);
        }

        internal static BrickPosition FromTuple((int X, int Y) tuple)
        {
            return new BrickPosition {XCoordinate = tuple.X, YCoordinate = tuple.Y};
        }
    }
}