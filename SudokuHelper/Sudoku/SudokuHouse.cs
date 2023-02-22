using System.Collections;
using System.Collections.Generic;
using System.Drawing;

namespace SudokuHelper.Sudoku
{
    //House: Row, Column or Block
    public class SudokuHouse : IEnumerable<SudokuCell>
    {
        public List<SudokuCell> Cells { get; set; }

        public SudokuHouse()
        {
            this.Cells = new List<SudokuCell>();
        }
        public void Add(ref SudokuCell cell)
        {
            this.Cells.Add(cell);
        }
        public void HighlightSelectedNumber(ref Graphics g, int num, bool bHighlight)
        {
            if (num > 0 && num<=9)
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
        public void Select(bool IsSelected, byte SelectMode)
        {
            foreach(var cell in this.Cells)
            {
                cell.Select(IsSelected, SelectMode);
            }
        }
        public void Unselect()
        {
            foreach (var cell in this.Cells)
            {
                cell.Unselect();
            }
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
        public void RemoveNote(int num)
        {
            foreach (var c in this.Cells)
            {
                c.RemoveNote(num);
            }
        }
        public IEnumerator<SudokuCell> GetEnumerator()
        {
            foreach(var c in this.Cells)
            {
                yield return c;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
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
            byte[] cnt = new byte[10];
            for (int i = 0; i <= 9; i++)
            {
                cnt[i] = 0;
            }
            foreach(var cell in Cells)
            {
                if(cell.Num > 0)
                {
                    cnt[cell.Num]++;
                    if (cnt[cell.Num] > 1) return false;
                }
            }
            return true;
        }
    }
}
