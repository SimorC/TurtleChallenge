﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
