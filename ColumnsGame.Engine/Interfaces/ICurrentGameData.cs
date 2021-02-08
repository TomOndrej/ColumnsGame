using System.Collections.Generic;

namespace ColumnsGame.Engine.Interfaces
{
    public interface ICurrentGameData
    {
        Dictionary<(int X, int Y), int> Column { get; set; }

        Dictionary<(int X, int Y), int> GameField { get; set; }
    }
}