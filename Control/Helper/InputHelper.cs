using MineSweeper.Visual.Model;

namespace MineSweeper.Control.Helper
{
	public static class InputHelper
	{
		public static FieldPosition ParseFieldPosition(string input, int gridSize)
		{
			if (input.Length < 2)
			{
				throw new ArgumentException($"Invalid input format '{input}'. Expected format 'A1'.");
			}

			char columnChar = input[0];
			int rowNumber = int.Parse(input[1..]);

			// Get position using ASCII
			int rowPosition = columnChar - 'A';
			int colPosition = rowNumber - 1;

			if (rowPosition < 0 || rowPosition >= gridSize || colPosition < 0 || colPosition >= gridSize)
			{
				throw new InvalidOperationException("Input is out of bounds.");
			}

			return new FieldPosition(rowPosition, colPosition);
		}
	}
}