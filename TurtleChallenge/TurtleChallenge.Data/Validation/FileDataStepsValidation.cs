using Newtonsoft.Json.Linq;
using System.IO;

namespace TurtleChallenge.Data.Validation
{
    internal static class FileDataStepsValidation
    {
        /// <summary>
        /// Checks whether the Sequences and Steps are properly configured
        /// </summary>
        /// <param name="json">dynamic json with loaded sequences</param>
        internal static void ValidateSequences(dynamic json)
        {
            if (!(json.Sequences is JArray) || json.Sequences.Count < 1)
            {
                throw new FileLoadException("Sequences not properly configured!");
            }

            foreach (var item in json.Sequences)
            {
                if (!(item.Steps is JArray) || item.Steps.Count < 1)
                {
                    throw new FileLoadException("Sequence steps not properly configured!");
                }
            }
        }
    }
}
