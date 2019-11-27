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

        /// <summary>
        /// Moves the Turtle in the current direction
        /// </summary>
        public void Move()
        {
            Coordinate moveCoordinate = GetMoveCoordinate();
            this.CurrentBoard.MoveObject(moveCoordinate, GetCurrentCoordinate());
        }

        /// <summary>
        /// Rotates the Turtle
        /// </summary>
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

        /// <summary>
        /// Retrieves the current Coordinate of the Turtle in the Board
        /// </summary>
        /// <returns>Current Coordinate</returns>
        public Coordinate GetCurrentCoordinate()
        {
            Coordinate turtleCoordinate = this.CurrentBoard.GetTurtleCoordinate();
            return new Coordinate(turtleCoordinate.PosX, turtleCoordinate.PosY);
        }

        /// <summary>
        /// Get target Coordinate for moving
        /// </summary>
        /// <returns>Target Coordinate</returns>
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

        /// <summary>
        /// Executes an action (Move/Rotate) based on a TurtleAction enum
        /// </summary>
        /// <param name="action">TurtleAction enum</param>
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