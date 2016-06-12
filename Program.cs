using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

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
            var numberOfBoardsToBeCreated = Convert.ToInt32(args[1]);
            var wordListFileName = args[2];
            List<Task<BoggleBoard>> tasks = new List<Task<BoggleBoard>>();
            var random = new Random();

            // Read the acceptable words from the file.
            var wordList = File.ReadAllLines(wordListFileName, Encoding.ASCII);

            // Create the list of tasks that will create and solve the boards.
            for (var i = 0; i < numberOfBoardsToBeCreated; i++)
            {
                var qualityChecker = new BoardQuality.NumberOfWords(200);

                var solver = new BoardSolvers.BasicSolver();
                solver.SetWordList(wordList);

                var t = BoggleBoard.CreateAndSolveAsync(new BoardCreationOptions
                {
                    Dimension = boardDimension,
                    WordList = wordList,
                    Rand = random,

                    QualityChecker = qualityChecker,
                    Creator = new BoardCreators.ClassicCubes(),
                    Solver = solver
                });
                tasks.Add(t);
            }

            // Wait for the tasks to finish.
            Task.WaitAll(tasks.ToArray());

            // Add each created board to the database.
            using (var db = new BoggleBoardContext())
            {
                foreach (var task in tasks)
                {
                    if (task.Result != null)
                    {
                        SaveBoardToDatabase(task.Result, db);
                    }
                }
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Save the given board to the given database context.
        /// </summary>
        public static void SaveBoardToDatabase(BoggleBoard board, BoggleBoardContext db)
        {
            var dbBoard = new Board
            {
                BoardId = Guid.NewGuid().ToString(),
                BoardStr = board.GetBoard()
            };
            db.Boards.Add(dbBoard);

            foreach (var word in board.WordsInBoard)
            {
                var dbWord = new BoardWord
                {
                    BoardWordId = Guid.NewGuid().ToString(),
                    BoardId = dbBoard.BoardId,
                    Word = word
                };
                db.BoardWords.Add(dbWord);
            }

        }
    }
}
