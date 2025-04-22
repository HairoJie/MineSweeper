namespace MineSweeper.Visual
{
	public class MineField
    {
        private readonly string[] ColumnHeaders;

        private readonly char[,] Grid;

        private readonly string[] RowHeaders;

		public MineField(int gridSize, int mineCount)
        {
            GridSize = gridSize;
            MineCount = mineCount;
            Grid = new char[gridSize, gridSize];
            Mines = new bool[gridSize, gridSize];

            RowHeaders = new string[gridSize];
            ColumnHeaders = new string[gridSize];

            InitializeHeaders(gridSize);
            InitializeGridMine();

			PlantMines(mineCount);

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

        private void UpdateField()
        {
			for (int i = 0; i < GridSize; i++)
            {
				for (int j = 0; j < GridSize; j++)
                {
					if (Mines[i, j])
                    {
                        Grid[i, j] = '*';
					}
					else
                    {
                        Grid[i, j] = ' ';
					}
				}
			}
		}
        
        public bool DigField(int rowPosition, int colPosition)
        {
            if (IsMine(rowPosition, colPosition))
            {
                return true;
            }

            return false;
        }

        private bool IsMine(int rowPosition, int colPosition)
        {
            return Mines[rowPosition, colPosition];
        }

        private void PlantMines(int mineCount)
        {
			var random = new Random();
			int plantedMines = 0;

			while (plantedMines < mineCount)
			{
				int colPosition = random.Next(GridSize);
				int rowPosition = random.Next(GridSize);

                var isMinePlanted = Mines[rowPosition, colPosition];
				if (isMinePlanted)
				{
					continue;
				}

				Mines[rowPosition, colPosition] = true;
				plantedMines++;
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