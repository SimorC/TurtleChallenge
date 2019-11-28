using System.Collections.Generic;
using System.IO;
using TurtleChallenge.Domain.Interfaces;
using TurtleChallenge.Domain.Model;
using TurtleChallenge.Domain.Model.Enum;
using TurtleChallenge.Test.Helper;
using Xunit;

namespace TurtleChallenge.Test
{
    public class GameServiceTest
    {
        private readonly IGameService _gameService;

        public GameServiceTest()
        {
            this._gameService = TestHelper.GetGameService();
        }

        [Fact]
        public void GetBoardFromConfigurationFile_CorrectFile_LoadsBoard()
        {
            Board board = _gameService.GetBoardFromConfigurationFile(TestHelper._correctConfigPath);

            Assert.Equal(5, board.SizeX);
            Assert.Equal(5, board.SizeY);
            Assert.Equal(0, board.GetTurtleCoordinate().PosX);
            Assert.Equal(2, board.GetTurtleCoordinate().PosY);
            Assert.Equal(4, board.GetExitCoordinate().PosX);
            Assert.Equal(2, board.GetExitCoordinate().PosY);
            Assert.Equal(0, board.GetMinesCoordinates()[0].PosX);
            Assert.Equal(0, board.GetMinesCoordinates()[0].PosY);
            Assert.Equal(3, board.GetMinesCoordinates()[1].PosX);
            Assert.Equal(2, board.GetMinesCoordinates()[1].PosY);
        }

        [Fact]
        public void GetBoardFromConfigurationFile_IncorrectFile_Throws()
        {
            Assert.Throws<FileLoadException>(() => this._gameService.GetBoardFromConfigurationFile(TestHelper._incorrectSequence));
        }

        [Fact]
        public void GetSequencesFromFile_CorrectFile_LoadsBoard()
        {
            List<ActionSequence> lstSeq = _gameService.GetSequencesFromFile(TestHelper._finishSingleSteps);

            Assert.Single(lstSeq);
            Assert.Equal(8, lstSeq[0].Actions.Count);
            Assert.Equal(TurtleAction.Move, lstSeq[0].Actions[0]);
            Assert.Equal(TurtleAction.Rotate, lstSeq[0].Actions[1]);
            Assert.Equal(TurtleAction.Move, lstSeq[0].Actions[2]);
            Assert.Equal(TurtleAction.Move, lstSeq[0].Actions[3]);
            Assert.Equal(TurtleAction.Move, lstSeq[0].Actions[4]);
            Assert.Equal(TurtleAction.Move, lstSeq[0].Actions[5]);
            Assert.Equal(TurtleAction.Rotate, lstSeq[0].Actions[6]);
            Assert.Equal(TurtleAction.Move, lstSeq[0].Actions[7]);
        }

        [Fact]
        public void GetSequencesFromFile_IncorrectFile__Throws()
        {
            Assert.Throws<FileLoadException>(() => this._gameService.GetSequencesFromFile(TestHelper._incorrectSequence));
        }
    }
}