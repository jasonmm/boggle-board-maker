using System;
using System.Text;
using System.Collections.Generic;

namespace BoggleBoardMaker
{
    /// <summary>
    /// Contains logic creating, solving, and displaying boggle boards.
    /// </summary>
    public class BoggleBoard
    {
        /// <summary>
        /// The directions of the compass.  Used when solving the boggle board.
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

        /// <summary>
        /// Standard alphabet.
        /// </summary>
        private static string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        
        /// <summary>
        /// An alphabet based on the frequency of the letters.
        /// Used the table found [here](https://en.wikipedia.org/wiki/Letter_frequency#Relative_frequencies_of_letters_in_the_English_language)
        /// and rounded the percentages.  All 0.xxx% values were rounded up.
        /// </summary>
        private static string LetterFrequencyAlphabet = "AAAAAAAABCCCDDDDEEEEEEEEEEEEEFFGHHHHHHIIIIIIIJKLLLLMMMNNNNNNNOOOOOOOOPPQRRRRRRSSSSSSTTTTTTTTTUUUVWWXYYZ";

        /// <summary>
        /// The boggle board stored as a string.
        /// </summary>
        private string Board = "";
        
        private int BoardDimension = 0;
        private List<int> BoardIndexesUsed = new List<int>();
        public List<string> WordsInBoard = new List<string>();

        /// <summary>
        /// Creates a boggle board and returns the string.
        /// </summary>
        public string Create(int dimension)
        {
            var r = new System.Random();
            var n = dimension * dimension;
            var alphabetLen = Alphabet.Length;
            BoardDimension = dimension;

            for (var i = 0; i < n; i++)
            {
                var index = r.Next(0, alphabetLen);
                Board += Alphabet[index];
            }

            return Board;
        }

        /// <summary>
        /// Get the board.  If "formatted" is true then a string
        /// with newlines will be returned allowing the string to
        /// be output and display as a grid.
        /// </summary>
        public string GetBoard(bool formatted = false)
        {
            return formatted ? Draw() : Board;
        }

        /// <summary>
        /// Solve the boggle board using the given "wordList" as the list of
        /// all valid words.
        /// </summary>
        public void Solve(string[] wordList)
        {
            foreach (var word in wordList)
            {
                // Words less than 3 chars is not boggle.
                if (word.Length < 3)
                {
                    continue;
                }
                
                if (IsWordInBoard(word.ToUpper()))
                {
                    WordsInBoard.Add(word);
                }
            }
        }

        /// <summary>
        /// Determines if the given word is in the boggle board.
        /// </summary>
        private bool IsWordInBoard(string word)
        {
            BoardIndexesUsed.Clear();

            for (var i = 0; i < Board.Length; i++)
            {
                if (word[0] == Board[i])
                {
                    BoardIndexesUsed.Add(i);
                    if (IsLetterInWord(i, word, 1))
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
        private bool IsLetterInWord(int boardIndex, string word, int wordIndex)
        {
            for (var directionIndex = 0; directionIndex < 8; directionIndex++)
            {
                // Get the X,Y coordinates from the board string's index.
                int boardY = boardIndex / BoardDimension;
                int boardX = boardIndex % BoardDimension;

                // Move the X,Y coordinates in the current direction.
                boardX += Directions[directionIndex, 0];
                boardY += Directions[directionIndex, 1];

                // Check to see if the new coordinates are outside the board.
                if (boardX < 0 || boardX > BoardDimension - 1 || boardY < 0 || boardY > BoardDimension - 1)
                {
                    continue;
                }

                // Convert the X,Y coordinates back into an index into our board string.
                int newBoardIndex = boardY * BoardDimension + boardX;

                // Check to see if the new board index is outside the board.
                if (newBoardIndex < 0 || newBoardIndex > Board.Length - 1)
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
                if (Board[newBoardIndex] == word[wordIndex])
                {
                    BoardIndexesUsed.Add(newBoardIndex);
                    if (wordIndex + 1 < word.Length)
                    {
                        if (IsLetterInWord(newBoardIndex, word, wordIndex + 1))
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

        /// <summary>
        /// Turn the board into a two dimensional char array.
        /// </summary>
        public char[,] GetBoardArray()
        {
            var dimension = Convert.ToInt16(Math.Sqrt(Board.Length));
            var row = 0;
            var col = 0;
            var boardArray = new char[dimension, dimension];

            for (var i = 0; i < Board.Length; i++)
            {
                if (i > 0 && i % dimension == 0)
                {
                    row++;
                    col = 0;
                }
                boardArray[row, col] = Board[i];
                col++;
            }

            return boardArray;
        }

        /// <summary>
        /// Returns a string with newlines allowing the string to display as a grid.
        /// </summary>
        private string Draw()
        {
            var dimension = Convert.ToInt16(Math.Sqrt(Board.Length));
            var boardArray = GetBoardArray();
            var board = new StringBuilder();
            for (var i = 0; i < dimension; i++)
            {
                for (var j = 0; j < dimension; j++)
                {
                    string ch = boardArray[i, j].ToString();
                    if (ch == "Q")
                    {
                        ch = "Qu";
                    }
                    else
                    {
                        ch = ch + " ";
                    }
                    board.Append(ch);
                }
                board.Append("\n");
            }
            return board.ToString();
        }
    }
}