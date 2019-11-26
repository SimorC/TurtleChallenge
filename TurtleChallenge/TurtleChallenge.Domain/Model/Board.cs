using System;
using System.Collections.Generic;
using System.Linq;
using TurtleChallenge.Domain.Interfaces;
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
                this._fileData.LoadConfigurationFile(configPath).Wait();
            }
            catch (System.Exception ex)
            {
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

        public bool IsGameWinnable()
        {
            throw new NotImplementedException();
        }

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
            tileItem.CurrentObject = objectTBA;
        }

        public void MoveObject(Coordinate sourceCoordinate, Coordinate targetCoordinate)
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
    }
}