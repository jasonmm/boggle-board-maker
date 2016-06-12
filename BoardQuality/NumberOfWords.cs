namespace BoggleBoardMaker.BoardQuality
{
    /// <summary>
    /// Verifies that the board contains at least the given minimum number of words.
    /// </summary>
    public class NumberOfWords : IBoardQualityInterface
    {
        public int MinimumNumberOfWords = 0;

        public NumberOfWords(int min = 0)
        {
            MinimumNumberOfWords = min;
        }

        public bool CheckQuality(BoggleBoard board)
        {
            return board.WordsInBoard.Count > MinimumNumberOfWords;
        }
    }
}