using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SudokuHelper.Sudoku
{
    public class SudokuGrid : IEnumerable<SudokuCell>
    {
        int currRow = -1, currCol = -1;
        SudokuCell[,] matrix;
        SudokuBlock[] blocks;
        SudokuRow[] rows;
        SudokuCol[] cols;
        List<SudokuHouse> houses;
        public List<SudokuHouse> Houses
        {
            get
            {
                return houses;
            }
        }
        public int CurrRow
        {
            get
            {
                return currRow;
            }
        }
        public int CurrCol
        {
            get
            {
                return currCol;
            }
        }
        public List<SudokuCell> Cells { get; set; }
        public SudokuGrid()
        {
            this.Cells = new List<SudokuCell>();
            houses = new List<SudokuHouse>();
            blocks = new SudokuBlock[9];
            rows = new SudokuRow[9];
            cols = new SudokuCol[9];
            for (int i = 0; i < 9; i++)
            {
                blocks[i] = new SudokuBlock(i);
                rows[i] = new SudokuRow(i);
                cols[i] = new SudokuCol(i);
                houses.Add(blocks[i]);
                houses.Add(rows[i]);
                houses.Add(cols[i]);
            }
            matrix = new SudokuCell[9, 9];
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    matrix[i, j] = new SudokuCell();
                    matrix[i, j].Row = i;
                    matrix[i, j].Col = j;
                    matrix[i, j].Block = 3 * (i / 3) + (j / 3);
                    matrix[i, j].SudokuBlock = blocks[matrix[i, j].Block];
                    matrix[i, j].SudoRow = rows[i];
                    matrix[i, j].SudoCol = cols[j];

                    matrix[i, j].SudokuHouses.Add(blocks[matrix[i, j].Block]);
                    matrix[i, j].SudokuHouses.Add(rows[i]);
                    matrix[i, j].SudokuHouses.Add(cols[j]);

                    blocks[matrix[i, j].Block].Add(ref matrix[i, j]);
                    rows[i].Add(ref matrix[i, j]);
                    cols[j].Add(ref matrix[i, j]);

                    this.Add(ref matrix[i, j]);
                }
            }
        }
        // use Multidimensional Indexer or GetCell method
        public SudokuCell this[int index1, int index2]
        {
            get
            {
                return matrix[index1, index2];
            }
            set
            {
                matrix[index1, index2] = value;
            }
        }
        public SudokuCell GetCell(int row, int col)
        {
            return matrix[row, col];
        }
        public SudokuRow GetRow(int row)
        {
            return rows[row];
        }
        public SudokuCol GetCol(int col)
        {
            return cols[col];
        }
        public SudokuBlock GetBlock(int block)
        {
            return blocks[block];
        }
        public SudokuBlock[] GetBlocks()
        {
            return blocks;
        }
        public SudokuGrid Copy()
        {
            SudokuGrid grid = new SudokuGrid();
            grid.currRow = this.currRow;
            grid.currCol = this.currCol;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    matrix[i, j].CopyTo(grid[i, j]);
                }
            }
            return grid;
        }
        public void Add(ref SudokuCell cell)
        {
            this.Cells.Add(cell);
        }
        public void HighlightSelectedNumber(ref Graphics g, int num, bool bHighlight)
        {
            if (num > 0 && num <= 9)
            {
                foreach (var cell in this.Cells)
                {
                    //set the value so that we can highlight the cell that match the number
                    if (cell.Num == num)
                    {
                        if (bHighlight)
                        {
                            cell.Select(false, 1);
                        }
                        else
                        {
                            cell.Select(false, 0);
                        }
                        cell.Draw(ref g);
                    }
                }
            }
        }
        public void HighlightNoteNum(ref Graphics g, int noteNum, SolidBrush brush)
        {            
            foreach (var cell in this.Cells)
            {
                if (cell.NotesList.Contains(noteNum))
                {
                    //cell.Draw(ref g);
                    cell.HighlightNoteNum(ref g, noteNum, brush);
                }
            }
        }
        public void HighlightNoteNum(ref Graphics g, int noteNum, int row, int col, SolidBrush brush)
        {
            SudokuCell cell = GetCell(row, col);
            cell.HighlightNoteNum(ref g, noteNum, brush);
        }
        public void SetNoteVisible(bool bNoteVisible)
        {            
            foreach (var cell in this.Cells)
            {
                cell.IsNoteVisible = bNoteVisible;
            }
        }
        public void Select()
        {
            if (IsSelected())
                GetCurrentCell().Select();
        }
        public void Select(SudokuCell cell)
        {
            //select the cell at (row, col)
            //cell.Select(); <- may not work, may be a copy
            GetCell(cell.Row, cell.Col).Select();
            UpdateCurrentPos(cell.Row, cell.Col);
        }
        public void Select(int row, int col)
        {
            //select the cell at (row, col)
            GetCell(row, col).Select();
            UpdateCurrentPos(row, col);
        }
        public void Unselect()
        {
            foreach (var cell in this.Cells)
            {
                cell.Unselect();
            }
            ResetCurrentPos();
        }
        public void Draw(ref Graphics g)
        {
            foreach (var cell in this.Cells)
            {
                cell.Draw(ref g);
            }
        }
        public void CheckFault()
        {
            foreach (var c in this.Cells)
            {
                c.CheckFault();
            }
        }

        public void Lock()
        {
            foreach (var cell in this.Cells)
            {
                cell.Lock();
            }
        }
        public void Unlock()
        {
            foreach (var cell in this.Cells)
            {
                cell.Unlock();
            }
        }
        public void Erase()
        {
            foreach (var cell in this.Cells)
            {
                cell.Erase();
            }
        }
        public void Reset()
        {
            foreach (var cell in this.Cells)
            {
                cell.Reset();
            }
        }
        public void SetNumber(int num, int i, int j)
        {
            matrix[i, j].SetNumber(num);
        }             
        public void RemoveNote(int num, int i, int j)
        {             
            matrix[i, j].RemoveNote(num);
        }
        public bool Load(string strSudoku)
        {
            //load the sudoku string
            string s = strSudoku.Trim();
            if (s.Length == 81 && s.All(c => c >= '0' && c <= '9'))
            {
                Reset();
                ResetCurrentPos();
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        SetNumber(Int32.Parse(s[i * 9 + j].ToString()), i, j);                        
                    }
                }
                return true;
            }
            return false;
        }
        public override string ToString()
        {
            //return the sudoku string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    sb.Append(matrix[i, j].Num);
                }
            }
            return sb.ToString();
        }
        public void ComputeNoteList()
        {
            foreach (var cell in this.Cells)
            {
                cell.ComputeNoteList();
            }
        }
        public List<SudokuCell> FindEmptyCells()
        {
            List<SudokuCell> lst = new List<SudokuCell>();
            foreach (var cell in this.Cells)
            {
                if (cell.Num == 0)
                {
                    lst.Add(cell);
                }
            }
            return lst;
        }
        public List<SudokuCell> FindEmptyCellsWithNNotes(int n)
        {
            List<SudokuCell> lst = new List<SudokuCell>();
            foreach (var cell in this.Cells)
            {
                if (cell.Num == 0 && cell.CountNotes(n))
                {
                    lst.Add(cell);
                }
            }
            return lst;
        }
        public bool IsValid()
        {
            foreach(var house in houses)
            {
                if (!house.IsValid()) return false;
            }
            return true;
        }
        private bool solve(ref List<SudokuCell> emptySpaces, int head = 0)
        {
            if (head >= emptySpaces.Count) return true;
            SudokuCell p = emptySpaces[head];
            for (int num = 1; num <= 9; num++)
            {
                if (p.CheckValidNum(num))
                {
                    p.SetNumber(num);
                    if (solve(ref emptySpaces, head + 1)) return true;
                    p.SetNumber(0);
                }
            }
            return false;
        }
        private int solve(ref List<SudokuCell> emptySpaces, ref int solnCnt, int head = 0)
        {            
            if (head >= emptySpaces.Count)
            {
                solnCnt++;
                return solnCnt;
            }
            SudokuCell p = emptySpaces[head];
            for (int num = 1; num <= 9; num++)
            {
                if (p.CheckValidNum(num))
                {
                    p.SetNumber(num);
                    if( solve(ref emptySpaces, ref solnCnt, head + 1)>1)
                    {
                        return solnCnt;
                    }
                    p.SetNumber(0);
                }
            }
            return solnCnt;
        }

        public bool Solve()
        {
            //the grid must be valid: no duplicate number in any house
            //not check multiple solution
            //Case1: No solution -> false
            //Case2: Unique solution -> true
            //Case3: Multiple solution -> true
            List<SudokuCell> emptySpaces = FindEmptyCells();
            if(solve(ref emptySpaces, 0))
            {
                return true;
            }
            return false;
        }
        public int FindSolutions()
        {
            SudokuGrid gridCopy = this.Copy();
            if (!gridCopy.IsValid()) return 0;

            List<SudokuCell> emptySpaces = gridCopy.FindEmptyCells();
            int solnCnt = 0;
            gridCopy.solve(ref emptySpaces, ref solnCnt, 0);
            return solnCnt;
        }
        public bool IsUnique()
        {
            return FindSolutions() == 1;
        }
        public bool IsEmpty()
        {
            foreach (var cell in this.Cells)
            {
                if (cell.IsEmpty())
                {
                    return true;
                }
            }
            return false;
        }
        public bool IsCompleted()
        {
            //no empty cell and the grid is valid
            if (IsEmpty()) return false;
            if (!IsValid()) return false;
            return true;
        }
        public bool IsSelected()
        {
            //check if any cell is selected
            return currRow>=0 && currCol>=0;
        }
        public bool IsSelected(int row, int col)
        {
            //check if the cell (row,col) is selected
            return currRow == row && currCol == col;
        }
        public void UpdateCurrentPos(int row, int col)
        {
            this.currRow = row;
            this.currCol = col;
        }
        public void ResetCurrentPos()
        {
            this.currRow = -1;
            this.currCol = -1;
        }
        public SudokuCell GetCurrentCell()
        {
            if (IsSelected())
            {
                return matrix[currRow, currCol];
            }
            return null;
        }
        public IEnumerator<SudokuCell> GetEnumerator()
        {
            foreach (var c in this.Cells)
            {
                yield return c;
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        public void Apply(SudokuChange c)
        {
            if(c.Type == SudokuChangeType.SetNum)
            {
                this.SetNumber(c.Num, c.Row, c.Col);
                var houses = this.GetCell(c.Row, c.Col).SudokuHouses;
                foreach (var house in houses)
                {
                    house.RemoveNote(c.Num);
                }
            }else if (c.Type == SudokuChangeType.RemoveNote)
            {
                this.RemoveNote(c.NoteNum, c.Row, c.Col);
            }
        }
    }
}
