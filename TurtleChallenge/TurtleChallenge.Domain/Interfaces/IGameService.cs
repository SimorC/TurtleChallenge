using System.Collections.Generic;
using TurtleChallenge.Domain.Model;

namespace TurtleChallenge.Domain.Interfaces
{
    public interface IGameService
    {
        Board GetBoardFromConfigurationFile(string path);
        List<ActionSequence> GetSequencesFromFile(string path);
    }
}
