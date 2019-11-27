namespace TurtleChallenge.Domain.Exception
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
