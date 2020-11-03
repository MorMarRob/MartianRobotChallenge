using Model.Enums;
using Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class RotateRightInstruction : IInstruction
    {

        #region Properties

        public char Identificator { get; private set; }

        #endregion


        #region Constructor

        public RotateRightInstruction()
        {
            Identificator = 'R';
        }

        #endregion


        #region Methods

        /// <summary>
        /// Performs a right rotation movement if it´s feasible
        /// </summary>
        /// <param name="robot"></param>
        /// <param name="grid"></param>
        /// <returns></returns>
        public bool TryPerformInstructionMovement(Robot robot, Grid grid)
        {
            switch (robot.LastValidPosition.RobotOrientation)
            {
                case Enums.OrientationTypes.N:
                    robot.LastValidPosition.RobotOrientation = Enums.OrientationTypes.E;
                    break;

                case Enums.OrientationTypes.S:
                    robot.LastValidPosition.RobotOrientation = Enums.OrientationTypes.W;
                    break;

                case Enums.OrientationTypes.E:
                    robot.LastValidPosition.RobotOrientation = Enums.OrientationTypes.S;
                    break;

                case Enums.OrientationTypes.W:
                    robot.LastValidPosition.RobotOrientation = Enums.OrientationTypes.N;
                    break;
            }

            return true;

        }

        #endregion

    }
}
