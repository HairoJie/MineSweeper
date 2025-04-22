namespace MineSweeper.Control
{
	using MineSweeper.Control.Helper;
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
			while (true)
			{
				try
				{
					Console.WriteLine();
					Console.WriteLine("Here is your minefield:");

					mineField.Display();

					Console.WriteLine();
					Console.WriteLine("Select a square to reveal (e.g. A1): ");

					var positon = Console.ReadLine();
					var parsedPosition = ParseInput(positon);
					var isMineField = mineField.DigField(parsedPosition.rowPosition, parsedPosition.colPosition);
					if (isMineField)
					{
						GameOver();
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
					Console.WriteLine();
				}
			}
		}

		private void GameOver()
		{
			Console.WriteLine();
			Console.WriteLine("Oh no, you detonated a mine! Game over.");
			Console.WriteLine("Press any key to play again...");
			Console.ReadLine();
		}

		private (int rowPosition, int colPosition) ParseInput(string? input)
		{
			if (string.IsNullOrEmpty(input))
			{
				throw new ArgumentException("Input cannot be null or empty.");
			}

			return InputHelper.ParseFieldPosition(input, mineField.GridSize);
		}
	}
}