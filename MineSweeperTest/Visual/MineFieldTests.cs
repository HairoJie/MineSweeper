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

		[Fact]
		public void TestRevealField()
		{
			try
			{
				_mineField.Uncover(3, 3);
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