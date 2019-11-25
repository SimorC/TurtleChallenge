using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurtleChallenge.Domain.Model
{
    public class Tile
    {
        public Coordinate Coordinate { get; set; }
        public GameObject CurrentObject { get; set; }

        public Tile(Coordinate coordinate, GameObject gameObject)
        {
            throw new NotImplementedException();
        }

        public Tile(int posX, int posY, GameObject gameObject)
        {
            throw new NotImplementedException();
        }
    }
}