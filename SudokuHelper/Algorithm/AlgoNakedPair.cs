using SudokuHelper.Sudoku;
using System.Collections.Generic;

namespace SudokuHelper.Algorithm
{
    /*
     * Test sudoku string
     * 700849030928135006400267089642783951397451628815692300204516093100008060500004010
     * 687004523953002614142356978310007246760000305020000701096001032230000057070000069
     * 
     */
    internal class AlgoNakedPair : ISudokuAlgorithm
    {
        public string AlgorithmName { get; } = "Naked Pair";
        public List<SudokuChange> Analyze(SudokuGrid grid)
        {
            //think about compute notes first??
            //pass a grid copy??
            //grid.ComputeNoteList();
            List<SudokuChange> changes = new List<SudokuChange>();
            foreach (var house in grid.Houses)
            {
                List<int> PossibleNums = new List<int>();
                List<SudokuCell> emptyCells = house.FindEmptyCells();
                List<SudokuCell> emptyCellsWith2Poss = house.FindEmptyCellsWithNNotes(2);

                foreach (var cell1 in emptyCellsWith2Poss)
                {
                    foreach (var cell2 in emptyCellsWith2Poss)
                    {
                        //not the same cell
                        if(cell1 != cell2 && cell1.HasSameNotes(cell2))
                        {
                            //here cell1 and cell2 are different and contains two same notes number
                            //and it forms a naked pair
                            int n1 = cell1.NotesList[0];
                            int n2 = cell1.NotesList[1];

                            //check whether we can eliminate n1, n2 in other cells in the same house
                            foreach (var cell in emptyCells)
                            {
                                if (cell != cell1 && cell != cell2)
                                {
                                    if (cell.NotesList.Contains(n1))
                                    {
                                        SudokuChange chg = new SudokuChange(SudokuChangeType.RemoveNote, cell.Row, cell.Col, 0, n1);
                                        chg.Message = $"Naked pair: ({n1}, {n2}) in R{cell1.Row}C{cell1.Col}, R{cell2.Row}C{cell2.Col} => R{cell.Row}C{cell.Col} != {n1}";
                                        changes.Add(chg);
                                    }
                                    if (cell.NotesList.Contains(n2))
                                    {
                                        SudokuChange chg = new SudokuChange(SudokuChangeType.RemoveNote, cell.Row, cell.Col, 0, n2);
                                        chg.Message = $"Naked pair: ({n1}, {n2}) in R{cell1.Row}C{cell1.Col}, R{cell2.Row}C{cell2.Col} => R{cell.Row}C{cell.Col} != {n2}";
                                        changes.Add(chg);
                                    }
                                    if (changes.Count > 0) return changes;
                                }
                            }
                        }
                    }
                }
            }
            return changes;
        }
    }
}
