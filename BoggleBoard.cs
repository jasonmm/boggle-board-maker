using System;
using System.Text;

namespace BoggleBoardMaker
{
    public class BoggleBoard
    {
        private static string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private string Board = "";

        public string Create(int dimension)
        {
            var r = new System.Random();
            var n = dimension * dimension;
            var alphabetLen = Alphabet.Length;

            for (var i = 0; i < n; i++)
            {
                var index = r.Next(0, alphabetLen);
                Board += Alphabet[index];
            }

            return Board;
        }

        public string GetBoard(bool formatted = false)
        {
            return formatted ? Draw() : Board;
        }

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

        private string Draw()
        {
            var dimension = Convert.ToInt16(Math.Sqrt(Board.Length));
            var boardArray = GetBoardArray();
            var board = new StringBuilder();
            for (var i = 0; i < dimension; i++)
            {
                for (var j = 0; j < dimension; j++)
                {
                    string ch = boardArray[j, i].ToString();
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