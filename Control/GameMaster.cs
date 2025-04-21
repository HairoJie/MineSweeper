namespace MineSweeper.Control
{
    using MineSweeper.Visual;

    internal class GameMaster
    {
        private readonly MineField mineField;

        public GameMaster(int gridSize, int mineCount)
        {
            mineField = new MineField(gridSize, mineCount);
        }

        public void Start()
        {
            mineField.Display();
            Console.WriteLine();
            Console.WriteLine("Game started!");
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}