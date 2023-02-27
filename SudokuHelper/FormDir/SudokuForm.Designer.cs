namespace SudokuHelper.FormDir
{
    partial class SudokuForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SudokuForm));
            this.btnTest = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnErase = new System.Windows.Forms.Button();
            this.btnNote = new System.Windows.Forms.Button();
            this.btnLock = new System.Windows.Forms.Button();
            this.btnUnlock = new System.Windows.Forms.Button();
            this.btnHelpNote = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnResetAll = new System.Windows.Forms.Button();
            this.btnLockSelected = new System.Windows.Forms.Button();
            this.btnUnlockSelected = new System.Windows.Forms.Button();
            this.btnEraseAll = new System.Windows.Forms.Button();
            this.btnNoteVisible = new System.Windows.Forms.Button();
            this.tbLogger = new System.Windows.Forms.TextBox();
            this.btnSolver = new System.Windows.Forms.Button();
            this.btnCheckUnique = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnRollback = new System.Windows.Forms.Button();
            this.btnHints = new System.Windows.Forms.Button();
            this.btnFindNext = new System.Windows.Forms.Button();
            this.btnAlgo = new System.Windows.Forms.Button();
            this.btnRedo = new System.Windows.Forms.Button();
            this.tbSudoku = new System.Windows.Forms.TextBox();
            this.btnLoad = new System.Windows.Forms.Button();
            this.btnSingleCandidate = new System.Windows.Forms.Button();
            this.btnSinglePosition = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCandidateLines = new System.Windows.Forms.Button();
            this.btnNakedPair = new System.Windows.Forms.Button();
            this.btnOutputSudo = new System.Windows.Forms.Button();
            this.btnHighlightNote = new System.Windows.Forms.Button();
            this.btnCheckAlgo = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(591, 262);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(111, 33);
            this.btnTest.TabIndex = 1;
            this.btnTest.TabStop = false;
            this.btnTest.Text = "Sample Test";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(845, 126);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(111, 36);
            this.btnReset.TabIndex = 2;
            this.btnReset.TabStop = false;
            this.btnReset.Text = "Reset Selected";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnErase
            // 
            this.btnErase.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnErase.BackgroundImage")));
            this.btnErase.Location = new System.Drawing.Point(591, 444);
            this.btnErase.Name = "btnErase";
            this.btnErase.Size = new System.Drawing.Size(50, 50);
            this.btnErase.TabIndex = 3;
            this.btnErase.TabStop = false;
            this.btnErase.UseVisualStyleBackColor = true;
            this.btnErase.Click += new System.EventHandler(this.btnErase_Click);
            // 
            // btnNote
            // 
            this.btnNote.Location = new System.Drawing.Point(591, 504);
            this.btnNote.Name = "btnNote";
            this.btnNote.Size = new System.Drawing.Size(111, 36);
            this.btnNote.TabIndex = 4;
            this.btnNote.TabStop = false;
            this.btnNote.Text = "Note Mode (OFF)";
            this.btnNote.UseVisualStyleBackColor = true;
            this.btnNote.Click += new System.EventHandler(this.btnNote_Click);
            // 
            // btnLock
            // 
            this.btnLock.Location = new System.Drawing.Point(591, 60);
            this.btnLock.Name = "btnLock";
            this.btnLock.Size = new System.Drawing.Size(111, 36);
            this.btnLock.TabIndex = 5;
            this.btnLock.TabStop = false;
            this.btnLock.Text = "Lock All";
            this.btnLock.UseVisualStyleBackColor = true;
            this.btnLock.Click += new System.EventHandler(this.btnLock_Click);
            // 
            // btnUnlock
            // 
            this.btnUnlock.Location = new System.Drawing.Point(720, 60);
            this.btnUnlock.Name = "btnUnlock";
            this.btnUnlock.Size = new System.Drawing.Size(111, 36);
            this.btnUnlock.TabIndex = 6;
            this.btnUnlock.TabStop = false;
            this.btnUnlock.Text = "UnLock All";
            this.btnUnlock.UseVisualStyleBackColor = true;
            this.btnUnlock.Click += new System.EventHandler(this.btnUnlock_Click);
            // 
            // btnHelpNote
            // 
            this.btnHelpNote.Location = new System.Drawing.Point(591, 199);
            this.btnHelpNote.Name = "btnHelpNote";
            this.btnHelpNote.Size = new System.Drawing.Size(111, 36);
            this.btnHelpNote.TabIndex = 7;
            this.btnHelpNote.TabStop = false;
            this.btnHelpNote.Text = "Help Take notes";
            this.btnHelpNote.UseVisualStyleBackColor = true;
            this.btnHelpNote.Click += new System.EventHandler(this.btnHelpNote_Click);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(540, 540);
            this.panel1.TabIndex = 8;
            this.panel1.Click += new System.EventHandler(this.panel1_Click);
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // btnResetAll
            // 
            this.btnResetAll.Location = new System.Drawing.Point(970, 60);
            this.btnResetAll.Name = "btnResetAll";
            this.btnResetAll.Size = new System.Drawing.Size(111, 36);
            this.btnResetAll.TabIndex = 9;
            this.btnResetAll.TabStop = false;
            this.btnResetAll.Text = "Reset All";
            this.btnResetAll.UseVisualStyleBackColor = true;
            this.btnResetAll.Click += new System.EventHandler(this.btnResetAll_Click);
            // 
            // btnLockSelected
            // 
            this.btnLockSelected.Location = new System.Drawing.Point(591, 126);
            this.btnLockSelected.Name = "btnLockSelected";
            this.btnLockSelected.Size = new System.Drawing.Size(111, 36);
            this.btnLockSelected.TabIndex = 10;
            this.btnLockSelected.TabStop = false;
            this.btnLockSelected.Text = "Lock Selected";
            this.btnLockSelected.UseVisualStyleBackColor = true;
            this.btnLockSelected.Click += new System.EventHandler(this.btnLockSelected_Click);
            // 
            // btnUnlockSelected
            // 
            this.btnUnlockSelected.Location = new System.Drawing.Point(720, 126);
            this.btnUnlockSelected.Name = "btnUnlockSelected";
            this.btnUnlockSelected.Size = new System.Drawing.Size(111, 36);
            this.btnUnlockSelected.TabIndex = 11;
            this.btnUnlockSelected.TabStop = false;
            this.btnUnlockSelected.Text = "Unlock Selected";
            this.btnUnlockSelected.UseVisualStyleBackColor = true;
            this.btnUnlockSelected.Click += new System.EventHandler(this.btnUnlockSelected_Click);
            // 
            // btnEraseAll
            // 
            this.btnEraseAll.Location = new System.Drawing.Point(845, 60);
            this.btnEraseAll.Name = "btnEraseAll";
            this.btnEraseAll.Size = new System.Drawing.Size(111, 36);
            this.btnEraseAll.TabIndex = 12;
            this.btnEraseAll.TabStop = false;
            this.btnEraseAll.Text = "Erase All";
            this.btnEraseAll.UseVisualStyleBackColor = true;
            this.btnEraseAll.Click += new System.EventHandler(this.btnEraseAll_Click);
            // 
            // btnNoteVisible
            // 
            this.btnNoteVisible.Location = new System.Drawing.Point(729, 504);
            this.btnNoteVisible.Name = "btnNoteVisible";
            this.btnNoteVisible.Size = new System.Drawing.Size(131, 36);
            this.btnNoteVisible.TabIndex = 13;
            this.btnNoteVisible.TabStop = false;
            this.btnNoteVisible.Text = "Note Visible (ON)";
            this.btnNoteVisible.UseVisualStyleBackColor = true;
            this.btnNoteVisible.Click += new System.EventHandler(this.btnNoteVisible_Click);
            // 
            // tbLogger
            // 
            this.tbLogger.Location = new System.Drawing.Point(591, 324);
            this.tbLogger.Margin = new System.Windows.Forms.Padding(2);
            this.tbLogger.Multiline = true;
            this.tbLogger.Name = "tbLogger";
            this.tbLogger.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbLogger.Size = new System.Drawing.Size(490, 105);
            this.tbLogger.TabIndex = 31;
            this.tbLogger.TabStop = false;
            // 
            // btnSolver
            // 
            this.btnSolver.Location = new System.Drawing.Point(845, 202);
            this.btnSolver.Name = "btnSolver";
            this.btnSolver.Size = new System.Drawing.Size(111, 33);
            this.btnSolver.TabIndex = 32;
            this.btnSolver.TabStop = false;
            this.btnSolver.Text = "Solve by computer";
            this.btnSolver.UseVisualStyleBackColor = true;
            this.btnSolver.Click += new System.EventHandler(this.btnSolver_Click);
            // 
            // btnCheckUnique
            // 
            this.btnCheckUnique.Location = new System.Drawing.Point(720, 202);
            this.btnCheckUnique.Name = "btnCheckUnique";
            this.btnCheckUnique.Size = new System.Drawing.Size(111, 33);
            this.btnCheckUnique.TabIndex = 33;
            this.btnCheckUnique.TabStop = false;
            this.btnCheckUnique.Text = "Check unique";
            this.btnCheckUnique.UseVisualStyleBackColor = true;
            this.btnCheckUnique.Click += new System.EventHandler(this.btnCheckUnique_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(720, 613);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(111, 33);
            this.btnSave.TabIndex = 34;
            this.btnSave.TabStop = false;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnRollback
            // 
            this.btnRollback.Location = new System.Drawing.Point(845, 613);
            this.btnRollback.Name = "btnRollback";
            this.btnRollback.Size = new System.Drawing.Size(111, 33);
            this.btnRollback.TabIndex = 35;
            this.btnRollback.TabStop = false;
            this.btnRollback.Text = "Rollback";
            this.btnRollback.UseVisualStyleBackColor = true;
            this.btnRollback.Click += new System.EventHandler(this.btnRollback_Click);
            // 
            // btnHints
            // 
            this.btnHints.Location = new System.Drawing.Point(720, 262);
            this.btnHints.Name = "btnHints";
            this.btnHints.Size = new System.Drawing.Size(111, 33);
            this.btnHints.TabIndex = 36;
            this.btnHints.TabStop = false;
            this.btnHints.Text = "Hints";
            this.btnHints.UseVisualStyleBackColor = true;
            this.btnHints.Click += new System.EventHandler(this.btnHints_Click);
            // 
            // btnFindNext
            // 
            this.btnFindNext.Location = new System.Drawing.Point(845, 262);
            this.btnFindNext.Name = "btnFindNext";
            this.btnFindNext.Size = new System.Drawing.Size(111, 33);
            this.btnFindNext.TabIndex = 37;
            this.btnFindNext.TabStop = false;
            this.btnFindNext.Text = "Find Next Step";
            this.btnFindNext.UseVisualStyleBackColor = true;
            this.btnFindNext.Click += new System.EventHandler(this.btnFindNext_Click);
            // 
            // btnAlgo
            // 
            this.btnAlgo.Location = new System.Drawing.Point(970, 262);
            this.btnAlgo.Name = "btnAlgo";
            this.btnAlgo.Size = new System.Drawing.Size(111, 33);
            this.btnAlgo.TabIndex = 38;
            this.btnAlgo.TabStop = false;
            this.btnAlgo.Text = "Solve by algorithm";
            this.btnAlgo.UseVisualStyleBackColor = true;
            this.btnAlgo.Click += new System.EventHandler(this.btnAlgo_Click);
            // 
            // btnRedo
            // 
            this.btnRedo.Location = new System.Drawing.Point(591, 613);
            this.btnRedo.Name = "btnRedo";
            this.btnRedo.Size = new System.Drawing.Size(111, 33);
            this.btnRedo.TabIndex = 39;
            this.btnRedo.TabStop = false;
            this.btnRedo.Text = "Redo";
            this.btnRedo.UseVisualStyleBackColor = true;
            this.btnRedo.Click += new System.EventHandler(this.btnRedo_Click);
            // 
            // tbSudoku
            // 
            this.tbSudoku.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(254)));
            this.tbSudoku.Location = new System.Drawing.Point(591, 12);
            this.tbSudoku.Name = "tbSudoku";
            this.tbSudoku.Size = new System.Drawing.Size(240, 26);
            this.tbSudoku.TabIndex = 40;
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(845, 12);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(111, 33);
            this.btnLoad.TabIndex = 41;
            this.btnLoad.TabStop = false;
            this.btnLoad.Text = "Load Sudoku";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // btnSingleCandidate
            // 
            this.btnSingleCandidate.Location = new System.Drawing.Point(1131, 63);
            this.btnSingleCandidate.Name = "btnSingleCandidate";
            this.btnSingleCandidate.Size = new System.Drawing.Size(111, 33);
            this.btnSingleCandidate.TabIndex = 42;
            this.btnSingleCandidate.TabStop = false;
            this.btnSingleCandidate.Text = "Single Candidate";
            this.btnSingleCandidate.UseVisualStyleBackColor = true;
            this.btnSingleCandidate.Click += new System.EventHandler(this.btnSingleCandidate_Click);
            // 
            // btnSinglePosition
            // 
            this.btnSinglePosition.Location = new System.Drawing.Point(1131, 116);
            this.btnSinglePosition.Name = "btnSinglePosition";
            this.btnSinglePosition.Size = new System.Drawing.Size(111, 33);
            this.btnSinglePosition.TabIndex = 43;
            this.btnSinglePosition.TabStop = false;
            this.btnSinglePosition.Text = "Single Position";
            this.btnSinglePosition.UseVisualStyleBackColor = true;
            this.btnSinglePosition.Click += new System.EventHandler(this.btnSinglePosition_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(254)));
            this.label1.Location = new System.Drawing.Point(1128, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(167, 20);
            this.label1.TabIndex = 44;
            this.label1.Text = "Sudoku Techniques";
            // 
            // btnCandidateLines
            // 
            this.btnCandidateLines.Location = new System.Drawing.Point(1131, 169);
            this.btnCandidateLines.Name = "btnCandidateLines";
            this.btnCandidateLines.Size = new System.Drawing.Size(111, 33);
            this.btnCandidateLines.TabIndex = 45;
            this.btnCandidateLines.TabStop = false;
            this.btnCandidateLines.Text = "Candidate Lines";
            this.btnCandidateLines.UseVisualStyleBackColor = true;
            this.btnCandidateLines.Click += new System.EventHandler(this.btnCandidateLines_Click);
            // 
            // btnNakedPair
            // 
            this.btnNakedPair.Location = new System.Drawing.Point(1132, 223);
            this.btnNakedPair.Name = "btnNakedPair";
            this.btnNakedPair.Size = new System.Drawing.Size(111, 33);
            this.btnNakedPair.TabIndex = 46;
            this.btnNakedPair.TabStop = false;
            this.btnNakedPair.Text = "Naked Pair";
            this.btnNakedPair.UseVisualStyleBackColor = true;
            this.btnNakedPair.Click += new System.EventHandler(this.btnNakedPair_Click);
            // 
            // btnOutputSudo
            // 
            this.btnOutputSudo.Location = new System.Drawing.Point(961, 12);
            this.btnOutputSudo.Name = "btnOutputSudo";
            this.btnOutputSudo.Size = new System.Drawing.Size(120, 33);
            this.btnOutputSudo.TabIndex = 47;
            this.btnOutputSudo.TabStop = false;
            this.btnOutputSudo.Text = "Output Sudoku String";
            this.btnOutputSudo.UseVisualStyleBackColor = true;
            this.btnOutputSudo.Click += new System.EventHandler(this.btnOutputSudo_Click);
            // 
            // btnHighlightNote
            // 
            this.btnHighlightNote.Location = new System.Drawing.Point(729, 453);
            this.btnHighlightNote.Name = "btnHighlightNote";
            this.btnHighlightNote.Size = new System.Drawing.Size(111, 33);
            this.btnHighlightNote.TabIndex = 48;
            this.btnHighlightNote.TabStop = false;
            this.btnHighlightNote.Text = "Test Highlight Note";
            this.btnHighlightNote.UseVisualStyleBackColor = true;
            this.btnHighlightNote.Click += new System.EventHandler(this.btnHighlightNote_Click);
            // 
            // btnCheckAlgo
            // 
            this.btnCheckAlgo.Location = new System.Drawing.Point(1131, 290);
            this.btnCheckAlgo.Name = "btnCheckAlgo";
            this.btnCheckAlgo.Size = new System.Drawing.Size(144, 33);
            this.btnCheckAlgo.TabIndex = 49;
            this.btnCheckAlgo.TabStop = false;
            this.btnCheckAlgo.Text = "Check Solve by algorithm";
            this.btnCheckAlgo.UseVisualStyleBackColor = true;
            this.btnCheckAlgo.Click += new System.EventHandler(this.btnCheckAlgo_Click);
            // 
            // SudokuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1331, 749);
            this.Controls.Add(this.btnCheckAlgo);
            this.Controls.Add(this.btnHighlightNote);
            this.Controls.Add(this.btnOutputSudo);
            this.Controls.Add(this.btnNakedPair);
            this.Controls.Add(this.btnCandidateLines);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSinglePosition);
            this.Controls.Add(this.btnSingleCandidate);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.tbSudoku);
            this.Controls.Add(this.btnRedo);
            this.Controls.Add(this.btnAlgo);
            this.Controls.Add(this.btnFindNext);
            this.Controls.Add(this.btnHints);
            this.Controls.Add(this.btnRollback);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCheckUnique);
            this.Controls.Add(this.btnSolver);
            this.Controls.Add(this.tbLogger);
            this.Controls.Add(this.btnNoteVisible);
            this.Controls.Add(this.btnEraseAll);
            this.Controls.Add(this.btnUnlockSelected);
            this.Controls.Add(this.btnLockSelected);
            this.Controls.Add(this.btnResetAll);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnHelpNote);
            this.Controls.Add(this.btnUnlock);
            this.Controls.Add(this.btnLock);
            this.Controls.Add(this.btnNote);
            this.Controls.Add(this.btnErase);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnTest);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "SudokuForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sudoku Helper";
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.OnUserControl_KeyUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnErase;
        private System.Windows.Forms.Button btnNote;
        private System.Windows.Forms.Button btnLock;
        private System.Windows.Forms.Button btnUnlock;
        private System.Windows.Forms.Button btnHelpNote;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnResetAll;
        private System.Windows.Forms.Button btnLockSelected;
        private System.Windows.Forms.Button btnUnlockSelected;
        private System.Windows.Forms.Button btnEraseAll;
        private System.Windows.Forms.Button btnNoteVisible;
        private System.Windows.Forms.TextBox tbLogger;
        private System.Windows.Forms.Button btnSolver;
        private System.Windows.Forms.Button btnCheckUnique;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnRollback;
        private System.Windows.Forms.Button btnHints;
        private System.Windows.Forms.Button btnFindNext;
        private System.Windows.Forms.Button btnAlgo;
        private System.Windows.Forms.Button btnRedo;
        private System.Windows.Forms.TextBox tbSudoku;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Button btnSingleCandidate;
        private System.Windows.Forms.Button btnSinglePosition;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCandidateLines;
        private System.Windows.Forms.Button btnNakedPair;
        private System.Windows.Forms.Button btnOutputSudo;
        private System.Windows.Forms.Button btnHighlightNote;
        private System.Windows.Forms.Button btnCheckAlgo;
    }
}