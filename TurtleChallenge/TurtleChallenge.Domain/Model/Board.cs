using System.Collections.Generic;
using System.Linq;
using System;
using TurtleChallenge.Domain.Model.Extension;

namespace TurtleChallenge.Domain.Model
{
    public class Board
    {
        public int SizeX { get; set; }
        public int SizeY { get; set; }

        public List<Tile> Tiles { get; set; }

        public Board(string configPath)
        {
            throw new NotImplementedException();
        }

        public Board(int sizeX, int sizeY)
        {
            this.SizeX = sizeX;
            this.SizeY = sizeY;

            this.Tiles = GetEmptyTiles(sizeX, sizeY).ToList();
        }

        public Board(int sizeX, int sizeY, IEnumerable<Tile> tiles)
        {
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
            var tileItem = this.Tiles.First(tile => tile.Coordinate.IsSame(coordinate));
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