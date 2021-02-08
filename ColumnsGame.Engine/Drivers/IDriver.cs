namespace ColumnsGame.Engine.Drivers
{
    internal interface IDriver<TDrivenEntity>
        where TDrivenEntity : IDrivable
    {
        TDrivenEntity DrivenEntity { get; }

        void Drive(TDrivenEntity entityToDrive);
    }
}