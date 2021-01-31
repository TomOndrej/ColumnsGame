using System.Collections.Generic;
using ColumnsGame.Engine.Bricks;
using ColumnsGame.Engine.Columns;
using ColumnsGame.Engine.Interfaces;

namespace ColumnsGame.Engine.Drivers
{
    internal class ColumnDriver : IColumnDriver
    {
        private IGameSettings Settings { get; set; }

        private readonly Dictionary<IBrick, BrickPosition> brickPositions = new Dictionary<IBrick, BrickPosition>();

        private Column DrivenColumn { get; set; }

        public void Initialize(IGameSettings settings)
        {
            this.Settings = settings;
        }

        public void DriveColumn(Column column)
        {
            this.DrivenColumn = column;

            this.brickPositions.Clear();

            for (int i = 0; i < this.DrivenColumn.Count; i++)
            {
                this.brickPositions.Add(
                    this.DrivenColumn[i], 
                    new BrickPosition
                    {
                        Xcoordinate = GetXCoordinateOfNewlyDrivenColumn(),
                        Ycoordinate = GetYCoorditeOfNewlyDrivenColumn(i)
                    });
            }
        }

        private int GetXCoordinateOfNewlyDrivenColumn()
        {
            return this.Settings.FieldWidth / 2;
        }

        private int GetYCoorditeOfNewlyDrivenColumn(int brickIndex)
        {
            return -(this.DrivenColumn.Count - brickIndex);
        }
    }
}
