using System;
using ColumnsGame.Engine.Interfaces;

namespace ColumnsGame.Engine.Bricks
{
    internal class BrickFactory : IBrickFactory
    {
        public IBrick CreateBrick(IGameSettings gameSettings)
        {
            return new Brick
            {
                BrickKind = GenerateRandomBrickKind(gameSettings)
            };
        }

        private int GenerateRandomBrickKind(IGameSettings gameSettings)
        {
            Random rand = new Random();
            return rand.Next(0, gameSettings.CountOfDifferentBrickKinds + 1);
        }
    }
}
