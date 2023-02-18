namespace SudokuHelper.Sudoku
{
    public class SudokuCol : SudokuHouse
    {
        public int ColNum { get; set; }
        public SudokuCol(int ColNum)
        {
            this.ColNum = ColNum;
        }
    }
}
