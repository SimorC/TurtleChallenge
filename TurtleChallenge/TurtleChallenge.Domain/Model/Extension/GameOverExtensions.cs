using TurtleChallenge.Domain.Model.Enum;

namespace TurtleChallenge.Domain.Model.Extension
{
    public static class GameOverExtensions
    {
        /// <summary>
        /// Returns a Friendly String from a GameOver enum value
        /// </summary>
        /// <param name="thisGameOver">Context GameOver enum</param>
        /// <returns>Friendly string</returns>
        public static string ToFriendlyString(this GameOver thisGameOver)
        {
            switch (thisGameOver)
            {
                case GameOver.Success:
                    return "Success!";
                case GameOver.MineHit:
                    return "Mine hit.";
                case GameOver.StillInDanger:
                    return "The turtle is still in danger!";
                case GameOver.OutOfBounds:
                    return "Turtle fell off the board (Out of bounds).";
                default:
                    return "";
            }
        }
    }
}
