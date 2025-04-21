namespace MineSweeper.Visual
{
    internal class MineField
    {
        private readonly string[] ColumnHeaders;

        private readonly char[,] Grid;

        private readonly string[] RowHeaders;

        internal MineField(int gridSize, int mineCount)
        {
            GridSize = gridSize;
            MineCount = mineCount;
            Grid = new char[gridSize, gridSize];
            Mines = new bool[gridSize, gridSize];

            RowHeaders = new string[gridSize];
            ColumnHeaders = new string[gridSize];

            InitializeHeaders(gridSize);
            InitializeGridMine();
        }

        public int GridSize { get; }

        public int MineCount { get; }

        public bool[,] Mines { get; }

        public void Display()
        {
            // Print row headers
            Console.Write($"  {string.Join(" ", RowHeaders)}");

            // Print Grid
            for (int i = 0; i < GridSize; i++)
            {
                Console.WriteLine();
                Console.Write(ColumnHeaders[i] + " ");
                for (int j = 0; j < GridSize; j++)
                {
                    Console.Write(Grid[i, j] + " ");
                }
            }
        }

        private void InitializeGridMine()
        {
            for (int i = 0; i < GridSize; i++)
            {
                for (int j = 0; j < GridSize; j++)
                {
                    Grid[i, j] = '_';
                    Mines[i, j] = false;
                }
            }
        }

        private void InitializeHeaders(int gridSize)
        {
            for (int i = 0; i < gridSize; i++)
            {
                RowHeaders[i] = (i + 1).ToString();
                ColumnHeaders[i] = ((char)('A' + i)).ToString();
            }
        }
    }
}