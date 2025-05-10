namespace MineSweeper
{
    using MineSweeper.Control;

    internal static class Program
    {
        private static void Main(string[] args)
        {
            var gameMaster = new GameMaster();
            gameMaster.GameStart();
        }
    }
}