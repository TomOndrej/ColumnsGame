using ColumnsGame.Engine.Field;

namespace ColumnsGame.Engine.Services
{
    internal interface IGravitationService
    {
        bool GravitateGameField(GameField gameField);
    }
}