namespace MineSweeper.Visual.Model
{
    public struct FieldPosition
    {
        public FieldPosition(int rowPosition, int colPosition)
        {
            RowPosition = rowPosition;
            ColPosition = colPosition;
        }

        public int RowPosition { get; }
        public int ColPosition { get; }
    }
}
