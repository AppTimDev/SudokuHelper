namespace SudokuHelper.Sudoku
{
    public class SudokuBlock: SudokuHouse
    {
        public int BlockNum { get; set; }
        public SudokuBlock(int blockNum)
        {
            BlockNum = blockNum;
        }
    }
}
