using System;
using ColumnsGame.Engine.Ioc;
using ColumnsGame.Engine.Providers;

namespace ColumnsGame.Engine.Bricks
{
    internal class BrickFactory : IBrickFactory
    {
        public IBrick CreateBrick()
        {
            return new Brick
            {
                BrickKind = GenerateRandomBrickKind()
            };
        }

        private int GenerateRandomBrickKind()
        {
            var settings = ContainerProvider.Resolve<ISettingsProvider>().GetSettingsInstance();

            var rand = new Random();
            return rand.Next(0, settings.CountOfDifferentBrickKinds);
        }
    }
}