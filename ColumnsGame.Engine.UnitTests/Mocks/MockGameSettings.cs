using System;
using ColumnsGame.Engine.Interfaces;

namespace ColumnsGame.Engine.UnitTests.Mocks
{
    public class MockGameSettings : IGameSettings
    {
        public TimeSpan GameSpeed { get; set; }
        public int FieldWidth { get; set; }
        public int FieldHeight { get; set; }
        public int CountOfDifferentBrickKinds { get; set; }
        public int ColumnLength { get; set; }
    }
}