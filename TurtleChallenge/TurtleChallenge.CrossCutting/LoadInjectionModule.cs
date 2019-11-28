using Ninject;
using Ninject.Modules;
using TurtleChallenge.Infra.Data;
using TurtleChallenge.Domain.Interfaces;

namespace TurtleChallenge.CrossCutting
{
    public class LoadInjectionModule : NinjectModule
    {
        StandardKernel _kernel;
        public LoadInjectionModule(StandardKernel kernel)
        {
            this._kernel = kernel;
        }

        /// <summary>
        /// Loads the Injection mapping
        /// </summary>
        public override void Load()
        {
            this._kernel.Bind<IFileData>().To<FileData>();
        }
    }
}
