using Newtonsoft.Json.Linq;
using System;
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
        public async Task<Board> LoadConfigurationFile(string path, Board board = null)
        {
            dynamic json = await RetrieveJSON(path);

            FileDataConfigurationValidation.ValidateBoard(json);
            FileDataConfigurationValidation.ValidateExit(json);
            FileDataConfigurationValidation.ValidateTurtle(json);
            FileDataConfigurationValidation.ValidateTurtleDirection(json);

            return SetGameSettings(json, board);
        }

        private Board SetGameSettings(dynamic json, Board board = null)
        {
            Board currentBoard = board ?? Game.GameBoard;

            Turtle turtle = new Turtle((Direction)json.TurtleDirection);
            Exit exit = new Exit();
            currentBoard = new Board((int)json.BoardSizeX, (int)json.BoardSizeY, this);

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

        public async Task LoadStepsFile(string path)
        {
            dynamic json = await RetrieveJSON(path);

            FileDataStepsValidation.ValidateTurtleDirection(json);

            SetGameSequences(json);
        }

        private void SetGameSequences(dynamic json)
        {
            Game.Sequences = new List<ActionSequence>();

            foreach (var seq in json.Sequences)
            {
                List<TurtleAction> actions = new List<TurtleAction>();
                SetActions(seq, ref actions);

                Game.Sequences.Add(new ActionSequence() { Actions = actions });
            }
        }

        private static void SetActions(dynamic seq, ref List<TurtleAction> actions)
        {
            foreach (var step in seq.Steps)
            {
                TurtleAction act = step.Action == "M" ? TurtleAction.Move : TurtleAction.Rotate;
                actions.Add(act);
            }
        }

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
