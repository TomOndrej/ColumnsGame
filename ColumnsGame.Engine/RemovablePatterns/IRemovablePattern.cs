using ColumnsGame.Engine.Field;

namespace ColumnsGame.Engine.RemovablePatterns
{
    internal interface IRemovablePattern
    {
        void MarkBricksInPatternToDestroy(GameField gameField);
    }
}