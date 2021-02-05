using ColumnsGame.Engine.Interfaces;

namespace ColumnsGame.Engine.Drivers
{
    internal interface IDriver<in TDrivenEntity>
        where TDrivenEntity : IDrivable
    {
        void Initialize(IGameSettings settings);

        void Drive(TDrivenEntity entityToDrive);
    }
}
