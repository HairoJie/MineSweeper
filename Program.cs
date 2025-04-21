namespace MineSweeper
{
	using MineSweeper.Control;

	internal static class Program
	{
		private static void Main(string[] args)
		{
			Console.WriteLine("Welcome to Minesweeper!!");
			Console.WriteLine("Enter the size of the grid (e.g. 4 for a 4x4 grid): ");

			int gridSize;
			while (!int.TryParse(Console.ReadLine(), out gridSize) || gridSize < 1)
			{
				Console.WriteLine("Invalid input. Please enter a positive integer.");
			}

			Console.WriteLine("Enter the number of mines to place on the grid (maximum is 35% of the total squares): ");
			int mineCount;
			while (!int.TryParse(Console.ReadLine(), out mineCount) || mineCount < 1 || mineCount >= gridSize * gridSize)
			{
				Console.WriteLine("Invalid input. Please enter a positive integer less than the total number of cells.");
			}

			var gameMaster = new GameMaster(gridSize, mineCount);
			gameMaster.Start();
		}
	}
}