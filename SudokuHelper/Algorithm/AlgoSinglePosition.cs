using SudokuHelper.Sudoku;
using System;
using System.Collections.Generic;

namespace SudokuHelper.Algorithm
{
    /*
     * Test sudoku string
     * 006030708030000001200000600100350006079040150500017004002000007600000080407060200
     * Apply Single candidate first or Single Position
     */
    internal class AlgoSinglePosition : ISudokuAlgorithm
    {         
        public string AlgorithmName { get; } = "Single Position"; //or Hidden Single
        public List<SudokuChange> Analyze(SudokuGrid grid)
        {
            //check if exactly one note number exist in any house
            List<SudokuChange> changes = new List<SudokuChange>();
            List<SudokuCell> emptyCells;
            foreach (var house in grid.Houses)
            {
                emptyCells = house.FindEmptyCells();
                int[] counts = new int[10];
                for (int i = 0; i < 10; i++)
                {
                    counts[i] = 0;
                }
                foreach (var cell in emptyCells)
                {
                    cell.ComputeNoteList();
                    foreach(var note in cell.NotesList)
                    {
                        counts[note]++;
                    }
                }
                for (int i = 1; i < 10; i++)
                {
                    if (counts[i] == 1)
                    {
                        foreach (var cell in emptyCells)
                        {
                            if (cell.NotesList.Contains(i))
                            {
                                SudokuChange chg = new SudokuChange(SudokuChangeType.SetNum, cell.Row, cell.Col, i);
                                chg.Message = $"R{cell.Row}C{cell.Col}: only one hidden number {i}";
                                changes.Add(chg);
                                return changes;
                            }
                        }

                    }
                }
            }
            return changes;
        }
    }
}
