namespace TurtleChallenge.Domain.Model.Extension
{
    public static class CoordinateExtensions
    {
        /// <summary>
        /// Extension method - compares two Coordinate objects
        /// </summary>
        /// <param name="thisCoordinate">Context Coordinate</param>
        /// <param name="coordinate">Comparison Coordinate</param>
        /// <returns>Boolean whether the Coordinates match or not</returns>
        public static bool IsSame(this Coordinate thisCoordinate, Coordinate coordinate)
        {
            if (thisCoordinate.PosX == coordinate.PosX && thisCoordinate.PosY == coordinate.PosY)
            {
                return true;
            }

            return false;
        }
    }
}
