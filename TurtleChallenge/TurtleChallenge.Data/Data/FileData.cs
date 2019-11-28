using Newtonsoft.Json.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using TurtleChallenge.Domain.Interfaces;
using TurtleChallenge.Domain.Model;
using TurtleChallenge.Infra.Validation;

namespace TurtleChallenge.Infra.Data
{
    public class FileData : IFileData
    {
        /// <summary>
        /// Loads a Board instance based on a Configuration File path
        /// </summary>
        /// <param name="path">File path</param>
        /// <param name="board">Board to be loaded - if null Game.GameBoard will be used</param>
        /// <returns>Loaded Board (Task)</returns>
        public async Task<dynamic> LoadConfigurationFile(string path, Board board = null)
        {
            dynamic json = await RetrieveJSON(path);

            FileDataConfigurationValidation.ValidateBoard(json);
            FileDataConfigurationValidation.ValidateExit(json);
            FileDataConfigurationValidation.ValidateTurtle(json);
            FileDataConfigurationValidation.ValidateTurtleDirection(json);

            return json;
        }

        /// <summary>
        /// Loads a Board instance based on a Sequences File path
        /// </summary>
        /// <param name="path">File path</param>
        /// <returns>Empty Task</returns>
        public async Task<dynamic> LoadSequencesFile(string path)
        {
            dynamic json = await RetrieveJSON(path);

            FileDataStepsValidation.ValidateSequences(json);

            return json;
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
