using TurtleChallenge.Domain.Exception;
using TurtleChallenge.Domain.Model;

namespace TurtleChallenge.Domain.Validation
{
    internal static class BoardValidation
    {
        internal static void ValidateTileObject(Tile tile)
        {
            if (tile == null)
            {
                throw new GameLoadException("Tile is out of bounds!");
            }
        }

        internal static void ValidateConcurrentObject(Tile tile)
        {
            if (tile.CurrentObject != null)
            {
                throw new GameLoadException("Conflicting game object!");
            }
        }
    }
}