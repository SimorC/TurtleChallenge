using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurtleChallenge.Domain.Model
{
    public class Exit : GameObject
    {
        public Exit(int posX, int posY) : base(posX, posY)
        {

        }

        public Exit(Coordinate coordinate) : base(coordinate)
        {

        }
    }
}
