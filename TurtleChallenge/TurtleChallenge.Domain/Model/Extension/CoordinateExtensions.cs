using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurtleChallenge.Domain.Model.Extension
{
    public static class CoordinateExtensions
    {
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
