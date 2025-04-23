namespace MineSweeper.Visual
{
	public class FieldCell
	{
		public bool IsMine { get; set; }

		public bool IsRevealed { get; set; }

		public int AdjacentMines { get; set; }
	}
}