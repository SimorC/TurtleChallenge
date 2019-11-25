using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurtleChallenge.Data.Validation
{
    internal static class FileDataConfigurationValidation
    {
        internal static void ValidateBoard(dynamic json)
        {
            if (json.BoardSizeX == null || !(json.BoardSizeX.Value is long))
            {
                throw new FileLoadException("Board (Size X) not properly configured!");
            }

            if (json.BoardSizeY == null || !(json.BoardSizeY.Value is long))
            {
                throw new FileLoadException("Board (Size Y) not properly configured!");
            }
        }

        internal static void ValidateTurtleDirection(dynamic json)
        {
            if (json.TurtleDirection == null || !(json.TurtleDirection.Value is long))
            {
                throw new FileLoadException("Turtle direction not properly configured!");
            }
        }

        internal static void ValidateExit(dynamic json)
        {
            if (json.ExitPosX == null || !(json.ExitPosX.Value is long))
            {
                throw new FileLoadException("Exit (Pos X) not properly configured!");
            }

            if (json.ExitPosY == null || !(json.ExitPosY.Value is long))
            {
                throw new FileLoadException("Board (Pos Y) not properly configured!");
            }
        }

        internal static void ValidateTurtle(dynamic json)
        {
            if (json.TurtlePosX == null || !(json.TurtlePosX.Value is long))
            {
                throw new FileLoadException("Turtle (Pos X) not properly configured!");
            }

            if (json.TurtlePosY == null || !(json.TurtlePosY.Value is long))
            {
                throw new FileLoadException("Turtle (Pos Y) not properly configured!");
            }
        }
    }
}