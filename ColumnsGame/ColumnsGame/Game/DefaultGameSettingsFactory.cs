using System;
using ColumnsGame.Engine.Interfaces;
using ColumnsGame.Game.Models;
using ColumnsGame.Ioc.Attributes;

namespace ColumnsGame.Game
{
    [IocRegisterImplementation]
    public class DefaultGameSettingsFactory : IDefaultGameSettingsFactory
    {
        public IGameSettings Create()
        {
            return new GameSettings
            {
                GameSpeed = TimeSpan.FromMilliseconds(200),
                FieldWidth = 6,
                FieldHeight = 13,
                CountOfDifferentBrickKinds = 4,
                ColumnLength = 3
            };
        }
    }
}