using SudokuHelper.Sudoku;
using System.Collections.Generic;

namespace SudokuHelper.Algorithm
{
    internal interface ISudokuAlgorithm
    {
        string AlgorithmName { get; }
        List<SudokuChange> Analyze(SudokuGrid grid);
    }
}
