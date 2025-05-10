namespace MineSweeper.Visual
{
    public class MineField
    {
        private readonly string[] ColumnHeaders;

        private readonly string[,] Grid;

        private readonly string[] RowHeaders;

        public MineField(int gridSize, double mineCount)
        {
            GridSize = gridSize;
            MineCount = Convert.ToInt32(Math.Round(mineCount));
            Grid = new string[gridSize, gridSize];

            RowHeaders = new string[gridSize];
            ColumnHeaders = new string[gridSize];

            Field = new FieldCell[gridSize, gridSize];

            InitializeHeaders(gridSize);
            InitializeBoard();
            InitializeGrid();

            PlaceMines();
            CalculateAdjacentMines();
        }

        public FieldCell[,] Field { get; }

        public int GridSize { get; }

        public int MineCount { get; }

        public int TotalFields { get => GridSize * GridSize; }

        public void CalculateAdjacentMines()
        {
            for (int rowPosition = 0; rowPosition < GridSize; rowPosition++)
            {
                for (int columnPosition = 0; columnPosition < GridSize; columnPosition++)
                {
                    if (Field[rowPosition, columnPosition].IsMine) continue;

                    int count = 0;
                    for (int dRow = -1; dRow <= 1; dRow++)
                    {
                        for (int dCol = -1; dCol <= 1; dCol++)
                        {
                            int newRowPosition = rowPosition + dRow;
                            int newColumnPosition = columnPosition + dCol;
                            if (IsValid(newRowPosition, newColumnPosition) && Field[newRowPosition, newColumnPosition].IsMine)
                                count++;
                        }
                    }

                    Field[rowPosition, columnPosition].AdjacentMines = count;
                }
            }
        }

        public void Display()
        {
            DisplayFields(false);
        }

        public void DisplayAll()
        {
            DisplayFields(true);
        }

        public void InitializeBoard()
        {
            for (int rowPosition = 0; rowPosition < GridSize; rowPosition++)
                for (int columnPosition = 0; columnPosition < GridSize; columnPosition++)
                    Field[rowPosition, columnPosition] = new FieldCell();
        }

        public bool IsMine(int rowPosition, int colPosition)
        {
            return Field[rowPosition, colPosition].IsMine;
        }

        public bool IsWin()
        {
            for (int row = 0; row < GridSize; row++)
            {
                for (int column = 0; column < GridSize; column++)
                {
                    if (!Field[row, column].IsMine && !Field[row, column].IsRevealed)
                        return false;
                }
            }

            return true;
        }

        public void PlaceMines()
        {
            var random = new Random();
            var fieldAttempted = new HashSet<(int, int)>(TotalFields);
            int placed = 0;
            while (placed < MineCount)
            {
                // Get a random field to place the mine
                int colPosition = random.Next(GridSize);
                int rowPosition = random.Next(GridSize);

                // If all fields has been checked, we stop placing the mine
                if (fieldAttempted.Count >= TotalFields)
                {
                    break;
                }

                // To prevent check repeated field
                if (!fieldAttempted.Contains((rowPosition, colPosition)))
                {
                    fieldAttempted.Add((rowPosition, colPosition));
                }
                else
                {
                    continue;
                }

                // Calculate (refresh) adjacent mines before we place the mine
                CalculateAdjacentMines();

                // Check surrounding fields to see if we can place the mine.
                bool canPlaceMine = CanPlaceMine(colPosition, rowPosition);

                // Place the mine
                if (!Field[rowPosition, colPosition].IsMine && canPlaceMine)
                {
                    Field[rowPosition, colPosition].IsMine = true;

                    // We need to re-visit field that we visited before after place a mine
                    fieldAttempted.Clear();
                    placed++;
                }
            }
        }

        public void Uncover(int startRow, int startCol)
        {
            var queue = new Queue<(int, int)>();
            queue.Enqueue((startRow, startCol));

            while (queue.Count > 0)
            {
                var (row, col) = queue.Dequeue();

                if (!IsValid(row, col))
                {
                    continue;
                }

                if (Field[row, col].IsRevealed || Field[row, col].IsMine)
                {
                    continue;
                }

                Field[row, col].IsRevealed = true;

                if (Field[row, col].AdjacentMines > 0)
                {
                    continue;
                }

                for (int dRow = -1; dRow <= 1; dRow++)
                {
                    for (int dCol = -1; dCol <= 1; dCol++)
                    {
                        if (dRow != 0 || dCol != 0)
                            queue.Enqueue((row + dRow, col + dCol));
                    }
                }
            }
        }

        internal void DisplayFields(bool displayAll)
        {
            Console.Write($"  {string.Join(" ", RowHeaders)}");

            for (int r = 0; r < GridSize; r++)
            {
                Console.WriteLine();
                Console.Write(ColumnHeaders[r] + " ");

                for (int c = 0; c < GridSize; c++)
                {
                    var cell = Field[r, c];
                    if (!cell.IsRevealed && !displayAll)
                        Console.Write("_ ");
                    else if (cell.IsMine && !displayAll)
                        Console.Write("_ ");
                    else if (cell.IsMine && displayAll)
                        Console.Write("* ");
                    else if (cell.AdjacentMines == 0)
                        Console.Write("0 ");
                    else
                        Console.Write($"{cell.AdjacentMines} ");
                }
            }
        }

        private bool CanPlaceMine(int colPosition, int rowPosition)
        {
            bool canPlaceMine = true;

            for (int dRow = -1; dRow <= 1; dRow++)
            {
                for (int dCol = -1; dCol <= 1; dCol++)
                {
                    int newRowPosition = rowPosition + dRow;
                    int newColumnPosition = colPosition + dCol;
                    if (IsValid(newRowPosition, newColumnPosition) && Field[newRowPosition, newColumnPosition].AdjacentMines >= 3)
                    {
                        canPlaceMine = false;
                        break;
                    }
                }

                if (!canPlaceMine)
                {
                    break;
                }
            }

            return canPlaceMine;
        }

        private void InitializeGrid()
        {
            for (int i = 0; i < GridSize; i++)
            {
                for (int j = 0; j < GridSize; j++)
                {
                    Grid[i, j] = "_";
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

        private bool IsValid(int rowPosition, int columnPosition) => rowPosition >= 0 && rowPosition < GridSize && columnPosition >= 0 && columnPosition < GridSize;
    }
}