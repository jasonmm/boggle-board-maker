using System;
using System.IO;
using System.Text;

namespace BoggleBoardMaker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length < 3)
            {
                throw new Exception("too few arguments");
            }

            var boardDimension = Convert.ToInt16(args[0]);
            var numberOfBoardsToCreated = Convert.ToInt16(args[1]);
            var wordListFileName = args[2];

            var p = new Program();
            p.BuildBoards(boardDimension, numberOfBoardsToCreated, wordListFileName);
        }

        public void BuildBoards(int dimension, int numberOfBoards, string wordListFileName)
        {
            var wordList = File.ReadAllLines(wordListFileName, Encoding.ASCII);

            var db = new BoggleBoardContext();

            for (var i = 0; i < numberOfBoards; i++)
            {
                var boggle = new BoggleBoard();
                boggle.Create(dimension);

                Console.WriteLine(boggle.GetBoard(formatted: true) + "\n");
                Console.WriteLine(boggle.GetBoard() + "\n");

                db.Boards.Add(new Board { BoardId = Guid.NewGuid().ToString(), BoardStr = boggle.GetBoard() });

                boggle.Solve(wordList);
                var wordsInBoard = boggle.WordsInBoard;

                Console.WriteLine(wordsInBoard.Count);
                Console.WriteLine(string.Join(",", wordsInBoard.ToArray()));

                db.SaveChanges();
            }
        }
    }
}
