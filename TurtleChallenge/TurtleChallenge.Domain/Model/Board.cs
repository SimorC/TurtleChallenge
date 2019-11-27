using System.Collections.Generic;
using System.Linq;
using TurtleChallenge.Domain.Exception;
using TurtleChallenge.Domain.Interfaces;
using TurtleChallenge.Domain.Model.Enum;
using TurtleChallenge.Domain.Model.Extension;
using TurtleChallenge.Domain.Validation;

namespace TurtleChallenge.Domain.Model
{
    public class Board
    {
        public int SizeX { get; set; }
        public int SizeY { get; set; }

        public List<Tile> Tiles { get; set; }

        private readonly IFileData _fileData;

        /// <summary>
        /// Starts the instance with a configuration file path and a FileData object
        /// </summary>
        /// <param name="configPath">Configuration file path</param>
        /// <param name="fileData">FileData object</param>
        public Board(string configPath, IFileData fileData)
        {
            this._fileData = fileData;

            try
            {
                Board board = this._fileData.LoadConfigurationFile(configPath, this).GetAwaiter().GetResult();

                this.SizeX = board.SizeX;
                this.SizeY = board.SizeY;
                this.Tiles = board.Tiles;
            }
            catch (System.Exception ex)
            {
                if (ex is GameLoadException)
                {
                    throw ex;
                }

                throw ex.InnerException;
            }
        }

        /// <summary>
        /// Starts the instance with the sizes of the board and a FileData object
        /// </summary>
        /// <param name="sizeX">Size of X axis</param>
        /// <param name="sizeY">Size of Y axis</param>
        /// <param name="fileData">FileData object</param>
        public Board(int sizeX, int sizeY, IFileData fileData)
        {
            this._fileData = fileData;

            this.SizeX = sizeX;
            this.SizeY = sizeY;

            this.Tiles = GetEmptyTiles(sizeX, sizeY).ToList();
        }

        /// <summary>
        /// Starts the instance with the sizes of the board, a list if Tile's and a FileData object
        /// </summary>
        /// <param name="sizeX">Size of X axis</param>
        /// <param name="sizeY">Size of Y axis</param>
        /// <param name="tiles">List of Tile's</param>
        /// <param name="fileData">FileData object</param>
        public Board(int sizeX, int sizeY, List<Tile> tiles, IFileData fileData)
        {
            this._fileData = fileData;

            this.SizeX = sizeX;
            this.SizeY = sizeY;

            this.Tiles = tiles;
        }

        #region IsGameWinnable
        /// <summary>
        /// Checks if a loaded game is winnable (not being used apart from the Tests)
        /// </summary>
        /// <returns>Boolean flag to say if the game is beatlable or not</returns>
        public bool IsGameWinnable()
        {
            List<Coordinate> visitedCoordinates = new List<Coordinate>();
            object lockObject = new object();

            try
            {
                // TODO: Refactor to use multi-thread
                PreviewMovements(lockObject, ref visitedCoordinates, this.GetTurtleCoordinate());
            }
            catch (GameOverException)
            {
                return true;
            }

            return false;

        }

        /// <summary>
        /// Previews the movement without changing the turtle position
        /// </summary>
        /// <param name="lockObject">"Lock" object for multi-threading</param>
        /// <param name="visitedCoordinates">List of "visited" Coordinate's to avoid infinite loops</param>
        /// <param name="targetCoordinate">Coordinate to which the turtle could move</param>
        private void PreviewMovements(object lockObject, ref List<Coordinate> visitedCoordinates, Coordinate targetCoordinate)
        {
            if (visitedCoordinates.Any(visited => visited.IsSame(targetCoordinate)))
            {
                return;
            }

            AddVisitedLocation(lockObject, visitedCoordinates, targetCoordinate);

            try
            {
                this.PreviewMoveObject(targetCoordinate);
            }
            catch (GameOverException ex)
            {
                if (ex.GameOver == Enum.GameOver.Success) { throw; }

                return;
            }
            catch (System.Exception)
            {
                return;
            }

            PreviewAllMovements(lockObject, visitedCoordinates, targetCoordinate);
        }

        /// <summary>
        /// Adds a Coordinate to the list of "visited" Coordinate's
        /// </summary>
        /// <param name="lockObject">"Lock" object for multi-threading</param>
        /// <param name="visitedCoordinates">List of "visited" Coordinate's to avoid infinite loops</param>
        /// <param name="targetCoordinate">Coordinate to which the turtle could move</param>
        private static void AddVisitedLocation(object lockObject, List<Coordinate> visitedCoordinates, Coordinate targetCoordinate)
        {
            lock (lockObject)
            {
                visitedCoordinates.Add(targetCoordinate);
            }
        }

        /// <summary>
        /// Previews all 4 movements from a given coordinate
        /// </summary>
        /// <param name="lockObject">"Lock" object for multi-threading</param>
        /// <param name="visitedCoordinates">List of "visited" Coordinate's to avoid infinite loops</param>
        /// <param name="targetCoordinate">Coordinate to which the turtle could move</param>
        private void PreviewAllMovements(object lockObject, List<Coordinate> visitedCoordinates, Coordinate targetCoordinate)
        {
            var nMove = new Coordinate(targetCoordinate);
            var eMove = new Coordinate(targetCoordinate);
            var sMove = new Coordinate(targetCoordinate);
            var wMove = new Coordinate(targetCoordinate);

            nMove.PosY--;
            eMove.PosX++;
            sMove.PosY++;
            wMove.PosX--;

            PreviewMovements(lockObject, ref visitedCoordinates, nMove);
            PreviewMovements(lockObject, ref visitedCoordinates, eMove);
            PreviewMovements(lockObject, ref visitedCoordinates, sMove);
            PreviewMovements(lockObject, ref visitedCoordinates, wMove);
        }
        #endregion

        /// <summary>
        /// Adds a GameObject to a Board
        /// </summary>
        /// <param name="posX">Position on X axis</param>
        /// <param name="posY">Position on Y axis</param>
        /// <param name="objectTBA">Object to be added</param>
        public void AddGameObject(int posX, int posY, GameObject objectTBA)
        {
            Coordinate coord = new Coordinate(posX, posY);

            AddGameObject(coord, objectTBA);
        }

        /// <summary>
        /// Adds a GameObject to a Board
        /// </summary>
        /// <param name="coordinate">Coordinate to be added</param>
        /// <param name="objectTBA">Object to be added</param>
        public void AddGameObject(Coordinate coordinate, GameObject objectTBA)
        {
            var tileItem = this.Tiles.FirstOrDefault(tile => tile.Coordinate.IsSame(coordinate));

            BoardValidation.ValidateTileObject(tileItem);
            BoardValidation.ValidateConcurrentObject(tileItem);

            tileItem.CurrentObject = objectTBA;
        }

        /// <summary>
        /// Resets the turtle position to the initial position
        /// </summary>
        /// <param name="posX">Position on X axis</param>
        /// <param name="posY">Position on Y axis</param>
        public void ResetTurtle(int posX, int posY)
        {
            Coordinate coord = new Coordinate(posX, posY);

            ResetTurtle(coord);
        }

        /// <summary>
        /// Resets the turtle position to the initial position
        /// </summary>
        /// <param name="initialPosition">Initial position coordinate</param>
        public void ResetTurtle(Coordinate initialPosition)
        {
            // Ignore if it's already in the initial position
            if (!initialPosition.IsSame(this.GetTurtleCoordinate()))
            {
                Tile initialTile = this.GetTile(initialPosition);
                Tile currentTile = this.GetTile(this.GetTurtleCoordinate());

                initialTile.CurrentObject = currentTile.CurrentObject;
                currentTile.CurrentObject = null;
            }
        }

        /// <summary>
        /// Moves an object on the Board
        /// </summary>
        /// <param name="targetCoordinate">Target Coordinate to be moved</param>
        /// <param name="sourceCoordinate">Source Coordinate where the object stands currently</param>
        public void MoveObject(Coordinate targetCoordinate, Coordinate sourceCoordinate)
        {
            Tile currentTile = GetTile(sourceCoordinate);
            Tile targetTile = GetTile(targetCoordinate);

            BoardValidation.ValidateSourceTile(currentTile);
            // GameOver.OutOfBounds if tile does not exist
            BoardValidation.ValidateTargetTile(targetTile);

            GameObject sourceGameObject = currentTile.CurrentObject;
            GameObject targetGameObject = targetTile.CurrentObject;

            BoardValidation.ValidateMovingObject(sourceGameObject);
            BoardValidation.ValidateLegitMovement(targetGameObject);

            targetTile.CurrentObject = sourceGameObject;
            currentTile.CurrentObject = null;
        }

        /// <summary>
        /// Previews a movement of an object on the Board
        /// </summary>
        /// <param name="targetCoordinate">Target Coordinate to be moved</param>
        public void PreviewMoveObject(Coordinate targetCoordinate)
        {
            Tile targetTile = GetTile(targetCoordinate);

            // GameOver.OutOfBounds if tile does not exist
            BoardValidation.ValidateTargetTile(targetTile);

            GameObject targetGameObject = targetTile.CurrentObject;

            BoardValidation.ValidateLegitMovement(targetGameObject);
        }

        /// <summary>
        /// Retrieves the current Turtle Coordinate
        /// </summary>
        /// <returns>Turtle Coordinate</returns>
        public Coordinate GetTurtleCoordinate()
        {
            var tileItem = this.Tiles.First(tile => tile.CurrentObject is Turtle);
            return tileItem.Coordinate;
        }

        /// <summary>
        /// Retrieves the current Exit Coordinate
        /// </summary>
        /// <returns>Exit Coordinate</returns>
        public Coordinate GetExitCoordinate()
        {
            var tileItem = this.Tiles.First(tile => tile.CurrentObject is Exit);
            return tileItem.Coordinate;
        }

        /// <summary>
        /// Retrieves a list of Mine's Coordinates
        /// </summary>
        /// <returns>List of Mine Coordinate</returns>
        public List<Coordinate> GetMinesCoordinates()
        {
            var tiles = this.Tiles.Where(tile => tile.CurrentObject is Mine).ToList();
            return tiles.Select(tile => tile.Coordinate).ToList();
        }

        /// <summary>
        /// Get a Tile object based in the position
        /// </summary>
        /// <param name="posX">Position on X axis</param>
        /// <param name="posY">Position on Y axis</param>
        /// <returns>Tile object from the given Coordinate</returns>
        public Tile GetTile(int posX, int posY)
        {
            return this.GetTile(new Coordinate(posX, posY));
        }

        /// <summary>
        /// Get a Tile object based in the position
        /// </summary>
        /// <param name="coordinate">Required Coordinate</param>
        /// <returns>Tile object from the given Coordinate</returns>
        public Tile GetTile(Coordinate coordinate)
        {
            return this.Tiles.FirstOrDefault(tile => tile.Coordinate.IsSame(coordinate));
        }

        /// <summary>
        /// Retrieves a GameObject from a given position
        /// </summary>
        /// <param name="posX">Position on X axis</param>
        /// <param name="posY">Position on Y axis</param>
        /// <returns>GameObject from given position</returns>
        public GameObject GetGameObject(int posX, int posY)
        {
            return this.GetGameObject(new Coordinate(posX, posY));
        }

        /// <summary>
        /// Retrieves a GameObject from a given position
        /// </summary>
        /// <param name="coordinate">Required Coordinate</param>
        /// <returns>GameObject from given position</returns>
        public GameObject GetGameObject(Coordinate coordinate)
        {
            Tile tile = this.GetTile(coordinate);
            return tile.CurrentObject;
        }

        /// <summary>
        /// Creat a list of "Empty" Tile objects
        /// </summary>
        /// <param name="sizeX">Size of the X axis</param>
        /// <param name="sizeY">Size of the Y axis</param>
        /// <returns>IEnumerable of "Empty" Tiles</returns>
        private IEnumerable<Tile> GetEmptyTiles(int sizeX, int sizeY)
        {
            for (int i = 0; i < sizeX; i++)
            {
                for (int j = 0; j < sizeY; j++)
                {
                    yield return new Tile(i, j, null);
                }
            }
        }

        /// <summary>
        /// Executes a list of Sequences on the board
        /// </summary>
        /// <param name="sequences">List of Sequence's to be executed</param>
        /// <param name="initialTurtleCoordinate">Initial Turtle coordinate on the Board</param>
        /// <param name="turtle">Turtle object</param>
        /// <returns></returns>
        public IEnumerable<GameOver> ExecuteSequences(List<ActionSequence> sequences, Coordinate initialTurtleCoordinate = null, Turtle turtle = null)
        {
            initialTurtleCoordinate = initialTurtleCoordinate ?? Game.GameBoard.GetTurtleCoordinate();
            turtle = turtle ?? Game.GameBoard.GetGameObject(initialTurtleCoordinate) as Turtle;

            foreach (var actionSeq in sequences)
            {
                Coordinate initialPos = new Coordinate(initialTurtleCoordinate);
                this.ResetTurtle(initialPos);

                GameOver gameOver = GameOver.Unset;

                this.ExecuteActions(turtle, actionSeq, ref gameOver);

                yield return gameOver == GameOver.Unset ? GameOver.StillInDanger : gameOver;
            }
        }

        /// <summary>
        /// Execute the Turtle actions
        /// </summary>
        /// <param name="turtle">Turtle object</param>
        /// <param name="actionSeq">List of Actions to be executed</param>
        /// <param name="gameOver">ref GameOver enum of the result of Sequence</param>
        private void ExecuteActions(Turtle turtle, ActionSequence actionSeq, ref GameOver gameOver)
        {
            foreach (var action in actionSeq.Actions)
            {
                try
                {
                    turtle.ExecuteAction(action);
                }
                catch (GameOverException ex)
                {
                    gameOver = ex.GameOver;
                    break;
                }
            }
        }
    }
}