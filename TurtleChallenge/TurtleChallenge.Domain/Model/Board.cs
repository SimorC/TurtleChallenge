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

        public Board(int sizeX, int sizeY, IFileData fileData)
        {
            this._fileData = fileData;

            this.SizeX = sizeX;
            this.SizeY = sizeY;

            this.Tiles = GetEmptyTiles(sizeX, sizeY).ToList();
        }

        public Board(int sizeX, int sizeY, List<Tile> tiles, IFileData fileData)
        {
            this._fileData = fileData;

            this.SizeX = sizeX;
            this.SizeY = sizeY;

            this.Tiles = tiles;
        }

        #region IsGameWinnable
        // This region is not being used apart from the Tests
        public bool IsGameWinnable()
        {
            List<Coordinate> visitedCoordinates = new List<Coordinate>();
            object lockObject = new object();

            try
            {
                // TODO: Refactor to use multi-thread
                PreviewMovements(lockObject, ref visitedCoordinates, this.GetTurtleCoordinate());
            }
            catch (GameOverException ex)
            {
                return true;
            }

            return false;

        }

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
            catch (System.Exception ex)
            {
                return;
            }

            PreviewAllMovements(lockObject, visitedCoordinates, targetCoordinate);
        }

        private static void AddVisitedLocation(object lockObject, List<Coordinate> visitedCoordinates, Coordinate targetCoordinate)
        {
            lock (lockObject)
            {
                visitedCoordinates.Add(targetCoordinate);
            }
        }

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

        public void AddGameObject(int posX, int posY, GameObject objectTBA)
        {
            Coordinate coord = new Coordinate(posX, posY);

            AddGameObject(coord, objectTBA);
        }

        public void AddGameObject(Coordinate coordinate, GameObject objectTBA)
        {
            var tileItem = this.Tiles.FirstOrDefault(tile => tile.Coordinate.IsSame(coordinate));
            BoardValidation.ValidateTileObject(tileItem);
            BoardValidation.ValidateConcurrentObject(tileItem);
            //if (objectTBA is Turtle)
            //{

            //}
            tileItem.CurrentObject = objectTBA;
        }

        public void ResetTurtle(int posX, int posY)
        {
            Coordinate coord = new Coordinate(posX, posY);

            ResetTurtle(coord);
        }

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

        public void PreviewMoveObject(Coordinate targetCoordinate)
        {
            Tile targetTile = GetTile(targetCoordinate);

            // GameOver.OutOfBounds if tile does not exist
            BoardValidation.ValidateTargetTile(targetTile);

            GameObject targetGameObject = targetTile.CurrentObject;

            BoardValidation.ValidateLegitMovement(targetGameObject);
        }

        public Coordinate GetTurtleCoordinate()
        {
            var tileItem = this.Tiles.First(tile => tile.CurrentObject is Turtle);
            return tileItem.Coordinate;
        }

        public Coordinate GetExitCoordinate()
        {
            var tileItem = this.Tiles.First(tile => tile.CurrentObject is Exit);
            return tileItem.Coordinate;
        }

        public List<Coordinate> GetMinesCoordinates()
        {
            var tiles = this.Tiles.Where(tile => tile.CurrentObject is Mine).ToList();
            return tiles.Select(tile => tile.Coordinate).ToList();
        }

        public Tile GetTile(int posX, int posY)
        {
            return this.GetTile(new Coordinate(posX, posY));
        }

        public Tile GetTile(Coordinate coordinate)
        {
            return this.Tiles.FirstOrDefault(tile => tile.Coordinate.IsSame(coordinate));
        }

        public GameObject GetGameObject(int posX, int posY)
        {
            return this.GetGameObject(new Coordinate(posX, posY));
        }

        public GameObject GetGameObject(Coordinate coordinate)
        {
            Tile tile = this.GetTile(coordinate);
            return tile.CurrentObject;
        }

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