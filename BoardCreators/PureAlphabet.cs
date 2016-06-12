namespace BoggleBoardMaker.BoardCreators
{
    /// <summary>
    /// Creates a boggle board by choosing random letter from just the alphabet.
    /// </summary>
    public class PureAlphabet : IBoardCreatorInterface
    {
        /// <summary>
        /// The alphabet.
        /// </summary>
        private static string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public BoggleBoard Create(BoardCreationOptions options)
        {
            var board = new BoggleBoard
            {
                BoardDimension = options.Dimension,
                Board = ""
            };
            var n = board.BoardDimension * board.BoardDimension;
            var alphabetLen = Alphabet.Length;

            for (var i = 0; i < n; i++)
            {
                var index = options.Rand.Next(0, alphabetLen);
                board.Board += Alphabet[index];
            }

            return board;
        }
    }
}