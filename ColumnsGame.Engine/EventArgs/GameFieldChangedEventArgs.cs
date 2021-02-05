namespace ColumnsGame.Engine.EventArgs
{
    public class GameFieldChangedEventArgs : System.EventArgs
    {
        public GameFieldChangedEventArgs(int[,] newGameFieldData)
        {
            this.NewGameFieldData = newGameFieldData;
        }

        public int[,] NewGameFieldData { get; }
    }
}