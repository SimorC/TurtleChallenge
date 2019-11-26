using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtleChallenge.Domain.Model.Enum;

namespace TurtleChallenge.Domain.Model
{
    public class Turtle : GameObject
    {
        public Direction Direction { get; private set; }
        private Board CurrentBoard { get; set; }

        public Turtle(Direction direction, Board currentBoard = null) : base()
        {
            this.Direction = direction;
            this.CurrentBoard = currentBoard ?? Game.GameBoard;
        }

        public void Move()
        {
            Coordinate moveCoordinate = GetMoveCoordinate();
            this.CurrentBoard.MoveObject(moveCoordinate, GetCurrentCoordinate());
        }

        public void Rotate()
        {
            var currentDirection = (int)this.Direction;
            currentDirection++;

            if (currentDirection == 5)
            {
                currentDirection = 1;
            }

            this.Direction = (Direction)currentDirection;
        }

        public Coordinate GetCurrentCoordinate()
        {
            Coordinate turtleCoordinate = this.CurrentBoard.GetTurtleCoordinate();
            return new Coordinate(turtleCoordinate.PosX, turtleCoordinate.PosY);
        }

        private Coordinate GetMoveCoordinate()
        {
            Coordinate coordinate = this.GetCurrentCoordinate();

            switch (this.Direction)
            {
                case Direction.North:
                    coordinate.PosY--;
                    break;
                case Direction.East:
                    coordinate.PosX++;
                    break;
                case Direction.South:
                    coordinate.PosY++;
                    break;
                case Direction.West:
                    coordinate.PosX--;
                    break;
                default:
                    break;
            }

            return coordinate;
        }

        internal void ExecuteAction(TurtleAction action)
        {
            switch (action)
            {
                case TurtleAction.Move:
                    this.Move();
                    break;
                case TurtleAction.Rotate:
                    this.Rotate();
                    break;
            }
        }
    }
}