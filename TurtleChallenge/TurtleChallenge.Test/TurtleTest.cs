using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtleChallenge.Domain.Exception;
using TurtleChallenge.Domain.Model;
using TurtleChallenge.Domain.Model.Enum;
using TurtleChallenge.Test.Helper;
using Xunit;

namespace TurtleChallenge.Test
{
    public class TurtleTest
    {
        [Fact]
        public void Rotate()
        {
            Game.GameBoard = TestHelper.GetEmptyBoard(1, 3);
            Turtle turtle = new Turtle(Direction.North);

            Game.GameBoard.AddGameObject(0, 2, turtle);

            turtle.Rotate();
            Assert.Equal(Direction.East, turtle.Direction);

            turtle.Rotate();
            Assert.Equal(Direction.South, turtle.Direction);

            turtle.Rotate();
            Assert.Equal(Direction.West, turtle.Direction);

            turtle.Rotate();
            Assert.Equal(Direction.North, turtle.Direction);

            turtle.Rotate();
            turtle.Rotate();
            Assert.Equal(Direction.South, turtle.Direction);
        }

        [Fact]
        public void Move()
        {
            Game.GameBoard = TestHelper.GetEmptyBoard(1, 3);
            Turtle turtle = new Turtle(Direction.North);

            Game.GameBoard.AddGameObject(0, 2, turtle);

            turtle.Move();
            Assert.Equal(new Coordinate(0, 1), turtle.GetCurrentCoordinate());

            turtle.Move();
            Assert.Equal(new Coordinate(0, 0), turtle.GetCurrentCoordinate());

            // If standing in the border of the board (?), simply won't move
            turtle.Move();
            Assert.Equal(new Coordinate(0, 0), turtle.GetCurrentCoordinate());
        }

        [Fact]
        public void MoveToMine()
        {
            Game.GameBoard = TestHelper.GetEmptyBoard(1, 3);
            Mine mine = new Mine();
            Turtle turtle = new Turtle(Direction.North);

            Game.GameBoard.AddGameObject(0, 2, turtle);
            Game.GameBoard.AddGameObject(0, 1, mine);

            Assert.Throws<GameplayException>(() => turtle.Move());
        }

        [Fact]
        public void MoveToExit()
        {
            Game.GameBoard = TestHelper.GetEmptyBoard(1, 3);
            Exit exit = new Exit();
            Turtle turtle = new Turtle(Direction.North);

            Game.GameBoard.AddGameObject(0, 2, turtle);
            Game.GameBoard.AddGameObject(0, 1, exit);

            Assert.Throws<GameplayException>(() => turtle.Move());
        }
    }
}