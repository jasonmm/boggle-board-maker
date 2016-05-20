using System.Collections.Generic;
using System.IO;
using Microsoft.EntityFrameworkCore;

namespace BoggleBoardMaker
{
    public class BoggleBoardContext : DbContext
    {
        public DbSet<Board> Boards { get; set; }
        public DbSet<BoardWord> BoardWords { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=./boggle-boards.db");
        }
    }

    public class Board
    {
        public string BoardId { get; set; }
        public string BoardStr { get; set; }

        public List<BoardWord> Words { get; set; }
    }

    public class BoardWord
    {
        public string BoardWordId { get; set; }
        public string BoardId { get; set; }
        public string Word { get; set; }
    }
}