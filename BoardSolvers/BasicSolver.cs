using System.Collections.Generic;

namespace BoggleBoardMaker.BoardSolvers
{
    /// <summary>
    /// A simple solver that iterates over each word in the word list 
    /// checking to see if that word exists in the board by iterating
    /// over each letter in the board.
    /// </summary>
    public class BasicSolver : IBoardSolverInterface
    {
        /// <summary>
        /// The directions of the compass.  Used when attempting to trace a 
        /// word through the board's letters.
        /// </summary>
        public static readonly int[,] Directions = {
            {1, 0},
            {1, 1},
            {0, 1},
            {-1, 1},
            {-1, 0},
            {-1, -1},
            {0, -1},
            {1, -1},
        };

        private string[] WordList;
        private List<string> WordsInBoard = new List<string>();
        /// <summary>
        /// Used to keep track of the letters we used looking for a word.  
        /// Because a letter can't be used twice in the same word.
        /// </summary>
        private List<int> BoardIndexesUsed = new List<int>();

        /// <summary>
        /// Set the list of valid words.
        /// </summary>
        public void SetWordList(string[] wordList)
        {
            WordList = wordList;
        }

        public List<string> Solve(BoggleBoard board)
        {
            // Loop over each word in the word list and determine if the word 
            // is in the board.
            foreach (var word in WordList)
            {
                // Words less than 3 chars is not boggle.
                if (word.Length < 3)
                {
                    continue;
                }

                if (IsWordInBoard(board, word.ToUpper()))
                {
                    WordsInBoard.Add(word);
                }
            }

            return WordsInBoard;
        }

        /// <summary>
        /// Determines if the given word is in the boggle board.
        /// </summary>
        private bool IsWordInBoard(BoggleBoard board, string word)
        {
            BoardIndexesUsed.Clear();

            for (var i = 0; i < board.Board.Length; i++)
            {
                if (word[0] == board.Board[i])
                {
                    BoardIndexesUsed.Add(i);
                    if (IsLetterInWord(board, i, word, 1))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Determines if the letter in "word" at "wordIndex" matches any of
        /// the letters surrounding the letter at the given "boardIndex".
        /// </summary>
        private bool IsLetterInWord(BoggleBoard board, int boardIndex, string word, int wordIndex)
        {
            for (var directionIndex = 0; directionIndex < 8; directionIndex++)
            {
                // Get the X,Y coordinates from the board string's index.
                int boardY = boardIndex / board.BoardDimension;
                int boardX = boardIndex % board.BoardDimension;

                // Move the X,Y coordinates in the current direction.
                boardX += Directions[directionIndex, 0];
                boardY += Directions[directionIndex, 1];

                // Check to see if the new coordinates are outside the board.
                bool invalidX = boardX < 0 || boardX > board.BoardDimension - 1;
                bool invalidY = boardY < 0 || boardY > board.BoardDimension - 1;
                if (invalidX || invalidY)
                {
                    continue;
                }

                // Convert the X,Y coordinates back into an index into our board string.
                int newBoardIndex = boardY * board.BoardDimension + boardX;

                // Check to see if the new board index is outside the board.
                if (newBoardIndex < 0 || newBoardIndex > board.Board.Length - 1)
                {
                    continue;
                }

                // Check to see if the new board index has already been used in this word.
                if (BoardIndexesUsed.Contains(newBoardIndex))
                {
                    continue;
                }

                // If the letter at the new board index matches the letter at 
                // wordIndex then we move there and do the process over again.
                if (board.Board[newBoardIndex] == word[wordIndex])
                {
                    BoardIndexesUsed.Add(newBoardIndex);
                    if (wordIndex + 1 < word.Length)
                    {
                        if (IsLetterInWord(board, newBoardIndex, word, wordIndex + 1))
                        {
                            return true;
                        }
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}