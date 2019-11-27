using System.IO;

namespace TurtleChallenge.Data.Validation
{
    internal static class FileDataConfigurationValidation
    {
        /// <summary>
        /// Checks whether the Sizes of the board are properly set
        /// </summary>
        /// <param name="json">dynamic json with configuration file loaded</param>
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

        /// <summary>
        /// Checks whether the Turtle Direction is properly set
        /// </summary>
        /// <param name="json">dynamic json with configuration file loaded</param>
        internal static void ValidateTurtleDirection(dynamic json)
        {
            if (json.TurtleDirection == null || !(json.TurtleDirection.Value is long))
            {
                throw new FileLoadException("Turtle direction not properly configured!");
            }
        }

        /// <summary>
        /// Checks whether the Exit is properly set
        /// </summary>
        /// <param name="json">dynamic json with configuration file loaded</param>
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

        /// <summary>
        /// Checks whether the Turtle Position is properly set
        /// </summary>
        /// <param name="json">dynamic json with configuration file loaded</param>
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