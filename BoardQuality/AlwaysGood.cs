namespace BoggleBoardMaker.BoardQuality
{
    /// <summary>
    /// This board quality checker always returns true.
    /// </summary>
    public class AlwaysGood : IBoardQualityInterface
    {
        public bool CheckQuality(BoggleBoard board)
        {
            return true;
        }
    }
}