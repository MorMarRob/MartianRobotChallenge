using Infrastructure.Factories.Interfaces;
using Model;
using Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Factories.Implementations
{
    public class InstructionFactory : IInstructionFactory
    {


        public InstructionFactory()
        {

        }

        public IInstruction CreateInstruction(char instructionIdentificator)
        {
            switch (instructionIdentificator)
            {
                case 'L':
                    return new RotateLeftInstruction();                   

                case 'R':
                    return new RotateRightInstruction();

                case 'F':
                    return new GoForwardInstruction();

                default:
                    return null;

            }
        }
    }
}
