namespace MineSweeper.Visual
{
	public class MineField
	{
		private readonly string[] ColumnHeaders;

		private readonly string[,] Grid;

		private readonly string[] RowHeaders;

		public MineField(int gridSize, int mineCount)
		{
			GridSize = gridSize;
			MineCount = mineCount;
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

		public void CalculateAdjacentMines()
		{
			for (int rowPosition = 0; rowPosition < GridSize; rowPosition++)
			{
				for (int columnPosition = 0; columnPosition < GridSize; columnPosition++)
				{
					if (Field[rowPosition, columnPosition].IsMine) continue;

					int count = 0;

					// Row delta and col delta
					// We check all 8 surrounding cells
					// Each cell is checked by adding the delta to the current cell's position
					// Delta is the difference between the current cell and the surrounding cell
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
			Console.Write($"  {string.Join(" ", RowHeaders)}");

			for (int r = 0; r < GridSize; r++)
			{
				Console.WriteLine();
				Console.Write(ColumnHeaders[r] + " ");

				for (int c = 0; c < GridSize; c++)
				{
					var cell = Field[r, c];
					if (!cell.IsRevealed)
						Console.Write("_ ");
					else if (cell.IsMine)
						Console.Write("_ ");
					else if (cell.AdjacentMines == 0)
						Console.Write("0 ");
					else
						Console.Write($"{cell.AdjacentMines} ");
				}
			}
		}

		public void InitializeBoard()
		{
			for (int rowPosition = 0; rowPosition < GridSize; rowPosition++)
				for (int columnPosition = 0; columnPosition < GridSize; columnPosition++)
					Field[rowPosition, columnPosition] = new FieldCell();
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

		public void UncoverRecursive(int row, int col)
		{
			if (!IsValid(row, col))
			{
				return;
			}

			if (Field[row, col].IsRevealed || Field[row, col].IsMine)
			{
				return;
			}

			Field[row, col].IsRevealed = true;

			if (Field[row, col].AdjacentMines > 0)
			{
				return;
			}

			for (int dRow = -1; dRow <= 1; dRow++)
			{
				for (int dCol = -1; dCol <= 1; dCol++)
				{
					if (dRow != 0 || dCol != 0)
					{
						Uncover(row + dRow, col + dCol);
					}
				}
			}
		}

		public void UpdateFieldGrid(int rowPosition, int colPosition, string value)
		{
			Grid[rowPosition, colPosition] = value;
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

		public bool IsMine(int rowPosition, int colPosition)
		{
			return Field[rowPosition, colPosition].IsMine;
		}

		private void InitializeHeaders(int gridSize)
		{
			for (int i = 0; i < gridSize; i++)
			{
				RowHeaders[i] = (i + 1).ToString();
				ColumnHeaders[i] = ((char)('A' + i)).ToString();
			}
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

		public void PlaceMines()
		{
			var random = new Random();
			int placed = 0;
			while (placed < MineCount)
			{
				int colPosition = random.Next(GridSize);
				int rowPosition = random.Next(GridSize);
				if (!Field[rowPosition, colPosition].IsMine)
				{
					Field[rowPosition, colPosition].IsMine = true;
					placed++;
				}
			}
		}

		private bool IsValid(int rowPosition, int columnPosition) => rowPosition >= 0 && rowPosition < GridSize && columnPosition >= 0 && columnPosition < GridSize;
	}
}