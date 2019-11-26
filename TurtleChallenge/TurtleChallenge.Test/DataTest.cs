using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TurtleChallenge.Data.Data;
using TurtleChallenge.Domain.Model;
using TurtleChallenge.Domain.Model.Extension;
using TurtleChallenge.Test.Helper;
using Xunit;

namespace TurtleChallenge.Test
{
    public class DataTest
    {
        #region Config
        [Fact]
        public async void LoadConfigurationFile_CorrectConfigFile()
        {
            FileData fileData = new FileData();

            Board board = await fileData.LoadConfigurationFile(TestHelper._correctConfigPath);

            Assert.True(board.SizeX > 0);
            Assert.True(board.SizeY > 0);
            Assert.True(board.Tiles.Count() > 0);
            Assert.True(new Coordinate(0, 2).IsSame(board.GetTurtleCoordinate()));
            Assert.True(new Coordinate(4, 2).IsSame(board.GetExitCoordinate()));
            Assert.True(new Coordinate(0, 0).IsSame(board.GetMinesCoordinates()[0]));
            Assert.True(new Coordinate(3, 2).IsSame(board.GetMinesCoordinates()[1]));
        }

        [Fact]
        public async void LoadConfigurationFile_MissingConfigFile_Throws()
        {
            FileData fileData = new FileData();

            await Assert.ThrowsAsync<FileNotFoundException>(() => fileData.LoadConfigurationFile(TestHelper._missingFile));
        }

        [Fact]
        public async void LoadConfigurationFile_MissingExit_Throws()
        {
            FileData fileData = new FileData();

            await Assert.ThrowsAsync<FileLoadException>(() => fileData.LoadConfigurationFile(TestHelper._missingExit));
        }

        [Fact]
        public async void LoadConfigurationFile_MissingTurtle_Throws()
        {
            FileData fileData = new FileData();

            await Assert.ThrowsAsync<FileLoadException>(() => fileData.LoadConfigurationFile(TestHelper._missingTurtle));
        }

        [Fact]
        public async void LoadConfigurationFile_MissingBoard_Throws()
        {
            FileData fileData = new FileData();

            await Assert.ThrowsAsync<FileLoadException>(() => fileData.LoadConfigurationFile(TestHelper._missingBoard));
        }

        [Fact]
        public async void LoadConfigurationFile_MissingTurtleDirection_Throws()
        {
            FileData fileData = new FileData();

            await Assert.ThrowsAsync<FileLoadException>(() => fileData.LoadConfigurationFile(TestHelper._missingTurtleDirection));
        }
        #endregion

        #region Steps
        [Fact]
        public async void LoadStepsFile_CorrectStepsFile()
        {
            FileData fileData = new FileData();

            await fileData.LoadStepsFile(TestHelper._finishSingleSteps);

            Assert.True(Game.Sequences.Count == 1);
        }

        [Fact]
        public async void LoadStepsFile_IncorrectStepsFile_Throws()
        {
            FileData fileData = new FileData();

            await Assert.ThrowsAsync<FileLoadException>(() => fileData.LoadStepsFile(TestHelper._incorrectSequence));
        }

        [Fact]
        public async void LoadStepsFile_IncorrectMultipleStepsFile_Throws()
        {
            FileData fileData = new FileData();

            await Assert.ThrowsAsync<FileLoadException>(() => fileData.LoadStepsFile(TestHelper._incorrectMultipleSequences));
        }

        [Fact]
        public async void LoadStepsFile_MissingStepsFile_Throws()
        {
            FileData fileData = new FileData();

            await Assert.ThrowsAsync<FileNotFoundException>(() => fileData.LoadStepsFile(TestHelper._missingFile));
        }

        [Fact]
        public async void LoadStepsFile_CorrectMultipleStepsFile()
        {
            FileData fileData = new FileData();

            await fileData.LoadStepsFile(TestHelper._finishMultipleSequence);

            Assert.Equal(2, Game.Sequences.Count);
        }
        #endregion
    }
}