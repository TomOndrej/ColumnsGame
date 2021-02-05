using System.Collections.Generic;
using ColumnsGame.Engine.Bricks;
using ColumnsGame.Engine.Drivers;

namespace ColumnsGame.Engine.Columns
{
    internal class Column : List<IBrick>, IDrivable
    { }
}
