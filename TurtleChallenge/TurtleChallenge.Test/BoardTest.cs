using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtleChallenge.CrossCutting.Exception;
using TurtleChallenge.Domain.Model;
using TurtleChallenge.Test.Helper;

namespace TurtleChallenge.Test
{
    [TestClass]
    public class BoardTest
    {
        [TestMethod]
        public void IsGameWinnable_True()
        {
            Board board = new Board(TestHelper._correctConfigPath);

            bool isWinnable = board.IsGameWinnable();

            Assert.IsFalse(isWinnable);
        }

        [TestMethod]
        public void IsGameWinnable_False1()
        {
            Board board = new Board(TestHelper._nonWinnableConfigPath_1);

            bool isWinnable = board.IsGameWinnable();

            Assert.IsFalse(isWinnable);
        }

        [TestMethod]
        public void IsGameWinnable_False2()
        {
            Board board = new Board(TestHelper._nonWinnableConfigPath_2);

            bool isWinnable = board.IsGameWinnable();

            Assert.IsFalse(isWinnable);
        }

        [TestMethod]
        [ExpectedException(typeof(GameLoadException), "Mines must be inside the board!")]
        public void BoardCtor_MineOutOfBounds_Throws()
        {
            Board board = new Board(TestHelper._mineOOB);
        }

        [TestMethod]
        [ExpectedException(typeof(GameLoadException), "Turtles must be inside the board!")]
        public void BoardCtor_TurtleOutOfBounds_Throws()
        {
            Board board = new Board(TestHelper._turtleOOB);
        }

        [TestMethod]
        [ExpectedException(typeof(GameLoadException), "Exit must be inside the board!")]
        public void BoardCtor_ExitOutOfBounds_Throws()
        {
            Board board = new Board(TestHelper._exitOOB);
        }

        [TestMethod]
        [ExpectedException(typeof(GameLoadException), "Turtle can't start on a mine!")]
        public void BoardCtor_TurtleOnMine_Throws()
        {
            Board board = new Board(TestHelper._turtleOnMine);
        }

        [TestMethod]
        [ExpectedException(typeof(GameLoadException), "Turtle can't start on the exit!")]
        public void BoardCtor_TurtleOnExit_Throws()
        {
            Board board = new Board(TestHelper._turtleOnExit);
        }

        [TestMethod]
        [ExpectedException(typeof(GameLoadException), "There is more than one mine in the same place!")]
        public void BoardCtor_ConflictingMines_Throws()
        {
            Board board = new Board(TestHelper._conflictingMines);
        }
    }
}