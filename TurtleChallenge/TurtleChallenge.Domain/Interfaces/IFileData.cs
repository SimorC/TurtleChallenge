using System.Threading.Tasks;
using TurtleChallenge.Domain.Model;

namespace TurtleChallenge.Domain.Interfaces
{
    public interface IFileData
    {
        Task<Board> LoadConfigurationFile(string path, Board board = null);
        Task LoadStepsFile(string path);
    }
}