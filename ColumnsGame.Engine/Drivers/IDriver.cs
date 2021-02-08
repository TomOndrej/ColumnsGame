namespace ColumnsGame.Engine.Drivers
{
    internal interface IDriver<in TDrivenEntity>
        where TDrivenEntity : IDrivable
    {
        void Drive(TDrivenEntity entityToDrive);
    }
}