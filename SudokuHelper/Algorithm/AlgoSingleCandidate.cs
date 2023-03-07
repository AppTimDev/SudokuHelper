using SudokuHelper.Sudoku;
using System;
using System.Collections.Generic;

namespace SudokuHelper.Algorithm
{
    internal class AlgoSingleCandidate : ISudokuAlgorithm
    {
        public string AlgorithmName { get; } = AlgoNames.SingleCandidate;
        public List<SudokuChange> Analyze(SudokuGrid grid)
        {
            //think about compute notes first??
            //grid.ComputeNoteList();

            List<SudokuChange> changes = new List<SudokuChange>();
            List <SudokuCell> emptyCells = grid.FindEmptyCellsWithNNotes(1);
            foreach (var cell in emptyCells)
            {
                SudokuChange chg = new SudokuChange(SudokuChangeType.SetNum, cell.Row, cell.Col, cell.NotesList[0]);
                chg.Message = $"Naked Single: R{cell.Row}C{cell.Col} = {cell.NotesList[0]}";
                changes.Add(chg);
                return changes;
            }
            return changes;
        }
    }
}
