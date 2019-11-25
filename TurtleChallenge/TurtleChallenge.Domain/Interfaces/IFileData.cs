using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurtleChallenge.Domain.Interfaces
{
    public interface IFileData
    {
        void LoadConfigurationFile(string path);
        void LoadStepsFile(string path);
    }
}