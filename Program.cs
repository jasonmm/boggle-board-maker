using System;

namespace BoggleBoardMaker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var boggle = new BoggleBoard();
            boggle.Create(4);

            Console.WriteLine(boggle.GetBoard(formatted: true) + "\n");

            using (var db = new BoggleBoardContext())
            {
                db.Boards.Add(new Board { BoardId = Guid.NewGuid().ToString(), BoardStr = boggle.GetBoard() });
                db.SaveChanges();
            }
        }
    }
}
