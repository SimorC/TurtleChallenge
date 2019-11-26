using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtleChallenge.Domain.Model.Enum;

namespace TurtleChallenge.Domain.Model.Extension
{
    public static class GameOverExtensions
    {
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
