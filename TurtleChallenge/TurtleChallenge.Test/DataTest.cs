using System.IO;
using TurtleChallenge.Infra.Data;
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

            dynamic boardJson = await fileData.LoadConfigurationFile(TestHelper._correctConfigPath);

            // Ideally this should be checking the dynamic values, but because of they are internal the string is checked instead
            Assert.Equal("{\r\n  \'BoardSizeX\': 5,\r\n  \'BoardSizeY\': 5,\r\n  \'TurtlePosX\': 0,\r\n  \'TurtlePosY\': 2,\r\n  \'TurtleDirection\': 1,\r\n  \'ExitPosX\': 4,\r\n  \'ExitPosY\': 2,\r\n  \'Mines\': [\r\n    {\r\n      \'MinePosX\': 0,\r\n      \'MinePosY\': 0\r\n    },\r\n    {\r\n      \'MinePosX\': 3,\r\n      \'MinePosY\': 2\r\n    }\r\n  ]\r\n}", boardJson.ToString().Replace('"', '\''));
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

            dynamic seqJson = await fileData.LoadSequencesFile(TestHelper._finishSingleSteps);

            // Ideally this should be checking the dynamic values, but because of they are internal the string is checked instead
            Assert.Equal("{\r\n  \'Sequences\': [\r\n    {\r\n      \'Steps\': [\r\n        {\r\n          \'Action\': \'M\'\r\n        },\r\n        {\r\n          \'Action\': \'R\'\r\n        },\r\n        {\r\n          \'Action\': \'M\'\r\n        },\r\n        {\r\n          \'Action\': \'M\'\r\n        },\r\n        {\r\n          \'Action\': \'M\'\r\n        },\r\n        {\r\n          \'Action\': \'M\'\r\n        },\r\n        {\r\n          \'Action\': \'R\'\r\n        },\r\n        {\r\n          \'Action\': \'M\'\r\n        }\r\n      ]\r\n    }\r\n  ]\r\n}", seqJson.ToString().Replace('"', '\''));
        }

        [Fact]
        public async void LoadStepsFile_IncorrectStepsFile_Throws()
        {
            FileData fileData = new FileData();

            await Assert.ThrowsAsync<FileLoadException>(() => fileData.LoadSequencesFile(TestHelper._incorrectSequence));
        }

        [Fact]
        public async void LoadStepsFile_IncorrectMultipleStepsFile_Throws()
        {
            FileData fileData = new FileData();

            await Assert.ThrowsAsync<FileLoadException>(() => fileData.LoadSequencesFile(TestHelper._incorrectMultipleSequences));
        }

        [Fact]
        public async void LoadStepsFile_MissingStepsFile_Throws()
        {
            FileData fileData = new FileData();

            await Assert.ThrowsAsync<FileNotFoundException>(() => fileData.LoadSequencesFile(TestHelper._missingFile));
        }

        [Fact]
        public async void LoadStepsFile_CorrectMultipleStepsFile()
        {
            FileData fileData = new FileData();

            dynamic seqJson = await fileData.LoadSequencesFile(TestHelper._finishMultipleSequence);

            // Ideally this should be checking the dynamic values, but because of they are internal the string is checked instead
            Assert.Equal("{\r\n  \'Sequences\': [\r\n    {\r\n      \'Steps\': [\r\n        {\r\n          \'Action\': \'M\'\r\n        },\r\n        {\r\n          \'Action\': \'R\'\r\n        },\r\n        {\r\n          \'Action\': \'M\'\r\n        },\r\n        {\r\n          \'Action\': \'M\'\r\n        },\r\n        {\r\n          \'Action\': \'M\'\r\n        },\r\n        {\r\n          \'Action\': \'M\'\r\n        },\r\n        {\r\n          \'Action\': \'R\'\r\n        },\r\n        {\r\n          \'Action\': \'M\'\r\n        }\r\n      ]\r\n    },\r\n    {\r\n      \'Steps\': [\r\n        {\r\n          \'Action\': \'M\'\r\n        },\r\n        {\r\n          \'Action\': \'R\'\r\n        },\r\n        {\r\n          \'Action\': \'M\'\r\n        },\r\n        {\r\n          \'Action\': \'M\'\r\n        },\r\n        {\r\n          \'Action\': \'M\'\r\n        },\r\n        {\r\n          \'Action\': \'M\'\r\n        },\r\n        {\r\n          \'Action\': \'R\'\r\n        },\r\n        {\r\n          \'Action\': \'M\'\r\n        }\r\n      ]\r\n    }\r\n  ]\r\n}", seqJson.ToString().Replace('"', '\''));
        }
        #endregion
    }
}