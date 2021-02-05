using ColumnsGame.Engine.Interfaces;

namespace ColumnsGame.Engine.Drivers
{
    internal abstract class DriverBase<TDrivenEntity> : IDriver<TDrivenEntity>
        where TDrivenEntity : IDrivable
    {
        protected IGameSettings Settings { get; set; }

        protected TDrivenEntity DrivenEntity { get; set; }

        public virtual void Drive(TDrivenEntity entityToDrive)
        {
            this.DrivenEntity = entityToDrive;
        }

        public void Initialize(IGameSettings settings)
        {
            this.Settings = settings;
        }
    }
}
