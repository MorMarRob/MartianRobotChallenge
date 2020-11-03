using Model;
using Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IParseInputsService
    {
        #region Properties

        ObservableCollection<string> ParseMessages { get; set; }

        #endregion


        #region Methods

        /// <summary>
        /// Gets Grid from input
        /// </summary>
        /// <param name="gridImput"></param>
        /// <returns></returns>
        Grid ParseGridLimits(string gridImput);

        /// <summary>
        /// Gets robot position from input
        /// </summary>
        /// <param name="robotPositionInput"></param>
        /// <returns></returns>
        RobotPosition ParseRobotPosition(string robotPositionInput, Grid grid);

        /// <summary>
        /// Gets a list of movement instructions from input
        /// </summary>
        /// <param name="instructions"></param>
        /// <returns></returns>
        List<IInstruction> ParseInstructionSet(string instructions);

        #endregion

    }
}
