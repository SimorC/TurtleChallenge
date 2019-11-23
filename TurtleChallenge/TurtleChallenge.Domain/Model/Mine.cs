using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurtleChallenge.Domain.Model
{
    public class Mine : GameObject
    {
        public Mine(int posX, int posY) : base(posX, posY)
        {

        }

        public Mine(Coordinate coordinate) : base(coordinate)
        {

        }
    }
}
