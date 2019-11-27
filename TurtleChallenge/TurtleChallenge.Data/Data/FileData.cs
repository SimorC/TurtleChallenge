using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using TurtleChallenge.Data.Validation;
using TurtleChallenge.Domain.Interfaces;
using TurtleChallenge.Domain.Model;
using TurtleChallenge.Domain.Model.Enum;

namespace TurtleChallenge.Data.Data
{
    public class FileData : IFileData
    {
        /// <summary>
        /// Loads a Board instance based on a Configuration File path
        /// </summary>
        /// <param name="path">File path</param>
        /// <param name="board">Board to be loaded - if null Game.GameBoard will be used</param>
        /// <returns>Loaded Board (Task)</returns>
        public async Task<Board> LoadConfigurationFile(string path, Board board = null)
        {
            dynamic json = await RetrieveJSON(path);

            FileDataConfigurationValidation.ValidateBoard(json);
            FileDataConfigurationValidation.ValidateExit(json);
            FileDataConfigurationValidation.ValidateTurtle(json);
            FileDataConfigurationValidation.ValidateTurtleDirection(json);

            return SetGameSettings(json, board);
        }

        /// <summary>
        /// Load a Board instance based on a loaded configuration json (dynamic)
        /// </summary>
        /// <param name="json">dynamic json with game configuration</param>
        /// <param name="board">Board to be loaded</param>
        /// <returns>Loaded Board</returns>
        private Board SetGameSettings(dynamic json, Board board = null)
        {
            Board currentBoard = board ?? Game.GameBoard;
            currentBoard = new Board((int)json.BoardSizeX, (int)json.BoardSizeY, this);

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
        /// Loads a Board instance based on a Sequences File path
        /// </summary>
        /// <param name="path">File path</param>
        /// <returns>Empty Task</returns>
        public async Task LoadSequencesFile(string path)
        {
            dynamic json = await RetrieveJSON(path);

            FileDataStepsValidation.ValidateSequences(json);

            SetGameSequences(json);
        }

        /// <summary>
        /// Set the sequences based on a dynamic json
        /// </summary>
        /// <param name="json">dynamic json with Sequences</param>
        private void SetGameSequences(dynamic json)
        {
            Game.Sequences = new List<ActionSequence>();

            foreach (var seq in json.Sequences)
            {
                List<TurtleAction> actions = new List<TurtleAction>();
                actions = GetActions(seq).ToList();

                Game.Sequences.Add(new ActionSequence() { Actions = actions });
            }
        }

        /// <summary>
        /// Loads the actions from a dynamic seq and a list of TurtleActions
        /// </summary>
        /// <param name="seq">dynamic Sequence node from the json</param>
        /// <returns>Enumerable of TurtleAction</returns>
        private static IEnumerable<TurtleAction> GetActions(dynamic seq)
        {
            foreach (var step in seq.Steps)
            {
                TurtleAction act = step.Action == "M" ? TurtleAction.Move : TurtleAction.Rotate;
                yield return act;
            }
        }

        /// <summary>
        /// Retrieves a dynamic json from a physical .json file
        /// </summary>
        /// <param name="path">Path to the file</param>
        /// <returns>Task (dynamic) with loaded json</returns>
        private async Task<dynamic> RetrieveJSON(string path)
        {
            byte[] result;

            using (FileStream SourceStream = File.Open(path, FileMode.Open))
            {
                result = new byte[SourceStream.Length];
                await SourceStream.ReadAsync(result, 0, (int)SourceStream.Length);
            }

            dynamic json = JValue.Parse(Encoding.ASCII.GetString(result));

            return json;
        }
    }
}
