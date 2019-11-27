using TurtleChallenge.Domain.Exception;
using TurtleChallenge.Domain.Interfaces;
using TurtleChallenge.Domain.Model;
using TurtleChallenge.Test.Helper;
using Xunit;

namespace TurtleChallenge.Test
{
    public class BoardTest
    {
        private readonly IFileData _fileData;

        public BoardTest()
        {
            this._fileData = TestHelper.GetFileData();
        }

        [Fact]
        public void IsGameWinnable_True()
        {
            Board board = new Board(TestHelper._correctConfigPath, this._fileData);

            bool isWinnable = board.IsGameWinnable();

            Assert.True(isWinnable);
        }

        [Fact]
        public void IsGameWinnable_False1()
        {
            Board board = new Board(TestHelper._nonWinnableConfigPath_1, this._fileData);

            bool isWinnable = board.IsGameWinnable();

            Assert.False(isWinnable);
        }

        [Fact]
        public void IsGameWinnable_False2()
        {
            Board board = new Board(TestHelper._nonWinnableConfigPath_2, this._fileData);

            bool isWinnable = board.IsGameWinnable();

            Assert.False(isWinnable);
        }

        [Fact]
        public void BoardCtor_MineOutOfBounds_Throws()
        {
            Assert.Throws<GameLoadException>(() => new Board(TestHelper._mineOOB, this._fileData));
        }

        [Fact]
        public void BoardCtor_TurtleOutOfBounds_Throws()
        {
            Assert.Throws<GameLoadException>(() => new Board(TestHelper._turtleOOB, this._fileData));
        }

        [Fact]
        public void BoardCtor_ExitOutOfBounds_Throws()
        {
            Assert.Throws<GameLoadException>(() => new Board(TestHelper._exitOOB, this._fileData));
        }

        [Fact]
        public void BoardCtor_TurtleOnMine_Throws()
        {
            Assert.Throws<GameLoadException>(() => new Board(TestHelper._turtleOnMine, this._fileData));
        }

        [Fact]
        public void BoardCtor_TurtleOnExit_Throws()
        {
            Assert.Throws<GameLoadException>(() => new Board(TestHelper._turtleOnExit, this._fileData));
        }

        [Fact]
        public void BoardCtor_ConflictingMines_Throws()
        {
            Assert.Throws<GameLoadException>(() => new Board(TestHelper._conflictingMines, this._fileData));
        }
    }
}