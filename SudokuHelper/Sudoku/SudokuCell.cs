using System.Collections.Generic;
using System.Drawing;

namespace SudokuHelper.Sudoku
{
    public class SudokuCell
    {
        public SudokuBlock SudokuBlock { get; set; }
        public SudokuRow SudoRow { get; set; }
        public SudokuCol SudoCol { get; set; }
        public List<SudokuHouse> SudokuHouses { get; set; } = new List<SudokuHouse>();

        private int num = 0;
        public int Num
        {
            get
            {
                return num;
            }
            //set
            //{
            //    if (value > 0 && value <= 9)
            //    {
            //        num = value;
            //    }
            //    else
            //    {
            //        num = 0;
            //    }
            //}
        }
        public int Block { get; set; }
        public int Row { get; set; }
        public int Col { get; set; }
        public byte SelectMode { get; set; } = 0;
        public bool IsSelected { get; set; } = false;
        public bool IsLocked { get; set; } = false;
        public bool IsFault { get; set; } = false;
        public bool IsNote { get; set; } = false;
        public bool IsNoteVisible { get; set; } = true;
        //public bool[] Notes { get; set; }
        public List<int> NotesList { get; set; }

        public SudokuCell()
        {
            //Notes = new bool[10];
            //for (int i = 0; i < Notes.Length; i++)
            //{
            //    Notes[i] = false;
            //}
            NotesList = new List<int>();
        }
        public void CopyTo(SudokuCell cell)
        {
            cell.SetNumber(num);
            cell.IsLocked = IsLocked;
            cell.IsFault = IsFault;
            cell.IsNote = IsNote;
            cell.IsNoteVisible = IsNoteVisible;
            cell.SelectMode = SelectMode;
            cell.IsSelected = IsSelected;
            foreach(var note in NotesList)
            {
                cell.AddNote(note);
            }
            //for (int i = 0; i < Notes.Length; i++)
            //{
            //    cell.Notes[i] = Notes[i];
            //}
        }
        public void Erase()
        {
            if (!this.IsLocked)
            {
                IsFault = false;
                IsNote = false;
                RemoveAllNotes();
                //for (int i = 0; i < Notes.Length; i++)
                //{
                //    Notes[i] = false;
                //}
                this.num = 0;
            }
        }
        public void Reset()
        {
            IsLocked = false;
            IsFault = false;
            IsNote = false;
            RemoveAllNotes();
            //for (int i = 0; i < Notes.Length; i++)
            //{
            //    Notes[i] = false;
            //}
            this.num = 0;
            IsSelected = false;
            SelectMode = 0;
        }
        public void Select(bool IsSelected, byte SelectMode)
        {
            //case 1: true, 0 the cell selected
            //case 2: false, 0 the cell not affected
            //case 3: false, 1 the cell of the house of the selected cell, its number is not matched with the selected cell
            //case 4: false, 2 the cell of the house of the selected cell, its number is matched with the selected cell
            this.IsSelected = IsSelected;
            this.SelectMode = SelectMode;
        }
        public void Select()
        {
            //case 1: true, 0 the cell selected
            IsSelected = true;
            SelectMode = 0;
        }
        public void Unselect()
        {
            //case 2: false, 0 the cell not affected
            SelectMode = 0;
            IsSelected = false;
        }
        public void Lock()
        {
            if (num > 0)
            {
                IsLocked = true;
            }
        }
        public void Unlock()
        {
            IsLocked = false;
        }
        public void Draw(ref Graphics g)
        {
            var cell = this;
            Sudoku.DrawSudokuCell(ref g, ref cell);
        }
        public void HighlightNoteNum(ref Graphics g, int noteNum, SolidBrush brush)
        {
            Sudoku.HighlightNoteNum(g, noteNum, Row, Col, brush);
        }
        public bool CheckFault()
        {
            //check if there is repeat number in the houses
            this.IsFault = false;
            if (!IsLocked && num > 0)
            {                
                foreach (var house in this.SudokuHouses)
                {
                    foreach (var cell in house.Cells)
                    {
                        if (cell.num == num)
                        {
                            if(cell.Row!=Row || cell.Col != Col)
                            {
                                this.IsFault = true;
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }
        public bool CheckValidNum(int num)
        {
            // check num: 1-9 is a valid num to fill
            //only check there is not same number in the house of this cell
            if (num > 0)
            {
                foreach (var house in this.SudokuHouses)
                {
                    foreach (var cell in house.Cells)
                    {
                        if (cell.num == num)
                        {
                            if (cell.Row != Row || cell.Col != Col)
                            {
                                return false;
                            }
                        }
                    }
                }
                return true;
            }
            return false;
        }
        public bool CountNotes(int n)
        {
            return NotesList.Count == n;
        }
        public void ComputeNoteList()
        {
            if (!this.IsLocked && this.Num == 0)
            {
                this.IsNote = true;
                NotesList = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
                foreach (var house in this.SudokuHouses)
                {
                    foreach (var cell in house.Cells)
                    {
                        if (cell.num > 0 && NotesList.Contains(cell.num))
                        {
                            NotesList.Remove(cell.num);
                        }
                    }
                }
            }
            else
            {
                this.IsNote = false;
                RemoveAllNotes();
            }
        }
        public void AddNote(int num)
        {
            if (!NotesList.Contains(num))
            {
                NotesList.Add(num);
                IsNote = true;
            }
        }
        public void RemoveAllNotes()
        {
            IsNote = false;
            if (NotesList.Count > 0)
            {
                NotesList.Clear();
            }
        }
        public void RemoveNote(int num)
        {
            if (NotesList.Contains(num))
            {
                NotesList.Remove(num);
            }
            if (NotesList.Count == 0)
            {
                this.IsNote = false;
            }            
        }
        public void FlipNote(int num)
        {
            if (NotesList.Contains(num))
            {
                NotesList.Remove(num);
                if (NotesList.Count == 0)
                {
                    this.IsNote = false;
                }
            }
            else
            {
                NotesList.Add(num);
                IsNote = true;
            }
        }

        public void SetNumber(int num)
        {            
            if (num > 0 && num <= 9)
            {
                this.num = num;
            }
            else
            {
                this.num = 0;
            }
        }
        public bool IsSameBlock(SudokuCell cell)
        {
            return cell.Block == Block;
        }
        public bool IsSameRow(SudokuCell cell)
        {
            return cell.Row == Row;
        }
        public bool IsSameCol(SudokuCell cell)
        {
            return cell.Col == Col;
        }

        public bool HasSameNotes(SudokuCell cell)
        {
            //compare notes with other cells
            //if both have same unmbers in notes, return true
            //assume both cells do not contain duplicate note number
            if(NotesList.Count != cell.NotesList.Count) return false;
            foreach (var note in cell.NotesList)
            {
                if(!NotesList.Contains(note)) return false;
            }
            return true;
        }
    }
}
