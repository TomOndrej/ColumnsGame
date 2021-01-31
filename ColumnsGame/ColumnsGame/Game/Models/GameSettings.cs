using System;
using ColumnsGame.Engine.Interfaces;

namespace ColumnsGame.Game.Models
{
    public class GameSettings : IGameSettings
    {
        public TimeSpan GameSpeed { get; set; }
    }
}
