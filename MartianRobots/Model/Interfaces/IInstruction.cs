using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Interfaces
{
    public interface IInstruction
    {
        #region Properties

        char Identificator { get; }

        #endregion


        #region Methods

        /// <summary>
        /// Performs the movement asociated to the instruction if it´s feasible
        /// </summary>
        /// <param name="robot"></param>
        /// <param name="grid"></param>
        /// <returns></returns>
        bool TryPerformInstructionMovement(Robot robot, Grid grid);

        #endregion

    }
}
