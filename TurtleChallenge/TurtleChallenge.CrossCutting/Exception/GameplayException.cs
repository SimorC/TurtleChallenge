using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurtleChallenge.CrossCutting.Exception
{
    public class GameplayException : System.Exception
    {
        public GameplayException()
        {
        }

        public GameplayException(string message)
            : base(message)
        {
        }

        public GameplayException(string message, System.Exception inner)
            : base(message, inner)
        {
        }
    }
}
