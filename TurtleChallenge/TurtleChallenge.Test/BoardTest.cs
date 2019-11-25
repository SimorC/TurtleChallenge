using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtleChallenge.CrossCutting.Exception;
using TurtleChallenge.Domain.Model;
using TurtleChallenge.Test.Helper;
using Xunit;

namespace TurtleChallenge.Test
{
    public class BoardTest
    {
        [Fact]
        public void IsGameWinnable_True()
        {
            Board board = new Board(TestHelper._correctConfigPath);

            bool isWinnable = board.IsGameWinnable();

            Assert.True(isWinnable);
        }

        [Fact]
        public void IsGameWinnable_False1()
        {
            Board board = new Board(TestHelper._nonWinnableConfigPath_1);

            bool isWinnable = board.IsGameWinnable();

            Assert.False(isWinnable);
        }

        [Fact]
        public void IsGameWinnable_False2()
        {
            Board board = new Board(TestHelper._nonWinnableConfigPath_2);

            bool isWinnable = board.IsGameWinnable();

            Assert.False(isWinnable);
        }

        [Fact]
        public void BoardCtor_MineOutOfBounds_Throws()
        {
            Assert.Throws<GameLoadException>(() => new Board(TestHelper._mineOOB));
        }

        [Fact]
        public void BoardCtor_TurtleOutOfBounds_Throws()
        {
            Assert.Throws<GameLoadException>(() => new Board(TestHelper._turtleOOB));
        }

        [Fact]
        public void BoardCtor_ExitOutOfBounds_Throws()
        {
            Assert.Throws<GameLoadException>(() => new Board(TestHelper._exitOOB));
        }

        [Fact]
        public void BoardCtor_TurtleOnMine_Throws()
        {
            Assert.Throws<GameLoadException>(() => new Board(TestHelper._turtleOnMine));
        }

        [Fact]
        public void BoardCtor_TurtleOnExit_Throws()
        {
            Assert.Throws<GameLoadException>(() => new Board(TestHelper._turtleOnExit));
        }

        [Fact]
        public void BoardCtor_ConflictingMines_Throws()
        {
            Assert.Throws<GameLoadException>(() => new Board(TestHelper._conflictingMines));
        }
    }
}