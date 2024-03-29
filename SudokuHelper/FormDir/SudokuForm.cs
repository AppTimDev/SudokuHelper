﻿using Common;
using Common.Extensions;
using SudokuHelper.Algorithm;
using SudokuHelper.Sudoku;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SudokuHelper.FormDir
{
    public partial class SudokuForm : Form
    {
        Stack<SudokuGrid> stackSteps;
        SudokuGrid grid;
        SudokuGrid gridCopy;
        SudokuGrid gridSaved = null;
        bool bModeNote = false;
        bool bNoteVisible = true;
        private Button[] btnNumbers;
        private Bitmap bmp; //use an in-memory bitmap to Persistent graphics
        private Graphics g;
        private int currDataLine = -1; //for load the next line of the data to the grid
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
            InitializeAlgoTreeview();
            InitializeSudoku();
            InitializeBtnNumber();
            btnRedo.Enabled = false;
            btnRollback.Enabled = false;

            //testing sample
            this.tbSudoku.Text = "006030708030000001200000600100350006079040150500017004002000007600000080407060200";
        }
        private void InitializeSudoku()
        {
            grid = new SudokuGrid();
            Sudoku.Sudoku.DrawGridLines(ref g);
        }
        private void InitializeAlgoTreeview()
        {
            this.algoTreeView.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.algoTreeView_AfterCheck);
            this.algoTreeView.DoubleClick += new System.EventHandler(this.algoTreeView_DoubleClick);
            this.algoTreeView.MouseUp += new System.Windows.Forms.MouseEventHandler(this.algoTreeView_MouseUp);

            this.algoTreeView.CheckBoxes = true;
            this.algoTreeView.HideSelection = false;
            this.algoTreeView.HotTracking = true;
            TreeNode rootNode = this.algoTreeView.Nodes.Add("Algorithms");
            foreach (var algoName in AlgoNames.ToList())
            {
                rootNode.Nodes.Add(algoName);
            }
            this.algoTreeView.ExpandAll();
        }
        private void algoTreeView_DoubleClick(object sender, EventArgs e)
        {
            var localPosition = algoTreeView.PointToClient(Cursor.Position);
            var hitTestInfo = algoTreeView.HitTest(localPosition);
            if (hitTestInfo.Location == TreeViewHitTestLocations.StateImage)
                return;
        }
        private void algoTreeView_MouseUp(object sender, MouseEventArgs e)
        {
            //unselect item when clicking outside of tree
            this.algoTreeView.SelectedNode = this.algoTreeView.GetNodeAt(this.algoTreeView.PointToClient(Control.MousePosition));
        }
        private void algoTreeView_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Action != TreeViewAction.Unknown)
            {
                try
                {
                    e.Node.TreeView.BeginUpdate();
                    CheckChildNodes(e.Node, e.Node.Checked);
                }
                finally
                {
                    e.Node.TreeView.EndUpdate();
                }
            }
        }
        private void CheckChildNodes(TreeNode node, Boolean bChecked)
        {
            foreach (TreeNode item in node.Nodes)
            {
                item.Checked = bChecked;
                if (item.Nodes.Count > 0)
                {
                    this.CheckChildNodes(item, bChecked);
                }
            }
        }
        private List<string> GetCheckedNodesText(TreeNodeCollection nodes)
        {
            List<string> strs = new List<string>();
            foreach (TreeNode node in nodes)
            {
                if (node.Checked)
                {
                    if (!String.IsNullOrEmpty(node.Text))
                    {
                        strs.Add(node.Text);
                    }
                }
            }
            return strs;
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
        public void RemoveNoteInHouse(ref Graphics g, ref SudokuCell cell, int noteNum)
        {
            foreach (var house in cell.SudokuHouses)
            {
                house.RemoveNote(noteNum);
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
                        RemoveNoteInHouse(ref g, ref cell, num);
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
            //grid is unique <-- assume it is unique
            if (!grid.IsEmpty())
            {
                //no empty cell
                log("No empty position!");
                return false;
            }
            //if (grid.IsCompleted())
            //{
            //    //no empty cell and the grid is valid
            //    log("The sudoku is solved!");
            //    return false;
            //}
            if (!grid.IsValid())
            {
                log("The sudoku is invalid now. Cannot proceed!");
                return false;
            }
            //time is longer for more empty spaces
            //if (!grid.IsUnique())
            //{
            //    log("There is no unique solution!");
            //    return false;
            //}
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
        public List<SudokuChange> Analyze(SudokuGrid _grid, List<string> algoNames = null, bool bComputeNote=false)
        {
            List<SudokuChange> changes;
            if (bComputeNote)
            {
                _grid.ComputeNoteList();
            }

            //use the selected algorithm, if algoNames is null, use all the algorithms
            List<ISudokuAlgorithm> algoList = GetAlgorithms(algoNames);

            foreach (var algo in algoList)
            {
                changes = algo.Analyze(_grid);
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
            List<SudokuChange> changes = Analyze(grid);
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

            List<SudokuChange> changes = Analyze(grid);
            if (changes != null && changes.Count > 0)
            {
                SudokuChange c = changes[0];
                //save step first before apply the change
                SaveStep();
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

                List<SudokuChange> changes = Analyze(grid);
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
        private void btnCheckAlgo_Click(object sender, EventArgs e)
        {
            if (!CanFindNextStep()) return;
            //not affect the original grid
            gridCopy = grid.Copy(); //copy all the grid data
            gridCopy.ComputeNoteList(); //compute all the notes for the grid copy
            bool bContinue = true;
            while (bContinue)
            {
                bContinue = false;

                List<SudokuChange> changes = Analyze(gridCopy);
                if (changes != null && changes.Count > 0)
                {
                    SudokuChange c = changes[0];
                    gridCopy.Apply(c);
                    bContinue = true;
                }
            }
            if (gridCopy.IsCompleted())
            {
                log("The sudoku can be solved by the algorithms!");
            }
            else
            {
                log("The sudoku cannot be solved by all these algorithms!");
            }
        }
        private void btnCheckSelectedAlgo_Click(object sender, EventArgs e)
        {
            if (!CanFindNextStep()) return;

            string s = "";
            //selected algos names
            List<string> algoNames = GetCheckedNodesText(this.algoTreeView.Nodes[0].Nodes);

            //not affect the original grid
            gridCopy = grid.Copy(); //copy all the grid data
            gridCopy.ComputeNoteList(); //compute all the notes for the grid copy
            bool bContinue = true;
            while (bContinue)
            {
                bContinue = false;

                List<SudokuChange> changes = Analyze(gridCopy, algoNames);
                if (changes != null && changes.Count > 0)
                {
                    SudokuChange c = changes[0];
                    gridCopy.Apply(c);
                    bContinue = true;
                }
            }
            if (gridCopy.IsCompleted())
            {
                log("The sudoku can be solved by the algorithms:");
            }
            else
            {
                log("The sudoku cannot be solved by all these algorithms:");
            }
            if(algoNames==null || algoNames.Count == 0)
            {
                algoNames = AlgoNames.ToList();
            }            
            foreach (var name in algoNames)
            {
                s += name + ", ";
            }
            log(s);
        }
        private void btnLoadFile_Click(object sender, EventArgs e)
        {
            try
            {
                string appPath = Path.GetDirectoryName(Application.ExecutablePath);
                string fileName = Path.Combine(appPath, "data.txt");
                List<string> grids = new List<string>();
                using (StreamReader sr = File.OpenText(fileName))
                {
                    string s = String.Empty;
                    while ((s = sr.ReadLine()) != null)
                    {
                        grids.Add(s);
                    }
                }
                
                int cnt = 0;
                string sudoStr;
                if(currDataLine==-1 || currDataLine > grids.Count)
                {
                    currDataLine = 0;
                }
                foreach (var s in grids)
                {
                    cnt++;
                    if (cnt>=currDataLine)
                    {
                        sudoStr = s.Trim().Substring(0, 81);
                        if (LoadSudoku(sudoStr))
                        {
                            currDataLine = cnt+1;
                            return;
                        }
                    }
                }
                cnt = 0;
                foreach (var s in grids)
                {
                    cnt++;
                    if (cnt < currDataLine)
                    {
                        sudoStr = s.Trim().Substring(0, 81);
                        if (LoadSudoku(sudoStr))
                        {
                            currDataLine = cnt+1;
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }
        private bool LoadSudoku(string s)
        {
            if (grid.Load(s))
            {
                grid.Lock();
                grid.Draw(ref g);
                Repaint();
                RemoveAllSteps();
                return true;
            }
            return false;
        }
        private void btnLoad_Click(object sender, EventArgs e)
        {
            bool bLoad = LoadSudoku(tbSudoku.Text);
            if (!bLoad)
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
        private bool AnalyzeByAlgorithm(ISudokuAlgorithm algo)
        {
            //SudokuGrid gridCopy = grid.Copy();
            //List<SudokuChange> changes = algo.Analyze(gridCopy);
            List<SudokuChange> changes = algo.Analyze(grid);
            if (changes.Count > 0)
            {
                grid.Unselect();
                foreach (var c in changes)
                {
                    if (c.Type == SudokuChangeType.SetNum || c.Type == SudokuChangeType.RemoveNote)
                    {
                        grid.Select(c.Row, c.Col);
                        log($"{c.Message}");
                    }
                }
                grid.Draw(ref g);
                foreach (var c in changes)
                {
                    //highlight note
                    if (c.Type == SudokuChangeType.HighlightNoteGreen)
                    {
                        grid.HighlightNoteNum(ref g, c.NoteNum, c.Row, c.Col, Sudoku.Sudoku.brushLightGreen);
                    }
                    else if (c.Type == SudokuChangeType.HighlightNoteRed)
                    {
                        grid.HighlightNoteNum(ref g, c.NoteNum, c.Row, c.Col, Sudoku.Sudoku.brushLightPink);
                    }
                }
                Repaint();
                return true;
            }
            else
            {
                log($"Cannot Apply {algo.AlgorithmName}!");
                return false;
            }
        }
        private List<ISudokuAlgorithm> GetAlgorithms(List<string> names = null)
        {
            List<ISudokuAlgorithm> algoList = new List<ISudokuAlgorithm>();
            if (names != null && names.Count>0)
            {
                ISudokuAlgorithm alg;
                foreach (var name in names)
                {
                    alg = GetAlgorithm(name);
                    if (alg != null)
                    {
                        algoList.Add(alg);
                    }
                }
            }
            else
            {
                //return all
                algoList.Add(new AlgoSingleCandidate());
                algoList.Add(new AlgoSinglePosition());
                algoList.Add(new AlgoCandidateLines());
                algoList.Add(new AlgoNakedPair());
            }
            return algoList;
        }
        private ISudokuAlgorithm GetAlgorithm(string name)
        {
            switch(name)
            {
                case AlgoNames.SingleCandidate:
                    return new AlgoSingleCandidate();
                case AlgoNames.SinglePosition:
                    return new AlgoSinglePosition();
                case AlgoNames.CandidateLines:
                    return new AlgoCandidateLines();
                case AlgoNames.NakedPair:
                    return new AlgoNakedPair();
                default:
                    return null;
            }
        }
        private void btnUseAlgo_Click(object sender, EventArgs e)
        {
            List<string> algoNames = GetCheckedNodesText(this.algoTreeView.Nodes[0].Nodes);
            foreach(var name in algoNames)
            {
                if(AnalyzeByAlgorithm(GetAlgorithm(name)))
                {
                    return;
                }
            }
        }
        private void btnSingleCandidate_Click(object sender, EventArgs e)
        {
            AnalyzeByAlgorithm(new AlgoSingleCandidate());
        }
        private void btnSinglePosition_Click(object sender, EventArgs e)
        {
            AnalyzeByAlgorithm(new AlgoSinglePosition());
        }
        private void btnCandidateLines_Click(object sender, EventArgs e)
        {
            AnalyzeByAlgorithm(new AlgoCandidateLines());
        }
        private void btnNakedPair_Click(object sender, EventArgs e)
        {
            AnalyzeByAlgorithm(new AlgoNakedPair());
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
