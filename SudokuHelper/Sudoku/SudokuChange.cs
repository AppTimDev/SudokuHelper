namespace SudokuHelper.Sudoku
{
    public enum SudokuChangeType { SetNum, AddNote, RemoveNote, HighlightNoteGreen, HighlightNoteRed};
    public class SudokuChange
    {
        public SudokuChangeType Type { get; set; }
        public int Row { get; set; }
        public int Col { get; set; }
        public int Num { get; set; }
        public int NoteNum { get; set; }
        public string Message { get; set; } = "";
        public SudokuChange(SudokuChangeType Type, int Row, int Col, int Num) { 
            this.Type = Type;
            this.Row = Row;
            this.Col = Col;
            this.Num = Num;
        }
        public SudokuChange(SudokuChangeType Type, int Row, int Col, int Num, int NoteNum)
        {
            this.Type = Type;
            this.Row = Row;
            this.Col = Col;
            this.Num = Num;
            this.NoteNum = NoteNum;
        }
    }
}
