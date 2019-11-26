using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtleChallenge.Domain.Model.Enum;

namespace TurtleChallenge.Domain.Exception
{
    public class GameOverException : System.Exception
    {
        public GameOver GameOver { get; }

        public GameOverException(GameOver gameOver)
        {
            this.GameOver = gameOver;
        }

        public GameOverException(GameOver gameOver, string message)
            : base(message)
        {
            this.GameOver = gameOver;
        }

        public GameOverException(GameOver gameOver, string message, System.Exception inner)
            : base(message, inner)
        {
            this.GameOver = gameOver;
        }
    }
}
