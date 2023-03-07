using System.Collections.Generic;

namespace SudokuHelper.Algorithm
{
    public static class AlgoNames
    {
        private static List<string> AlgoNamesList = null;

        public static string SingleCandidate = "Single Candidate";
        public static string SinglePosition = "SinglePosition";
        public static string CandidateLines = "Candidate Lines";
        public static string NakedPair = "Naked Pair";
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
