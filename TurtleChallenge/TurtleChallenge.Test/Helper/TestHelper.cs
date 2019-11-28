using Ninject;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TurtleChallenge.CrossCutting;
using TurtleChallenge.Domain.Interfaces;
using TurtleChallenge.Domain.Model;

namespace TurtleChallenge.Test.Helper
{
    internal class TestHelper
    {
        #region Config Files
        internal static string _correctConfigPath = "ConfigurationFiles/CorrectConfig.json";
        internal static string _nonWinnableConfigPath_1 = "ConfigurationFiles/NonWinnable_1.json";
        internal static string _nonWinnableConfigPath_2 = "ConfigurationFiles/NonWinnable_2.json";
        internal static string _mineOOB = "ConfigurationFiles/MinesOutOfBounds.json";
        internal static string _turtleOOB = "ConfigurationFiles/TurtleOutOfBounds.json";
        internal static string _exitOOB = "ConfigurationFiles/ExitOutOfBounds.json";
        internal static string _conflictingMines = "ConfigurationFiles/ConflictingMines.json";
        internal static string _turtleOnExit = "ConfigurationFiles/TurtleStartingOnExit.json";
        internal static string _turtleOnMine = "ConfigurationFiles/TurtleStartingOnMine.json";
        internal static string _missingExit = "ConfigurationFiles/MissingExit.json";
        internal static string _missingTurtle = "ConfigurationFiles/MissingTurtleConfig.json";
        internal static string _missingBoard = "ConfigurationFiles/MissingBoard.json";
        internal static string _missingTurtleDirection = "ConfigurationFiles/MissingTurtleDirection.json";
        #endregion

        #region Step Files
        internal static string _finishMultipleSequence = "StepFiles/FinishMultipleSequence.json";
        internal static string _finishSingleSteps = "StepFiles/FinishSingleSteps.json";
        internal static string _incorrectMultipleSequences = "StepFiles/IncorrectMultipleSequences.json";
        internal static string _incorrectSequence = "StepFiles/IncorrectSequence.json";
        #endregion

        internal static string _missingFile = "RandomFile.json";

        /// <summary>
        /// Returns an empty (No GameObject's) loaded board
        /// </summary>
        /// <param name="sizeX">Size of the X axis</param>
        /// <param name="sizeY">Size of the Y axis</param>
        /// <returns>Loaded Board instance</returns>
        internal static Board GetEmptyBoard(int sizeX, int sizeY)
        {
            List<Tile> tiles = GetEmptyTiles(sizeX, sizeY).ToList();

            Board ret = new Board(sizeX, sizeY, tiles, GetGameService());

            return ret;
        }

        /// <summary>
        /// Manual injection to get a FileData instance
        /// </summary>
        /// <returns>FileData instance</returns>
        internal static IFileData GetFileData()
        {
            var kernel = new StandardKernel();
            new LoadInjectionModule(kernel).Load();
            kernel.Load(Assembly.GetExecutingAssembly());
            return kernel.Get<IFileData>();
        }

        /// <summary>
        /// Manual injection to get a GameService instance
        /// </summary>
        /// <returns>GameService instance</returns>
        internal static IGameService GetGameService()
        {
            var kernel = new StandardKernel();
            new LoadInjectionModule(kernel).Load();
            kernel.Load(Assembly.GetExecutingAssembly());
            return kernel.Get<IGameService>();
        }

        /// <summary>
        /// Get empty tiles to be loaded on a Board instance
        /// </summary>
        /// <param name="sizeX">Size of the X axis</param>
        /// <param name="sizeY">Size of the Y axis</param>
        /// <returns>Enumerable of "empty" Tile's</returns>
        private static IEnumerable<Tile> GetEmptyTiles(int sizeX, int sizeY)
        {
            for (int i = 0; i < sizeX; i++)
            {
                for (int j = 0; j < sizeY; j++)
                {
                    yield return new Tile(i, j, null);
                }
            }
        }
    }
}