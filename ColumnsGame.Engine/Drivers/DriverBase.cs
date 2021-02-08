namespace ColumnsGame.Engine.Drivers
{
    internal abstract class DriverBase<TDrivenEntity> : IDriver<TDrivenEntity>
        where TDrivenEntity : IDrivable
    {
        protected TDrivenEntity DrivenEntity { get; set; }

        public virtual void Drive(TDrivenEntity entityToDrive)
        {
            this.DrivenEntity = entityToDrive;
        }
    }
}