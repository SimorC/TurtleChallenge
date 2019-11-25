using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Linq;
using TurtleChallenge.Data.Data;
using TurtleChallenge.Domain.Model;
using TurtleChallenge.Test.Helper;

namespace TurtleChallenge.Test
{
    [TestClass]
    public class DataTest
    {
        #region Config
        [TestMethod]
        public void LoadConfigurationFile_CorrectConfigFile()
        {
            FileData fileData = new FileData();

            fileData.LoadConfigurationFile(TestHelper._correctConfigPath);

            Assert.IsTrue(Game.GameBoard.SizeX > 0);
            Assert.IsTrue(Game.GameBoard.SizeY > 0);
            Assert.IsTrue(Game.GameBoard.Tiles.Count() > 0);
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException), "Configuration file not found!")]
        public void LoadConfigurationFile_MissingConfigFile_Throws()
        {
            FileData fileData = new FileData();

            fileData.LoadConfigurationFile(TestHelper._missingFile);
        }

        [TestMethod]
        [ExpectedException(typeof(FileLoadException), "Exit node is missing!")]
        public void LoadConfigurationFile_MissingExit_Throws()
        {
            FileData fileData = new FileData();

            fileData.LoadConfigurationFile(TestHelper._missingExit);
        }

        [TestMethod]
        [ExpectedException(typeof(FileLoadException), "Turtle position is missing!")]
        public void LoadConfigurationFile_MissingTurtle_Throws()
        {
            FileData fileData = new FileData();

            fileData.LoadConfigurationFile(TestHelper._missingTurtle);
        }

        [TestMethod]
        [ExpectedException(typeof(FileLoadException), "Board configuration is missing!")]
        public void LoadConfigurationFile_MissingBoard_Throws()
        {
            FileData fileData = new FileData();

            fileData.LoadConfigurationFile(TestHelper._missingBoard);
        }

        [TestMethod]
        [ExpectedException(typeof(FileLoadException), "Turtle direction is missing!")]
        public void LoadConfigurationFile_MissingTurtleDirection_Throws()
        {
            FileData fileData = new FileData();

            fileData.LoadConfigurationFile(TestHelper._missingTurtleDirection);
        }
        #endregion

        #region Steps
        [TestMethod]
        public void LoadStepsFile_CorrectStepsFile()
        {
            FileData fileData = new FileData();

            fileData.LoadStepsFile(TestHelper._finishSingleSteps);

            Assert.IsTrue(Game.Sequences.Count == 1);
        }

        [TestMethod]
        public void LoadStepsFile_CorrectMultipleStepsFile()
        {
            FileData fileData = new FileData();

            fileData.LoadStepsFile(TestHelper._finishMultipleSequence);

            Assert.IsTrue(Game.Sequences.Count == 2);
        }

        [TestMethod]
        [ExpectedException(typeof(FileLoadException), "Incorrect sequence file!")]
        public void LoadStepsFile_IncorrectStepsFile_Throws()
        {
            FileData fileData = new FileData();

            fileData.LoadStepsFile(TestHelper._incorrectSequence);
        }

        [TestMethod]
        [ExpectedException(typeof(FileLoadException), "Incorrect sequence file!")]
        public void LoadStepsFile_IncorrectMultipleStepsFile_Throws()
        {
            FileData fileData = new FileData();

            fileData.LoadStepsFile(TestHelper._incorrectMultipleSequences);
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException), "Steps file not found!")]
        public void LoadStepsFile_MissingStepsFile_Throws()
        {
            FileData fileData = new FileData();

            fileData.LoadStepsFile(TestHelper._missingFile);
        }
        #endregion
    }
}