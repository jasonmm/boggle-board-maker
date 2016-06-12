using System.Collections.Generic;

namespace BoggleBoardMaker
{
    public interface IBoardSolverInterface
    {
        List<string> Solve(BoggleBoard board);
    }
}