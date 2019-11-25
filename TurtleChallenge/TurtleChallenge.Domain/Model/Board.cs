using System.Collections.Generic;
using System;

namespace TurtleChallenge.Domain.Model
{
    public class Board
    {
        public int SizeX { get; set; }
        public int SizeY { get; set; }

        public IEnumerable<Tile> Tiles { get; set; }

        public Board(string configPath)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public void AddGameObject(Coordinate coordinate, GameObject objectTBA)
        {
            throw new NotImplementedException();
        }
    }
}