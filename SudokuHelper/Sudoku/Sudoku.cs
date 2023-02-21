using System;
using System.Drawing;

namespace SudokuHelper.Sudoku
{
    //static class
    //contain all const value of sudoku and
    //all the draw methods on the panel
    public static class Sudoku
    {
        public static readonly int CellSize = 60;
        public static readonly int BlockSize = CellSize * 3;
        public static readonly int GridSize = CellSize * 9;

        private static Pen blackPen = new Pen(Color.Black, 3);
        private static Pen gridLinePen = new Pen(Color.FromArgb(192, 199, 213), 1);

        public static Font fontSmall = new Font("Arial", 10);
        public static Font fontLarge = new Font("Arial", 36);

        public static SolidBrush brushBlack = new SolidBrush(Color.Black);
        public static SolidBrush brushBlue = new SolidBrush(Color.Blue);
        public static SolidBrush brushRed = new SolidBrush(Color.Red);
        public static SolidBrush brushLightGreen = new SolidBrush(Color.LightGreen);
        public static SolidBrush brushLightPink = new SolidBrush(Color.LightPink);

        //select cells
        public static SolidBrush brushYellow = new SolidBrush(Color.Yellow);
        public static SolidBrush brushGreenYellow = new SolidBrush(Color.GreenYellow);
        public static SolidBrush brushSurrondCell = new SolidBrush(Color.FromArgb(217, 229, 242));
        public static SolidBrush brushEmpty = new SolidBrush(SystemColors.Control);

        private static Rectangle rect =  new Rectangle(0,0, CellSize - 1, CellSize - 1);

        public static void DrawGridLines(ref Graphics g)
        {
            Rectangle rect = new Rectangle(0, 0, BlockSize, BlockSize);
            PointF p1 = new PointF(0F, 0F);
            PointF p2 = new PointF(0F, 0F);
            for (int i = 0; i < 9; i++)
            {
                p1.X = 0;
                p1.Y = i * CellSize;
                p2.X = GridSize;
                p2.Y = i * CellSize;                    
                g.DrawLine(gridLinePen, p1, p2);
            }
            for (int i = 0; i < 9; i++)
            {
                p1.Y = 0;
                p1.X = i * CellSize;
                p2.Y = GridSize;
                p2.X = i * CellSize;
                g.DrawLine(gridLinePen, p1, p2);
            }            
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    rect.Y = i * BlockSize;
                    rect.X = j * BlockSize;
                    g.DrawRectangle(blackPen, rect);
                }
            }
        }
        public static void DrawSudokuCell(ref Graphics g, ref SudokuCell cell)
        {
            if (cell.IsSelected)
            {
                DrawCellBackColor(g, cell.Row, cell.Col, ref brushYellow);
            }
            else
            {
                if (cell.SelectMode == 1)
                {
                    //match the active cell number
                    DrawCellBackColor(g, cell.Row, cell.Col, ref brushGreenYellow);
                }
                else if (cell.SelectMode == 2)
                {
                    DrawCellBackColor(g, cell.Row, cell.Col, ref brushSurrondCell);
                }
                else
                {
                    DrawCellBackColor(g, cell.Row, cell.Col, ref brushEmpty);
                }
            }
            if (cell.Num > 0)
            {
                if (cell.IsLocked)
                {
                    DrawNum(g, cell.Num, cell.Row, cell.Col, fontLarge, brushBlack);
                }
                else
                {
                    if (cell.IsFault)
                    {
                        DrawNum(g, cell.Num, cell.Row, cell.Col, fontLarge, brushRed);
                    }
                    else
                    {
                        //normal numbers 1-9                    
                        DrawNum(g, cell.Num, cell.Row, cell.Col, fontLarge, brushBlue);
                    }
                }
            }
            else
            {
                //note numbers 1-9
                //check is there any note numbers
                //else cell is empty 
                if (cell.IsNote && cell.IsNoteVisible)
                {
                    foreach(var noteNum in cell.NotesList)
                    {
                        DrawNoteNum(g, noteNum, cell.Row, cell.Col, fontSmall, brushBlack);
                    }
                }
            }
        }
        private static void DrawCellBackColor(Graphics g, int row, int col, ref SolidBrush brush)
        {
            rect.X = col * CellSize + 1;
            rect.Y = row * CellSize + 1;
            g.FillRectangle(brush, rect);
        }
        private static void DrawNum(Graphics g, int num, int row, int col, Font f, SolidBrush brush)
        {
            if (num > 0 && num <= 9)
            {
                g.DrawString(num.ToString(), f, brush, col * CellSize + 5, row * CellSize);
            }
        }

        public static void HighlightNoteNum(Graphics g, int noteNum, int row, int col, SolidBrush brush)
        {
            DrawNoteNum(g, noteNum, row, col, fontSmall, brushBlack, brush);
        }
        private static void DrawNoteNum(Graphics g, int num, int row, int col, Font f, SolidBrush brush, SolidBrush brushBackColor = null)
        {
            if (num > 0 && num <= 9)
            {
                float x = 0F; float y = 0F;
                switch (num)
                {
                    case 1:
                        x = 0F; y = 0F;
                        break;
                    case 2:
                        x = 20F; y = 0F;
                        break;
                    case 3:
                        x = 40F; y = 0F;
                        break;
                    case 4:
                        x = 0F; y = 20F;
                        break;
                    case 5:
                        x = 20F; y = 20F;
                        break;
                    case 6:
                        x = 40F; y = 20F;
                        break;
                    case 7:
                        x = 0F; y = 40F;
                        break;
                    case 8:
                        x = 20F; y = 40F;
                        break;
                    case 9:
                        x = 40F; y = 40F;
                        break;
                }
                if (brushBackColor != null)
                {
                    g.FillEllipse(brushBackColor, col * CellSize + x+2, row * CellSize + y + 2, 15, 15);
                }
                g.DrawString(num.ToString(), f, brush, col * CellSize + x + 3, row * CellSize + y + 2);
            }
        }
        private static void DrawNotes(Graphics g, int row, int col, Font f, SolidBrush brush)
        {
            for (int i = 1; i <= 9; i++)
            {
                DrawNoteNum(g, i, row, col, f, brush);
            }
        }
    }
}
