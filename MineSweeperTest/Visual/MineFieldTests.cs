using MineSweeper.Visual;
using Xunit;
using Assert = Xunit.Assert;

namespace MineSweeper.Test.Visual
{
	public class MineFieldTests
	{
		private readonly MineField _mineField;

		public MineFieldTests()
		{
			// Arrange
			int gridSize = 5;
			int mineCount = 3;
			_mineField = new MineField(gridSize, mineCount);
		}

		[Fact]
		public void TestIsMinePlanted()
		{
			var minesPlanted = 0;

			foreach (var item in _mineField.Field)
			{
				if (!item.IsMine)
				{
					continue;
				}

				minesPlanted++;
			}

			Assert.Equal(_mineField.MineCount, minesPlanted);
		}

		[Theory]
		[InlineData(3, 3)]
		[InlineData(1, 3)]
		[InlineData(0, 0)]
		public void TestRevealField(int rowPosition, int colPosition)
		{
			try
			{
				_mineField.Uncover(rowPosition, colPosition);

				// Check if field is revaled
				Assert.True(_mineField.Field[rowPosition, colPosition].IsRevealed);
			}
			catch (Exception)
			{
				Assert.Fail();
			}
		}

		[Fact]
		public void TestCalAdjMines()
		{
			try
			{
				_mineField.CalculateAdjacentMines();
			}
			catch (Exception)
			{
				Assert.Fail();
			}
		}
	}
}