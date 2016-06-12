using System;
using System.Collections.Generic;
using System.Linq;

namespace BoggleBoardMaker.BoardCreators
{
    /// <summary>
    /// Creates a board from a set of letter cubes.  This class is intended
    /// to be extended with the subclass containing the specific cube definition.
    /// </summary>
    public abstract class LetterCubes : IBoardCreatorInterface
    {
        abstract protected List<string> Cubes { get; }
        private Random random;
        private List<string> UsedCubes = new List<string>();

        public BoggleBoard Create(BoardCreationOptions options)
        {
            if (Cubes == null)
            {
                throw new NullReferenceException("No cubes are defined. Cubes must be defined by the subclass as an array strings.");
            }

            var board = new BoggleBoard
            {
                BoardDimension = options.Dimension,
                Board = ""
            };
            var n = board.BoardDimension * board.BoardDimension;

            random = options.Rand;

            for (var i = 0; i < n; i++)
            {
                board.Board += ChooseLetter(ChooseCube());
            }

            return board;
        }

        /// <summary>
        /// Choose a letter form the given cube.
        /// </summary>
        private char ChooseLetter(string cube)
        {
            var index = random.Next(cube.Length);
            return cube[index];
        }

        /// <summary>
        /// Choose a cube from the list of available cubes.
        /// </summary>
        private string ChooseCube()
        {
            var availableCubes = Cubes.Except(UsedCubes).ToList();
            var cubeIndex = random.Next(availableCubes.Count());
            UsedCubes.Add(Cubes[cubeIndex]);
            return Cubes[cubeIndex];
        }
    }
}