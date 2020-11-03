using Model.Enums;
using Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class RotateLeftInstruction : IInstruction
    {

        #region Properties

        public char Identificator { get ; private set; }

        #endregion


        #region Constructor

        public RotateLeftInstruction()
        {          
            Identificator = 'L';
        }

        #endregion


        #region Methods

        /// <summary>
        /// Performs a left rotation movement if it´s feasible
        /// </summary>
        /// <param name="robot"></param>
        /// <param name="grid"></param>
        /// <returns></returns>
        public bool TryPerformInstructionMovement(Robot robot, Grid grid)
        {
            switch (robot.LastValidPosition.RobotOrientation)
            {
                case OrientationTypes.N:
                    robot.LastValidPosition.RobotOrientation = OrientationTypes.W;
                    break;

                case OrientationTypes.S:
                    robot.LastValidPosition.RobotOrientation = OrientationTypes.E;
                    break;

                case OrientationTypes.E:
                    robot.LastValidPosition.RobotOrientation = OrientationTypes.N;
                    break;

                case OrientationTypes.W:
                    robot.LastValidPosition.RobotOrientation = OrientationTypes.S;
                    break;
            }

            return true;

        }

        #endregion
    }
}
