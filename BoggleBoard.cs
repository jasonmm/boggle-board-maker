using System;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BoggleBoardMaker
{
    /// <summary>
    /// Contains logic creating, solving, and displaying boggle boards.
    /// </summary>
    public class BoggleBoard
    {

        /// <summary>
        /// Letter cubes from the original boggle game.  Each cube is a 
        /// string, with each letter a side of the cube.
        /// [source](http://www.bananagrammer.com/2013/10/the-boggle-cube-redesign-and-its-effect.html)
        /// </summary>
        private static string[] LetterCubesClassic = {
            "AACIOT",
            "ABILTY",
            "ABJMOQ",
            "ACDEMP",
            "ACELRS",
            "ADENVZ",
            "AHMORS",
            "BIFORX",
            "DENOSW",
            "DKNOTU",
            "EEFHIY",
            "EGKLUY",
            "EGINTV",
            "EHINPS",
            "ELPSTU",
            "GILRUW"
        };

        /// <summary>
        /// Letter cubes from the "new" boggle game.  Each cube is a 
        /// string, with each letter a side of the cube.
        /// [source](http://www.bananagrammer.com/2013/10/the-boggle-cube-redesign-and-its-effect.html)
        /// </summary>
        private static string[] LetterCubesNew = {
            "AAEEGN",
            "ABBJOO",
            "ACHOPS",
            "AFFKPS",
            "AOOTTW",
            "CIMOTU",
            "DEILRX",
            "DELRVY",
            "DISTTY",
            "EEGHNW",
            "EEINSU",
            "EHRTVW",
            "EIOSST",
            "ELRTTY",
            "HIMNUQ",
            "HLNNRZ"
        };

        /// <summary>
        /// The boggle board stored as a string.
        /// </summary>
        public string Board = "";

        public int BoardDimension = 0;
        public List<string> WordsInBoard = new List<string>();

        /// <summary>
        /// Asynchronously create and solve a boggle board. The result of the 
        /// task will be null if the created and solved board does not pass the
        /// the quality check.  Otherwise the result will be an object of type
        /// BoggleBoard.
        /// </summary>
        public static Task<BoggleBoard> CreateAndSolveAsync(BoardCreationOptions options)
        {
            return Task.Run(() =>
            {
                var board = options.Creator.Create(options);
                board.WordsInBoard = options.Solver.Solve(board);
                return options.QualityChecker.CheckQuality(board) ? board : null;
            });
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