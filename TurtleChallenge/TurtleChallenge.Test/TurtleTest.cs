using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtleChallenge.CrossCutting.Exception;
using TurtleChallenge.Domain.Model;
using TurtleChallenge.Domain.Model.Enum;
using TurtleChallenge.Test.Helper;

namespace TurtleChallenge.Test
{
    [TestClass]
    public class TurtleTest
    {
        [TestMethod]
        public void Rotate()
        {
            Game.GameBoard = TestHelper.GetEmptyBoard(1, 3);
            Turtle turtle = new Turtle(Direction.North);

            Game.GameBoard.AddGameObject(0, 2, turtle);

            turtle.Rotate();
            Assert.AreEqual(turtle.Direction, Direction.East);

            turtle.Rotate();
            Assert.AreEqual(turtle.Direction, Direction.South);

            turtle.Rotate();
            Assert.AreEqual(turtle.Direction, Direction.West);

            turtle.Rotate();
            Assert.AreEqual(turtle.Direction, Direction.North);

            turtle.Rotate();
            turtle.Rotate();
            Assert.AreEqual(turtle.Direction, Direction.South);
        }

        [TestMethod]
        public void Move()
        {
            Game.GameBoard = TestHelper.GetEmptyBoard(1, 3);
            Turtle turtle = new Turtle(Direction.North);

            Game.GameBoard.AddGameObject(0, 2, turtle);

            turtle.Move();
            Assert.AreEqual(turtle.GetCurrentCoordinate(), new Coordinate(0, 1));

            turtle.Move();
            Assert.AreEqual(turtle.GetCurrentCoordinate(), new Coordinate(0, 0));

            // If standing in the border of the board (?), simply won't move
            turtle.Move();
            Assert.AreEqual(turtle.GetCurrentCoordinate(), new Coordinate(0, 0));
        }

        [TestMethod]
        [ExpectedException(typeof(GameplayException), "Mine hit!")]
        public void MoveToMine()
        {
            Game.GameBoard = TestHelper.GetEmptyBoard(1, 3);
            Mine mine = new Mine();
            Turtle turtle = new Turtle(Direction.North);

            Game.GameBoard.AddGameObject(0, 2, turtle);
            Game.GameBoard.AddGameObject(0, 1, mine);

            turtle.Move();
        }

        [TestMethod]
        [ExpectedException(typeof(GameplayException), "Exited!")]
        public void MoveToExit()
        {
            Game.GameBoard = TestHelper.GetEmptyBoard(1, 3);
            Exit exit = new Exit();
            Turtle turtle = new Turtle(Direction.North);

            Game.GameBoard.AddGameObject(0, 2, turtle);
            Game.GameBoard.AddGameObject(0, 1, exit);

            turtle.Move();
        }
    }
}