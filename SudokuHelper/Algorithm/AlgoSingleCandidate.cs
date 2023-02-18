using SudokuHelper.Sudoku;
using System;
using System.Collections.Generic;

namespace SudokuHelper.Algorithm
{
    internal class AlgoSingleCandidate : ISudokuAlgorithm
    {
        public string AlgorithmName { get; } = "Single Candidate";
        public List<SudokuChange> Analyze(SudokuGrid grid)
        {
            List<SudokuChange> changes = new List<SudokuChange>();
            List <SudokuCell> emptyCells = grid.FindEmptyCells();
            foreach (var cell in emptyCells)
            {
                cell.ComputeNoteList();
                if (cell.NotesList.Count == 1)
                {
                    SudokuChange chg = new SudokuChange(SudokuChangeType.SetNum, cell.Row, cell.Col, cell.NotesList[0]);
                    chg.Message = $"R{cell.Row}C{cell.Col}: only one possible number {cell.NotesList[0]}";
                    changes.Add(chg);
                    return changes;
                }
            }
            return changes;
        }
    }
}
