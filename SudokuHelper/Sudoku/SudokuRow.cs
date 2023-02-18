namespace SudokuHelper.Sudoku
{
    public class SudokuRow : SudokuHouse
    {
        public int RowNum { get; set; }
        public SudokuRow(int RowNum)
        {
            this.RowNum = RowNum;
        }
    }
}
