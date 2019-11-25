using System.Collections.Generic;
using System.Linq;
using System;
using TurtleChallenge.Domain.Model.Extension;
using TurtleChallenge.Domain.Interfaces;
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

        public Board(int sizeX, int sizeY, IEnumerable<Tile> tiles, IFileData fileData)
        {
            this._fileData = fileData;
            throw new NotImplementedException();
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

        public Coordinate GetTurtleCoordinate()
        {
            var tileIte = this.Tiles.First(tile => tile.CurrentObject is Turtle);
            return tileIte.Coordinate;
        }

        public Coordinate GetExitCoordinate()
        {
            var tileIte = this.Tiles.First(tile => tile.CurrentObject is Exit);
            return tileIte.Coordinate;
        }

        public List<Coordinate> GetMinesCoordinates()
        {
            var tiles = this.Tiles.Where(tile => tile.CurrentObject is Mine).ToList();
            return tiles.Select(tile => tile.Coordinate).ToList();
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