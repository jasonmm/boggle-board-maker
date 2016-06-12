using System;

namespace BoggleBoardMaker
{
    /// <summary>
    /// Encapsulates the options needed to create and solve a boggle board.
    /// </summary>
    public class BoardCreationOptions
    {
        public int Dimension;
        public string[] WordList;
        public Random Rand;
        public IBoardQualityInterface QualityChecker;
    }
}