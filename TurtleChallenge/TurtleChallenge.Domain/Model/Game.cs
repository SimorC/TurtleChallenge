using System;
using System.Collections.Generic;
using TurtleChallenge.Domain.Model.Enum;

namespace TurtleChallenge.Domain.Model
{
    public static class Game
    {
        public static Coordinate InitialTurtlePosition { get; set; }
        public static Board GameBoard { get; set; }
        public static object LockBoard { get; set; }
        public static List<ActionSequence> Sequences { get; set; } = new List<ActionSequence>();

        public static List<GameOver> ExecuteSequences()
        {
            throw new NotImplementedException();
        }
    }
}