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
                GameSpeed = TimeSpan.FromMilliseconds(500)
            };
        }
    }
}
