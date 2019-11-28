using System.Threading.Tasks;
using TurtleChallenge.Domain.Model;

namespace TurtleChallenge.Domain.Interfaces
{
    public interface IFileData
    {
        Task<dynamic> LoadConfigurationFile(string path, Board board = null);
        Task<dynamic> LoadSequencesFile(string path);
    }
}