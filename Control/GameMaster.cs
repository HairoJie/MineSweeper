namespace MineSweeper.Control
{
    using MineSweeper.Control.Helper;
    using MineSweeper.Visual;
    using MineSweeper.Visual.Model;

    public class GameMaster
    {
        public void GameStart()
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Welcome to Minesweeper!!");
                Console.WriteLine("Enter the size of the grid (e.g. 4 for a 4x4 grid): ");

                int gridSize;
                while (!int.TryParse(Console.ReadLine(), out gridSize) || gridSize < 1)
                {
                    Console.WriteLine("Invalid input. Please enter a positive integer.");
                }

                Console.WriteLine("Enter the number of mines to place on the grid (maximum is 35% of the total squares): ");
                double mineCount;
                while (!double.TryParse(Console.ReadLine(), out mineCount) || mineCount < 1 || mineCount >= gridSize * gridSize)
                {
                    Console.WriteLine("Invalid input. Please enter a positive integer less than the total number of cells.");
                }

                var mineField = new MineField(gridSize, mineCount);

                while (true)
                {
                    try
                    {
                        Console.WriteLine();
                        Console.WriteLine("Here is your minefield:");

                        mineField.Display();

                        Console.WriteLine();
                        Console.WriteLine("Select a square to reveal (e.g. A1): ");

                        var rawPosition = Console.ReadLine();
                        var fieldPosition = ParseInput(rawPosition, mineField.GridSize);
                        mineField.Uncover(fieldPosition.RowPosition, fieldPosition.ColPosition);

                        if (mineField.IsWin())
                        {
                            Congrats(mineField);
                            break;
                        }

                        if (mineField.IsMine(fieldPosition.RowPosition, fieldPosition.ColPosition))
                        {
                            GameOver(mineField);
                            break;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        Console.WriteLine();
                    }
                }
            }
        }

        private void GameOver(MineField mineField)
        {
            Console.WriteLine();
            Console.WriteLine("Oh no, you detonated a mine! Game over.");
            Console.WriteLine();
            mineField.DisplayAll();
            Console.WriteLine();
            Console.WriteLine("Press any key to play again...");
            Console.ReadKey();
        }

        private void Congrats(MineField mineField)
        {
            Console.WriteLine();
            Console.WriteLine("Congratulations, you have won the game!");
            Console.WriteLine();
            mineField.DisplayAll();
            Console.WriteLine("Press any key to play again...");
            Console.WriteLine();
            Console.ReadKey();
        }

        private FieldPosition ParseInput(string? input, int gridSize)
        {
            if (string.IsNullOrEmpty(input))
            {
                throw new ArgumentException("Input cannot be null or empty.");
            }

            return InputHelper.ParseFieldPosition(input, gridSize);
        }
    }
}