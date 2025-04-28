using MineSweeper.Control.Helper;
using Xunit;
using Assert = Xunit.Assert;

namespace MineSweeper.Test.Control.Helper
{
	public static class InputHelperTest
	{
		[Theory]
		[InlineData("A1", 5, 0, 0)]
		[InlineData("A4", 5, 0, 3)]
		[InlineData("C5", 5, 2, 4)]
		public static void TestParseFieldPosition(string input, int gridSize, int rowResult, int colResult)
		{
			// Act
			var result = InputHelper.ParseFieldPosition(input, gridSize);

			// Assert
			Assert.Equal(rowResult, result.RowPosition);
			Assert.Equal(colResult, result.ColPosition);
		}

		[Fact]
		public static void TestParseFieldPosition_InvalidInput()
		{
			// Arrange
			string input = "Z1";
			int gridSize = 5;

			// Act & Assert
			Assert.Throws<InvalidOperationException>(() => InputHelper.ParseFieldPosition(input, gridSize));
		}
	}
}