using System.Collections.Generic;
using System.Linq;

namespace BoggleBoardMaker.BoardCreators
{
    /// <summary>
    /// Uses the LetterCubes base class to create a board from a set of 
    /// letter cubes.  The cubes used are from the "new" Boggle game.
    /// </summary>
    public class NewCubes : LetterCubes, IBoardCreatorInterface
    {
        /// <summary>
        /// Letter cubes from the "new" boggle game.  Each cube is a 
        /// string, with each letter representing a side of the cube.
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

        protected override List<string> Cubes
        {
            get
            {
                return LetterCubesNew.ToList();
            }
        }
    }
}