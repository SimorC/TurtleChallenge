using System.Collections.Generic;
using System;

namespace TurtleChallenge.Domain.Model
{
    public class Board
    {
        public int SizeX { get; set; }
        public int SizeY { get; set; }

        public IEnumerable<GameObject> Tiles { get; set; }

        public Board(int sizeX, int sizeY)
        {
            throw new NotImplementedException();
        }

        public void BuildBoard()
        {
            throw new NotImplementedException();
        }
    }
}