using Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Factories.Interfaces
{
    public interface IInstructionFactory
    {
        IInstruction CreateInstruction(char instructionIdentificator);

    }
}
