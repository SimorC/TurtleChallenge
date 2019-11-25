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
        public Direction Direction { get; private set; }

        public Turtle(Direction direction) : base()
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

        public Coordinate GetCurrentCoordinate()
        {
            throw new NotImplementedException();
        }
    }
}