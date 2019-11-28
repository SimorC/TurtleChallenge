using TurtleChallenge.Domain.Exception;
using TurtleChallenge.Domain.Interfaces;
using TurtleChallenge.Domain.Model;
using TurtleChallenge.Test.Helper;
using Xunit;

namespace TurtleChallenge.Test
{
    public class BoardTest
    {
        private readonly IGameService _gameService;

        public BoardTest()
        {
            this._gameService = TestHelper.GetGameService();
        }

        [Fact]
        public void IsGameWinnable_True()
        {
            Board board = new Board(TestHelper._correctConfigPath, this._gameService);

            bool isWinnable = board.IsGameWinnable();

            Assert.True(isWinnable);
        }

        [Fact]
        public void IsGameWinnable_False1()
        {
            Board board = new Board(TestHelper._nonWinnableConfigPath_1, this._gameService);

            bool isWinnable = board.IsGameWinnable();

            Assert.False(isWinnable);
        }

        [Fact]
        public void IsGameWinnable_False2()
        {
            Board board = new Board(TestHelper._nonWinnableConfigPath_2, this._gameService);

            bool isWinnable = board.IsGameWinnable();

            Assert.False(isWinnable);
        }

        [Fact]
        public void BoardCtor_MineOutOfBounds_Throws()
        {
            Assert.Throws<GameLoadException>(() => new Board(TestHelper._mineOOB, this._gameService));
        }

        [Fact]
        public void BoardCtor_TurtleOutOfBounds_Throws()
        {
            Assert.Throws<GameLoadException>(() => new Board(TestHelper._turtleOOB, this._gameService));
        }

        [Fact]
        public void BoardCtor_ExitOutOfBounds_Throws()
        {
            Assert.Throws<GameLoadException>(() => new Board(TestHelper._exitOOB, this._gameService));
        }

        [Fact]
        public void BoardCtor_TurtleOnMine_Throws()
        {
            Assert.Throws<GameLoadException>(() => new Board(TestHelper._turtleOnMine, this._gameService));
        }

        [Fact]
        public void BoardCtor_TurtleOnExit_Throws()
        {
            Assert.Throws<GameLoadException>(() => new Board(TestHelper._turtleOnExit, this._gameService));
        }

        [Fact]
        public void BoardCtor_ConflictingMines_Throws()
        {
            Assert.Throws<GameLoadException>(() => new Board(TestHelper._conflictingMines, this._gameService));
        }
    }
}