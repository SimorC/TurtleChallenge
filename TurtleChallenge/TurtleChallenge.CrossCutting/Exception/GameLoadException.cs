using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurtleChallenge.CrossCutting.Exception
{
    public class GameLoadException : System.Exception
    {
        public GameLoadException()
        {
        }

        public GameLoadException(string message)
            : base(message)
        {
        }

        public GameLoadException(string message, System.Exception inner)
            : base(message, inner)
        {
        }
    }
}
