namespace ColumnsGame.Engine.Drivers
{
    internal abstract class DriverBase<TDrivenEntity> : IDriver<TDrivenEntity>
        where TDrivenEntity : IDrivable
    {
        public TDrivenEntity DrivenEntity { get; private set; }

        public virtual void Drive(TDrivenEntity entityToDrive)
        {
            this.DrivenEntity = entityToDrive;
        }
    }
}