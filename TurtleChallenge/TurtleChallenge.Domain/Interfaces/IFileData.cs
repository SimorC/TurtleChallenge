using System.Threading.Tasks;

namespace TurtleChallenge.Domain.Interfaces
{
    public interface IFileData
    {
        Task LoadConfigurationFile(string path);
        Task LoadStepsFile(string path);
    }
}