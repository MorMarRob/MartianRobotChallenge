using Infrastructure.Factories.Implementations;
using Infrastructure.Factories.Interfaces;
using Ninject.Modules;
using Services.Interfaces;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Composer
{
    public class NinjectComposer : NinjectModule
    {

        public override void Load()
        {         
            Bind<IInstructionFactory>().To<InstructionFactory>().InSingletonScope();
            Bind<IParseInputsService>().To<ParseInputsService>().InSingletonScope();
            Bind<IRobotMovementService>().To<RobotMovementService>().InSingletonScope();         
        }
    }
}
