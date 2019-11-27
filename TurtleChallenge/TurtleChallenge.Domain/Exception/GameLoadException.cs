namespace TurtleChallenge.Domain.Exception
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
