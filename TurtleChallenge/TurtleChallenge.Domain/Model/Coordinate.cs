using System;

namespace TurtleChallenge.Domain.Model
{
    public class Coordinate
    {
        public int PosX { get; set; }
        public int PosY { get; set; }

        public Coordinate(int posX, int posY)
        {
            this.PosX = posX;
            this.PosY = posY;
        }

        public Coordinate(Coordinate coordinate)
        {
            this.PosX = coordinate.PosX;
            this.PosY = coordinate.PosY;
        }
    }
}
