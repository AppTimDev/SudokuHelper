using System.Collections.Generic;

namespace SudokuHelper.Algorithm
{
    public static class AlgoNames
    {
        private static List<string> AlgoNamesList = null;

        public const string SingleCandidate = "Single Candidate";
        public const string SinglePosition = "SinglePosition";
        public const string CandidateLines = "Candidate Lines";
        public const string NakedPair = "Naked Pair";
        public static List<string> ToList()
        {
            if (AlgoNamesList == null)
            {
                AlgoNamesList = new List<string>()
                {
                    SingleCandidate,
                    SinglePosition,
                    CandidateLines,
                    NakedPair,
                };
            }
            return AlgoNamesList;
        }
    }
}
