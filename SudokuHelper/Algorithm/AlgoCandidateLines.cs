using SudokuHelper.Sudoku;
using System.Collections.Generic;

namespace SudokuHelper.Algorithm
{
    /*
     * Candidate Lines
     * Test sudoku string
     * 001957063000806070769130805007261350312495786056378000108609507090710608674583000
     * Sample from https://www.sudokuoftheday.com/techniques/candidate-lines
     */
    internal class AlgoCandidateLines : ISudokuAlgorithm
    {
        public string AlgorithmName { get; } = "Candidate Lines";
        public List<SudokuChange> Analyze(SudokuGrid grid)
        {
            //check if exactly one note number exist in any house
            List<SudokuChange> changes = new List<SudokuChange>();
            List<SudokuCell> emptyCells;

            //change whether use??
            //grid.ComputeNoteList();

            foreach (var block in grid.GetBlocks())
            {
                emptyCells = block.FindEmptyCells();
                //change
                int[] counts = new int[10];
                int[] numberRow = new int[10];
                int[] numberCol = new int[10];
                bool[] bSameRow = new bool[10];
                bool[] bSameCol = new bool[10];
                for (int i = 0; i < 10; i++)
                {
                    counts[i] = 0;
                    numberRow[i] = -1;
                    numberCol[i] = -1;
                    bSameRow[i] = true;
                    bSameCol[i] = true;
                }
                foreach (var cell in emptyCells)
                {                    
                    foreach (var note in cell.NotesList)
                    {
                        counts[note]++;
                        if (bSameRow[note])
                        {                            
                            if (numberRow[note] == -1)
                            {
                                numberRow[note] = cell.Row;
                            }
                            else
                            {
                                if(numberRow[note]!= cell.Row)
                                {
                                    bSameRow[note] = false;
                                }
                            }
                        }
                        if (bSameCol[note])
                        {
                            if (numberCol[note] == -1)
                            {
                                numberCol[note] = cell.Col;
                            }
                            else
                            {
                                if (numberCol[note] != cell.Col)
                                {
                                    bSameCol[note] = false;
                                }
                            }
                        }
                    }
                }
                //above check all numbers in each block
                //here check one number only
                //enhance later
                for (int i = 1; i < 10; i++)
                {
                    if (bSameRow[i] && counts[i]>1 && counts[i]<=3)
                    {
                        SudokuRow row = grid.GetRow(numberRow[i]);
                        List <SudokuCell> rowEmptyCells = row.FindEmptyCells();
                        foreach (var cell in rowEmptyCells)
                        {
                            if(cell.Block!= block.BlockNum)
                            {
                                if (cell.NotesList.Contains(i))
                                {
                                    SudokuChange chg = new SudokuChange(SudokuChangeType.RemoveNote, cell.Row, cell.Col, 0, i);
                                    chg.Message = $"Candidate Line: {i} in R{cell.Row}-B{block.BlockNum} => R{cell.Row}C{cell.Col} != {i}";
                                    changes.Add(chg);
                                }
                            }
                        }

                    }
                    else if (bSameCol[i] && counts[i] > 1 && counts[i] <= 3)
                    {
                        SudokuCol col = grid.GetCol(numberCol[i]);
                        List<SudokuCell> colEmptyCells = col.FindEmptyCells();
                        foreach (var cell in colEmptyCells)
                        {
                            if (cell.Block != block.BlockNum)
                            {
                                if (cell.NotesList.Contains(i))
                                {
                                    SudokuChange chg = new SudokuChange(SudokuChangeType.RemoveNote, cell.Row, cell.Col, 0, i);
                                    chg.Message = $"Candidate Line: {i} in C{cell.Col}-B{block.BlockNum} => R{cell.Row}C{cell.Col} != {i}";
                                    changes.Add(chg);
                                }
                            }
                        }
                    }
                    if (changes.Count > 0)
                    {
                        return changes;
                    }                    
                }
            }
            return changes;
        }
    }
}
