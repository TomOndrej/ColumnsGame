using System;

namespace ColumnsGame.Engine.Interfaces
{
    public interface IGameSettings
    {
        TimeSpan GameSpeed { get; }

        int FieldWidth { get; }

        int FieldHeight { get; }

        int CountOfDifferentBrickKinds { get; }

        int ColumnLength { get; }
    }
}
