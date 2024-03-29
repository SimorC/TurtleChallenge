﻿using System.Collections.Generic;

namespace TurtleChallenge.Domain.Model
{
    public static class Game
    {
        public static Coordinate InitialTurtlePosition { get; set; }
        public static Board GameBoard { get; set; }
        public static List<ActionSequence> Sequences { get; set; } = new List<ActionSequence>();
    }
}