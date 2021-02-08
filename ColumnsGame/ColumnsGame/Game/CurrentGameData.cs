using System.Collections.Generic;
using ColumnsGame.Engine.Interfaces;
using Newtonsoft.Json;

namespace ColumnsGame.Game
{
    internal class CurrentGameData : ICurrentGameData
    {
        public Dictionary<(int X, int Y), int> Column { get; set; }
        public Dictionary<(int X, int Y), int> GameField { get; set; }

        [JsonIgnore]
        public bool IsEmpty => this.Column == null && this.GameField == null;
    }
}