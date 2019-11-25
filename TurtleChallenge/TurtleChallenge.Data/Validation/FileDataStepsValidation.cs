using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurtleChallenge.Data.Validation
{
    internal static class FileDataStepsValidation
    {
        internal static void ValidateTurtleDirection(dynamic json)
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
