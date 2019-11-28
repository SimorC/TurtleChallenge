using System.Collections.Generic;
using System.Linq;
using TurtleChallenge.Domain.Interfaces;
using TurtleChallenge.Domain.Model;
using TurtleChallenge.Domain.Model.Enum;

namespace TurtleChallenge.Domain.Service
{
    public class GameService : IGameService
    {
        private readonly IFileData _fileData;

        public GameService(IFileData fileData)
        {
            this._fileData = fileData;
        }

        /// <summary>
        /// Retrieves a Board based on a file
        /// </summary>
        /// <param name="path">Full path for the file</param>
        /// <param name="board">Board instance to be loaded</param>
        /// <returns>Loaded board instance</returns>
        public Board GetBoardFromConfigurationFile(string path)
        {
            dynamic json = this._fileData.LoadConfigurationFile(path).GetAwaiter().GetResult();

            return this.GetGameSettings(json);
        }

        /// <summary>
        /// Retrieves the list of ActionSequence based on a file
        /// </summary>
        /// <param name="path">Full path for the file</param>
        /// <returns>List of loaded ActionSequence</returns>
        public List<ActionSequence> GetSequencesFromFile(string path)
        {
            dynamic json = this._fileData.LoadSequencesFile(path).GetAwaiter().GetResult();

            IEnumerable<ActionSequence> lstActSeq = this.GetGameSequences(json);

            return lstActSeq.ToList();
        }

        /// <summary>
        /// Gets a Board instance based on a loaded configuration json (dynamic)
        /// </summary>
        /// <param name="json">dynamic json with game configuration</param>
        /// <param name="board">Board to be loaded</param>
        /// <returns>Loaded Board</returns>
        private Board GetGameSettings(dynamic json)
        {
            Board currentBoard = new Board((int)json.BoardSizeX, (int)json.BoardSizeY, this);

            Turtle turtle = new Turtle((Direction)json.TurtleDirection, currentBoard);
            Exit exit = new Exit();

            // Add Turtle
            currentBoard.AddGameObject((int)json.TurtlePosX, (int)json.TurtlePosY, turtle);
            // Add Exit
            currentBoard.AddGameObject((int)json.ExitPosX, (int)json.ExitPosY, exit);

            // Add Mines
            foreach (var item in json.Mines)
            {
                Mine mine = new Mine();
                currentBoard.AddGameObject((int)item.MinePosX, (int)item.MinePosY, mine);
            }

            return currentBoard;
        }

        /// <summary>
        /// Get the Enumerable of ActionSEquence from a dynamic json
        /// </summary>
        /// <param name="json">loaded dynamic json</param>
        /// <returns>Enumerable of ActionSequence loaded from json</returns>
        private IEnumerable<ActionSequence> GetGameSequences(dynamic json)
        {
            List<ActionSequence> actionSeq = new List<ActionSequence>();

            foreach (var seq in json.Sequences)
            {
                IEnumerable<TurtleAction> actions = GetActions(seq);

                yield return new ActionSequence() { Actions = actions.ToList() };
            }
        }

        /// <summary>
        /// Loads the actions from a dynamic seq and a list of TurtleActions
        /// </summary>
        /// <param name="seq">dynamic Sequence node from the json</param>
        /// <returns>Enumerable of TurtleAction</returns>
        private IEnumerable<TurtleAction> GetActions(dynamic seq)
        {
            foreach (var step in seq.Steps)
            {
                TurtleAction act = step.Action == "M" ? TurtleAction.Move : TurtleAction.Rotate;
                yield return act;
            }
        }
    }
}
