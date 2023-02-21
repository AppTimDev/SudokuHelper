using Common;
using Common.Extensions;
using SudokuHelper.Algorithm;
using SudokuHelper.Sudoku;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SudokuHelper.FormDir
{
    public partial class SudokuForm : Form
    {
        Stack<SudokuGrid> stackSteps;
        SudokuGrid grid;
        SudokuGrid gridSaved = null;
        bool bModeNote = false;
        bool bNoteVisible = true;
        private Button[] btnNumbers;
        private Bitmap bmp; //use an in-memory bitmap to Persistent graphics
        private Graphics g;
        public SudokuForm()
        {
            InitializeComponent();
            stackSteps = new Stack<SudokuGrid>();
            //form setting
            this.DoubleBuffered = true;
            this.WindowState = FormWindowState.Maximized;
            //this.Width = 1600;
            //this.Height = 1000;
            
            //sudoku grid container panel
            this.panel1.Width = this.panel1.Height = Sudoku.Sudoku.GridSize + 20;
            this.panel1.Top = 10;
            this.panel1.Left = 5;
            this.panel1.Visible = true;

            bmp = new Bitmap(this.panel1.Width, this.panel1.Height);
            g = Graphics.FromImage(bmp);
            //g = panel1.CreateGraphics();
            g.SetHighQulity();

            InitializeSudoku();
            InitializeBtnNumber();
            btnRedo.Enabled = false;
            btnRollback.Enabled = false;

            //testing sample
            this.tbSudoku.Text = "006030708030000001200000600100350006079040150500017004002000007600000080407060200";
        }
        public void InitializeSudoku()
        {
            grid = new SudokuGrid();
            Sudoku.Sudoku.DrawGridLines(ref g);
        }
        
        public void btnNumber_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.Enabled = false;
            if (grid.IsSelected())
            {
                UpdateSudokuCell(Int32.Parse(btn.Text));
            }
            btn.Enabled = true;
        }
        public void InitializeBtnNumber()
        {
            this.btnNumbers = new Button[9];
            for (int i = 0; i < 9; i++)
            {
                btnNumbers[i] = new Button();

                btnNumbers[i].Name = "btnNumber-" + (i+1).ToString();
                btnNumbers[i].Text = (i + 1).ToString();
                btnNumbers[i].Location = new System.Drawing.Point(60*i, 600);
                btnNumbers[i].Size = new System.Drawing.Size(60, 60);
                btnNumbers[i].UseVisualStyleBackColor = true;
                btnNumbers[i].TabStop = false;
                btnNumbers[i].Click += new System.EventHandler(this.btnNumber_Click);
                this.Controls.Add(btnNumbers[i]);
            }
        }

        public void CheckDuplicateInHouse(ref Graphics g, ref SudokuCell cell)
        {
            //Check Fault: check any duplicate number in house
            foreach (var house in cell.SudokuHouses)
            {
                house.CheckFault();
                house.Draw(ref g);
            }
        }
        public void UpdateSudokuCell(int num)
        {
            SudokuCell cell = grid.GetCurrentCell();
            if (!cell.IsLocked)
            {
                //num: range 0, 1-9
                //checking for 1-9
                grid.Unselect();
                if (num > 0)
                {

                    if (!this.bModeNote)
                    {
                        if (cell.Num != num)
                        {
                            SaveStep();
                        }
                        //normal fill                        
                        //grid.HighlightSelectedNumber(ref g, cell.Num, false);
                        cell.SetNumber(num);

                        //check any duplicate number in house
                        CheckDuplicateInHouse(ref g, ref cell);

                        grid.HighlightSelectedNumber(ref g, cell.Num, true);                        
                    }
                    else
                    {
                        SaveStep();
                        //take notes
                        cell.IsFault = false; //reset this field
                        cell.IsNote = true;
                        //cell.Notes[num] = !cell.Notes[num];
                        cell.FlipNote(num);
                        //take notes, value of num is 0
                        cell.SetNumber(0);
                        CheckDuplicateInHouse(ref g, ref cell);
                    }
                }
                else
                {
                    if(cell.Num != num)
                    {
                        SaveStep();
                    }
                    //num is 0
                    cell.Erase();
                    CheckDuplicateInHouse(ref g, ref cell);
                }
                grid.Select(cell);
                //grid.Select(cell);
                grid.Draw(ref g);
                Repaint();
            }
        }

        public void Repaint()
        {
            Sudoku.Sudoku.DrawGridLines(ref g);
            this.panel1.Invalidate();
        }
        //public void FindAllCellNotes()
        //{
        //    grid.ComputeNoteList();
        //    grid.Draw(ref g);
        //    Repaint();
        //    //SudokuCell cell;
        //    //for (int i = 0; i < 9; i++)
        //    //{
        //    //    for (int j = 0; j < 9; j++)
        //    //    {
        //    //        cell = grid[i, j];
        //    //        if (SetCellNotes(ref cell))
        //    //        {
        //    //            cell.Draw(ref g);
        //    //        }
        //    //    }
        //    //}

        //}
        //public bool SetCellNotes(ref SudokuCell cell)
        //{
        //    if(!cell.IsLocked && cell.Num == 0)
        //    {
        //        cell.IsNote = true;
        //        for (int i = 0; i <= 9; i++)
        //        {
        //            cell.Notes[i] = true;
        //        }
        //        for (int i = 0; i < 9; i++)
        //        {
        //            if (grid[i, cell.Col].Num > 0)
        //            {
        //                cell.Notes[grid[i, cell.Col].Num] = false;
        //            }
        //        }
        //        for (int i = 0; i < 9; i++)
        //        {
        //            if (grid[cell.Row, i].Num > 0)
        //            {
        //                cell.Notes[grid[cell.Row, i].Num] = false;
        //            }
        //        }
        //        //Block
        //        //0, 1, 2
        //        //3, 4, 5
        //        //6, 7, 8
        //        foreach (var c in cell.SudokuBlock)
        //        {
        //            if (c.Num > 0)
        //            {
        //                cell.Notes[c.Num] = false;
        //            }
        //        }
        //        return true;
        //    }
        //    return false;
        //}

        public void SelectSudokuCell(int row, int col)
        {
            if (!grid.IsSelected(row, col))
            {
                SudokuCell cell;

                //unselect previous active cells
                if (grid.IsSelected())
                {
                    cell = grid.GetCurrentCell();
                    grid.Unselect();
                    //foreach (var house in cell.SudokuHouses)
                    //{
                    //    house.Unselect();
                    //    house.Draw(ref g);
                    //}
                    grid.HighlightSelectedNumber(ref g, cell.Num, false);
                }
                //update the position of the active cell
                grid.UpdateCurrentPos(row, col);

                //select current active cell
                cell = grid.GetCurrentCell();
                foreach (var house in cell.SudokuHouses)
                {
                    house.Select(false, 2);
                    //house.Draw(ref g);
                }
                grid.HighlightSelectedNumber(ref g, cell.Num, true);
                cell.Select(true, 0);
                //cell.Draw(ref g);
                grid.Draw(ref g);

                Repaint();                
            }
        }
        public void OnUserControl_KeyUp(object sender, KeyEventArgs e)
        {
            if (grid.IsSelected())
            {
                int num = 0;
                bool bNum = true;
                bool bChangeDirection = false;
                if (e.KeyCode == Keys.NumPad0 || e.KeyCode == Keys.D0 || e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back)
                {
                    //will erase cell
                    num = 0;
                }
                else if (e.KeyCode == Keys.NumPad1 || e.KeyCode == Keys.D1)
                {
                    num = 1;
                }
                else if (e.KeyCode == Keys.NumPad2 || e.KeyCode == Keys.D2)
                {
                    num = 2;
                }
                else if (e.KeyCode == Keys.NumPad3 || e.KeyCode == Keys.D3)
                {
                    num = 3;
                }
                else if (e.KeyCode == Keys.NumPad4 || e.KeyCode == Keys.D4)
                {
                    num = 4;
                }
                else if (e.KeyCode == Keys.NumPad5 || e.KeyCode == Keys.D5)
                {
                    num = 5;
                }
                else if (e.KeyCode == Keys.NumPad6 || e.KeyCode == Keys.D6)
                {
                    num = 6;
                }
                else if (e.KeyCode == Keys.NumPad7 || e.KeyCode == Keys.D7)
                {
                    num = 7;
                }
                else if (e.KeyCode == Keys.NumPad8 || e.KeyCode == Keys.D8)
                {
                    num = 8;
                }
                else if (e.KeyCode == Keys.NumPad9 || e.KeyCode == Keys.D9)
                {
                    num = 9;
                }
                else
                {
                    bNum = false;
                }
                if (bNum)
                {
                    UpdateSudokuCell(num);
                }
                else
                {
                    int row = grid.CurrRow;
                    int col = grid.CurrCol;
                    if (e.KeyCode == Keys.Up)
                    {
                        if (row > 0)
                        {
                            row = row - 1;
                            bChangeDirection = true;
                        }
                    }
                    else if (e.KeyCode == Keys.Down)
                    {
                        if (row >= 0 && row < 8)
                        {
                            row = row + 1;
                            bChangeDirection = true;
                        }
                    }
                    else if (e.KeyCode == Keys.Left)
                    {
                        if (col > 0)
                        {
                            col = col - 1;
                            bChangeDirection = true;
                        }
                    }
                    else if (e.KeyCode == Keys.Right)
                    {
                        if (col >= 0 && col < 8)
                        {
                            col = col + 1;
                            bChangeDirection = true;
                        }
                    }
                    if (bChangeDirection)
                    {
                        SelectSudokuCell(row, col);
                    }
                }
            }
        }
        private void btnNoteVisible_Click(object sender, EventArgs e)
        {
            bNoteVisible = !bNoteVisible;            
            if (bNoteVisible)
            {
                btnNoteVisible.Text = "Note Visible(ON)";
            }
            else
            {
                btnNoteVisible.Text = "Note Visible (OFF)";
            }
            grid.SetNoteVisible(bNoteVisible);
            grid.Draw(ref g);
            Repaint();
        }
        private void btnNote_Click(object sender, EventArgs e)
        {
            bModeNote = !bModeNote;
            if (bModeNote)
            {
                btnNote.Text = "Note Mode (ON)";
            }
            else
            {
                btnNote.Text = "Note Mode (OFF)";
            }
        }

        private void btnHelpNote_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.Enabled = false;
            grid.ComputeNoteList();
            grid.Draw(ref g);
            Repaint();
            btn.Enabled = true;
        }
        private void btnLock_Click(object sender, EventArgs e)
        {
            grid.Lock();
            grid.Draw(ref g);
            Repaint();
        }
        private void btnUnlock_Click(object sender, EventArgs e)
        {
            grid.Unlock();
            grid.Draw(ref g);
            Repaint();
        }
        private void btnEraseAll_Click(object sender, EventArgs e)
        {
            grid.ResetCurrentPos();
            grid.Erase();
            grid.Unselect();
            grid.Draw(ref g);
            Repaint();
        }

        private void btnResetAll_Click(object sender, EventArgs e)
        {
            grid.ResetCurrentPos();
            grid.Reset();
            grid.Draw(ref g);
            Repaint();
        }
        private void btnReset_Click(object sender, EventArgs e)
        {
            if (grid.IsSelected())
            {
                SudokuCell cell = grid.GetCurrentCell();
                grid.Unselect();
                cell.Reset();
                CheckDuplicateInHouse(ref g, ref cell);
                grid.Select(cell);
                grid.Draw(ref g);
                Repaint();
            }
        }
        private void btnErase_Click(object sender, EventArgs e)
        {
            if (grid.IsSelected())
            {
                UpdateSudokuCell(0);
            }
        }
        private void btnLockSelected_Click(object sender, EventArgs e)
        {
            if (grid.IsSelected())
            {
                SudokuCell cell = grid.GetCurrentCell();
                cell.Lock();
                cell.Draw(ref g);
                Repaint();
            }
        }

        private void btnUnlockSelected_Click(object sender, EventArgs e)
        {
            if (grid.IsSelected())
            {
                SudokuCell cell = grid.GetCurrentCell();
                cell.Unlock();
                cell.Draw(ref g);
                Repaint();
            }
        }
        private void panel1_Click(object sender, EventArgs e)
        {
            Point point = panel1.PointToClient(Cursor.Position);
            int row = point.Y / Sudoku.Sudoku.CellSize;
            int col = point.X / Sudoku.Sudoku.CellSize;
            SelectSudokuCell(row, col);
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {            
            //to persist the drawn graphics
            //must repaint them everytime when the form or control is painted
            e.Graphics.DrawImage(bmp, e.ClipRectangle, e.ClipRectangle, GraphicsUnit.Pixel);
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            int[,] data = { 
                {3,0,0,0,0,1,0,7,0}, 
                {2,9,0,0,5,0,8,0,0}, 
                {0,0,0,6,0,0,0,0,4}, 

                {0,0,1,0,0,7,0,9,0}, 
                {4,0,0,0,2,0,0,0,3}, 
                {0,8,0,3,0,0,4,0,0}, 

                {5,0,0,0,0,8,0,0,0},
                {0,0,7,0,3,0,0,1,2},
                {0,4,0,9,0,0,0,0,5},
                //{0,0,0,0,0,0,0,0,0},
            };
            grid.ResetCurrentPos();
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    grid[i, j].Reset();
                    if (data[i, j] > 0)
                    {
                        grid[i, j].IsLocked = true;
                        grid.SetNumber(data[i, j], i, j);
                    }                    
                }
            }
            grid.Draw(ref g);
            Repaint();
        }
        private void log(string message, bool bLogTextbox = true, bool bLogFile = false)
        {
            FormLogger.Log(message, tbLogger, bLogTextbox, bLogFile);
        }

        private void btnSolver_Click(object sender, EventArgs e)
        {
            string msg;
            if (grid.IsUnique())
            {
                if (grid.Solve())
                {
                    grid.Draw(ref g);
                    Repaint();
                    msg = "The sudoku is solved.";
                    log(msg, true, false);
                }
                else
                {
                    MessageBox.Show("Not solvable!!");
                }
            }
            else
            {
                msg = "The sudoku is not solvable! It has multiple solutions or no solution.";
                log(msg, true, false);
            }
        }
        private bool CanFindNextStep()
        {
            //must have empty cell
            //grid is valid
            //grid is unique
            if (grid.IsCompleted())
            {
                //no empty cell and the grid is valid
                log("The sudoku is solved!");
                return false;
            }
            if (!grid.IsValid())
            {
                log("The sudoku is invalid now. Cannot proceed!");
                return false;
            }
            if (!grid.IsUnique())
            {
                log("There is no unique solution!");
                return false;
            }
            return true;
        }
        private bool CheckComplete()
        {
            if (grid.IsCompleted())
            {
                log("The sudoku is solved.");
                return true;
            }
            return false;
        }
        private bool CheckUnique()
        {
            int solnCnt = grid.FindSolutions();
            string msg;
            if (solnCnt == 1)
            {
                msg = "There is an unique solution.";
            }
            else if (solnCnt > 1)
            {
                msg = "There are multiple solutions. The sudoku is not solvable!";
            }
            else
            {
                msg = "There is no solution. The sudoku is not solvable!";
            }
            log(msg, true, false);
            return solnCnt == 1;
        }
        private void btnCheckUnique_Click(object sender, EventArgs e)
        {
            CheckUnique();
        }
        private void RemoveAllSteps()
        {
            stackSteps.Clear();
            btnRedo.Enabled = false;
        }
        private void SaveStep()
        {
            stackSteps.Push(grid.Copy());
            btnRedo.Enabled = true;
        }
        private void btnRedo_Click(object sender, EventArgs e)
        {
            if(stackSteps.Count > 0)
            {
                grid = stackSteps.Pop();
                grid.Select();
                grid.Draw(ref g);
                Repaint();
            }
            if (stackSteps.Count == 0)
                btnRedo.Enabled = false;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            gridSaved = grid.Copy();
            log("The sudoku is saved.");
            btnRollback.Enabled = true;
        }

        private void btnRollback_Click(object sender, EventArgs e)
        {
            if (gridSaved != null)
            {
                grid = gridSaved.Copy();
                grid.Draw(ref g);
                Repaint();
                log("Rollback!");
                RemoveAllSteps();
            }
            else
            {
                log("It is not saved before!");
            }
        }

        private void btnClearLog_Click(object sender, EventArgs e)
        {
            try
            {
                if (tbLogger != null)
                {
                    tbLogger.Text = "";
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        public List<SudokuChange> Analyze()
        {
            List<SudokuChange> changes;
            SudokuGrid gridCopy = grid.Copy();
            List<ISudokuAlgorithm> algoList = new List<ISudokuAlgorithm>();
            ISudokuAlgorithm alg = new AlgoSingleCandidate();
            algoList.Add(alg);

            algoList.Add(new AlgoSinglePosition());

            foreach (var algo in algoList)
            {
                changes = algo.Analyze(gridCopy);
                if (changes.Count > 0)
                {
                    return changes;
                }
            }
            return null;
        }
        private void btnHints_Click(object sender, EventArgs e)
        {
            if (!CanFindNextStep()) return;

            List<SudokuChange> changes = Analyze();
            if (changes != null && changes.Count > 0)
            {
                SudokuChange c = changes[0];
                grid.Unselect();
                grid.Select(c.Row, c.Col);
                grid.Draw(ref g);
                Repaint();
                return;
            }
            else
            {
                log("Cannot find next step!");
            }
        }
        private void btnFindNext_Click(object sender, EventArgs e)
        {
            if (!CanFindNextStep()) return;
            List<SudokuChange> changes = Analyze();
            if (changes != null && changes.Count > 0)
            {
                SudokuChange c = changes[0];
                grid.Unselect();
                grid.Apply(c);
                grid.Select(c.Row, c.Col);
                grid.Draw(ref g);
                Repaint();
                return;
            }
            else
            {
                log("Cannot find next step!");
            }
        }

        private void btnAlgo_Click(object sender, EventArgs e)
        {
            if (!CanFindNextStep()) return;
            bool bContinue = true;
            while (bContinue)
            {
                bContinue = false;

                List<SudokuChange> changes = Analyze();
                if (changes != null && changes.Count > 0)
                {
                    SudokuChange c = changes[0];
                    grid.Unselect();
                    grid.Apply(c);
                    grid.Select(c.Row, c.Col);
                    grid.Draw(ref g);
                    Repaint();
                    bContinue = true;
                }
            }
            if (grid.IsCompleted())
            {
                log("The sudoku is solved!");
            }
            else
            {
                log("Cannot find next step!");
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            if (grid.Load(tbSudoku.Text))
            {
                grid.Lock();
                grid.Draw(ref g);
                Repaint();
                RemoveAllSteps();
            }
            else
            {
                log("The sudoku string is not valid!! It should contain 81 characters from 0 to 9.");
            }
        }
        private void btnOutputSudo_Click(object sender, EventArgs e)
        {
            if (!grid.IsUnique())
            {
                log("There is no solution. The sudoku is not solvable!");
                return;
            }
            log(grid.ToString());
        }
        private void btnSingleCandidate_Click(object sender, EventArgs e)
        {
            SudokuGrid gridCopy = grid.Copy();
            AlgoSingleCandidate algo = new AlgoSingleCandidate();
            List<SudokuChange> changes = algo.Analyze(gridCopy);
            if (changes.Count > 0)
            {
                grid.Unselect();
                foreach (var c in changes)
                {
                    //grid.Apply(c);
                    grid.Select(c.Row, c.Col);
                    log($"{c.Message}");
                }
                grid.Draw(ref g);
                Repaint();
            }
            else
            {
                log($"Cannot Apply {algo.AlgorithmName}!");
            }
        }

        private void btnSinglePosition_Click(object sender, EventArgs e)
        {
            SudokuGrid gridCopy = grid.Copy();
            AlgoSinglePosition algo = new AlgoSinglePosition();
            List<SudokuChange> changes = algo.Analyze(gridCopy);
            if (changes.Count > 0)
            {
                grid.Unselect();
                foreach (var c in changes)
                {
                    grid.Select(c.Row, c.Col);
                    log($"{c.Message}");
                }
                grid.Draw(ref g);
                Repaint();
            }
            else
            {
                log($"Cannot Apply {algo.AlgorithmName}!");
            }
        }

        private void btnCandidateLines_Click(object sender, EventArgs e)
        {
            SudokuGrid gridCopy = grid.Copy();
            AlgoCandidateLines algo = new AlgoCandidateLines();
            List<SudokuChange> changes = algo.Analyze(gridCopy);
            if (changes.Count > 0)
            {
                grid.Unselect();
                foreach (var c in changes)
                {
                    grid.Select(c.Row, c.Col);
                    log($"{c.Message}");
                }
                grid.Draw(ref g);
                Repaint();
            }
            else
            {
                log($"Cannot Apply {algo.AlgorithmName}!");
            }
        }

        private void btnNakedPair_Click(object sender, EventArgs e)
        {
            AlgoNakedPair algo = new AlgoNakedPair();
            
            //SudokuGrid gridCopy = grid.Copy();
            //List<SudokuChange> changes = algo.Analyze(gridCopy);            
            List<SudokuChange> changes = algo.Analyze(grid);
            if (changes.Count > 0)
            {
                grid.Unselect();
                foreach (var c in changes)
                {
                    grid.Select(c.Row, c.Col);
                    log($"{c.Message}");
                }
                grid.Draw(ref g);
                Repaint();
            }
            else
            {
                log($"Cannot Apply {algo.AlgorithmName}!");
            }
        }

        private void btnHighlightNote_Click(object sender, EventArgs e)
        {
            for (int i = 1; i <= 4; i++)
            {
                grid.HighlightNoteNum(ref g, i, Sudoku.Sudoku.brushLightGreen);
            }
            for (int i = 5; i <= 9; i++)
            {
                grid.HighlightNoteNum(ref g, i, Sudoku.Sudoku.brushLightPink);
            }
            Repaint();
        }
    }
}
