namespace MineSweeper.Control.Helper
{
    public static class InputHelper
    {
        public static (int rowPosition, int colPosition) ParseFieldPosition(string input, int gridSize)
        {
            if (input.Length < 2)
            {
                throw new ArgumentException($"Invalid input format '{input}'. Expected format 'A1'.");
            }

            char columnChar = input[0];
            int rowNumber = int.Parse(input[1..]);

            // Get position using ASCII
            int colPosition = columnChar - 'A';
            int rowPosition = rowNumber - 1;

            if (colPosition < 0 || colPosition >= gridSize || rowPosition < 0 || rowPosition >= gridSize)
            {
                throw new InvalidOperationException("Input is out of bounds.");
            }

            return (rowPosition, colPosition);
        }
    }
}
