using Ninject;
using System.Reflection;
using TurtleChallenge.CrossCutting;
using TurtleChallenge.Domain.Interfaces;

namespace TurtleChallenge.ConsoleApp.Helper
{
    public class NinjectHelper
    {
        internal static IGameService GetGameService()
        {
            var kernel = new StandardKernel();
            new LoadInjectionModule(kernel).Load();
            kernel.Load(Assembly.GetExecutingAssembly());
            return kernel.Get<IGameService>();
        }
    }
}