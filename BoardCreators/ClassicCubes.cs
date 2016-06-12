using System.Collections.Generic;
using System.Linq;

namespace BoggleBoardMaker.BoardCreators
{
    /// <summary>
    /// Uses the LetterCubes base class to create a board from a set of 
    /// letter cubes.  The cubes used are from the original Boggle game.
    /// </summary>
    public class ClassicCubes : LetterCubes, IBoardCreatorInterface
    {
        /// <summary>
        /// Letter cubes from the original boggle game.  Each cube is a 
        /// string, with each letter representing a side of the cube.
        /// [source](http://www.bananagrammer.com/2013/10/the-boggle-cube-redesign-and-its-effect.html)
        /// </summary>
        protected string[] LetterCubesClassic = {
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

        protected override List<string> Cubes
        {
            get
            {
                return LetterCubesClassic.ToList();
            }
        }
    }
}