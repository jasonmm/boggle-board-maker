
namespace BoggleBoardMaker.BoardCreators
{
    /// <summary>
    /// Creates a boggle board by choosing random letter from an alphabet 
    /// weighted by frequency of use in English words.
    /// </summary>
    public class FrequencyAlphabet : IBoardCreatorInterface
    {
        /// <summary>
        /// An alphabet based on the frequency of the letters.
        /// Used the table found [here](https://en.wikipedia.org/wiki/Letter_frequency#Relative_frequencies_of_letters_in_the_English_language)
        /// and rounded the percentages.  All 0.xxx% values were rounded up.
        /// </summary>
        private static string Alphabet = "AAAAAAAABCCCDDDDEEEEEEEEEEEEEFFGHHHHHHIIIIIIIJKLLLLMMMNNNNNNNOOOOOOOOPPQRRRRRRSSSSSSTTTTTTTTTUUUVWWXYYZ";

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