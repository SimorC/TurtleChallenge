using TurtleChallenge.Domain.Exception;
using TurtleChallenge.Domain.Model;
using TurtleChallenge.Domain.Model.Enum;

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
                throw new GameLoadException("Conflicting game object position!");
            }
        }

        internal static void ValidateSourceTile(Tile tile)
        {
            if (tile == null)
            {
                throw new GameplayException("Tile does not exist!");
            }
        }

        internal static void ValidateMovingObject(GameObject gameObject)
        {
            if (gameObject == null)
            {
                throw new GameplayException("Moving object can't be null!");
            }

            if (gameObject is Mine)
            {
                throw new GameplayException("Mines are immovable!");
            }

            if (gameObject is Exit)
            {
                throw new GameplayException("Exits are immovable!");
            }
        }

        internal static void ValidateTargetTile(Tile tile)
        {
            if (tile == null)
            {
                throw new GameOverException(GameOver.OutOfBounds, "Out of bounds movement!");
            }
        }

        internal static void ValidateLegitMovement(GameObject targetGameObject)
        {
            if (targetGameObject is Mine)
            {
                throw new GameOverException(GameOver.MineHit, "Mine hit!");
            }

            if (targetGameObject is Exit)
            {
                throw new GameOverException(GameOver.Success, "Successfully reached the exit!");
            }
        }
    }
}