using System;
using System.Linq;
using TurtleChallenge.Domain.Exception;
using TurtleChallenge.Domain.Model;
using TurtleChallenge.Domain.Model.Enum;
using TurtleChallenge.Domain.Model.Extension;
using TurtleChallenge.Test.Helper;
using Xunit;

namespace TurtleChallenge.Test
{
    public class TurtleTest
    {
        [Fact]
        public void TurtleRotate_TurtleObjectRotates()
        {
            Board gameBoard = TestHelper.GetEmptyBoard(1, 3);
            Turtle turtle = new Turtle(Direction.North, gameBoard);

            gameBoard.AddGameObject(0, 2, turtle);

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
        public void TurtleRotate_TurtleInTileRotates()
        {
            Board gameBoard = TestHelper.GetEmptyBoard(1, 3);
            Turtle turtle = new Turtle(Direction.North, gameBoard);

            gameBoard.AddGameObject(0, 2, turtle);

            var tile = gameBoard.Tiles.FirstOrDefault(tileItem => tileItem.Coordinate.IsSame(gameBoard.GetTurtleCoordinate()));

            turtle.Rotate();
            Assert.Equal(Direction.East, ((Turtle)tile.CurrentObject).Direction);

            turtle.Rotate();
            Assert.Equal(Direction.South, ((Turtle)tile.CurrentObject).Direction);

            turtle.Rotate();
            Assert.Equal(Direction.West, ((Turtle)tile.CurrentObject).Direction);

            turtle.Rotate();
            Assert.Equal(Direction.North, ((Turtle)tile.CurrentObject).Direction);

            turtle.Rotate();
            turtle.Rotate();
            Assert.Equal(Direction.South, ((Turtle)tile.CurrentObject).Direction);
        }

        [Fact]
        public void TurtleMove_TurtleMovesInBoard()
        {
            Board gameBoard = TestHelper.GetEmptyBoard(1, 3);
            Turtle turtle = new Turtle(Direction.North, gameBoard);

            gameBoard.AddGameObject(0, 2, turtle);

            turtle.Move();
            Assert.True(new Coordinate(0, 1).IsSame(turtle.GetCurrentCoordinate()));

            turtle.Move();
            Assert.True(new Coordinate(0, 0).IsSame(turtle.GetCurrentCoordinate()));
        }

        [Fact]
        public void TurtleMove_TurtleMovesToMine_Throws()
        {
            Board gameBoard = TestHelper.GetEmptyBoard(1, 3);
            Mine mine = new Mine();
            Turtle turtle = new Turtle(Direction.North, gameBoard);

            gameBoard.AddGameObject(0, 1, mine);
            gameBoard.AddGameObject(0, 2, turtle);

            try
            {
                turtle.Move();
                throw new Exception("Mine not hit!");
            }
            catch (GameOverException ex)
            {
                Assert.Equal(GameOver.MineHit, ex.GameOver);
            }
        }

        [Fact]
        public void TurtleMove_TurtleMovesToExit_Throws()
        {
            Board gameBoard = TestHelper.GetEmptyBoard(1, 3);
            Exit exit = new Exit();
            Turtle turtle = new Turtle(Direction.North, gameBoard);

            gameBoard.AddGameObject(0, 2, turtle);
            gameBoard.AddGameObject(0, 1, exit);

            try
            {
                turtle.Move();
                throw new Exception("Exit not hit!");
            }
            catch (GameOverException ex)
            {
                Assert.Equal(GameOver.Success, ex.GameOver);
            }
        }

        [Fact]
        public void TurtleMove_TurtleMovesToOutOfBounds_Throws()
        {
            Board gameBoard = TestHelper.GetEmptyBoard(2, 1);
            Exit exit = new Exit();
            Turtle turtle = new Turtle(Direction.North, gameBoard);

            gameBoard.AddGameObject(0, 0, turtle);
            gameBoard.AddGameObject(1, 0, exit);

            try
            {
                turtle.Move();
                throw new Exception("OutofBounds not reached!");
            }
            catch (GameOverException ex)
            {
                Assert.Equal(GameOver.OutOfBounds, ex.GameOver);
            }
        }
    }
}