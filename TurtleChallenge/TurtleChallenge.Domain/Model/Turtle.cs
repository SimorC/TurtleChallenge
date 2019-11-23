using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtleChallenge.Domain.Model.Enum;

namespace TurtleChallenge.Domain.Model
{
    public class Turtle : GameObject
    {
        public Direction Direction { get; set; }

        public Turtle(int posX, int posY) : base(posX, posY)
        {
            throw new NotImplementedException();
        }

        public Turtle(Coordinate coordinate) : base(coordinate)
        {
            throw new NotImplementedException();
        }

        public void Move()
        {
            throw new NotImplementedException();
        }

        public void Rotate()
        {
            throw new NotImplementedException();
        }
    }
}
