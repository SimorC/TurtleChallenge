using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurtleChallenge.Domain.Model
{
    public abstract class GameObject
    {
        public Coordinate Coordinate { get; set; }
        public GameObject CurrentObject { get; set; }

        public GameObject(int posX, int posY)
        {
            throw new NotImplementedException();
        }

        public GameObject(Coordinate pos)
        {
            throw new NotImplementedException();
        }
    }
}
