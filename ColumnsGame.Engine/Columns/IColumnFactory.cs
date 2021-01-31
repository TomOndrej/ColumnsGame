using ColumnsGame.Engine.Interfaces;

namespace ColumnsGame.Engine.Columns
{
    interface IColumnFactory
    {
        Column CreateColumn(IGameSettings gameSettings);
    }
}
