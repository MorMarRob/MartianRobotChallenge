using Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class GoForwardInstruction : IInstruction
    {
        #region Properties

        public char Identificator { get; private set; }

        #endregion

        #region Constructor

        public GoForwardInstruction()
        {           
            Identificator = 'F';
        }

        #endregion


        #region Methods

        /// <summary>
        /// Performs a forward movement if it´s feasible
        /// </summary>
        /// <param name="robot"></param>
        /// <param name="grid"></param>
        /// <returns></returns>
        public bool TryPerformInstructionMovement(Robot robot, Grid grid)
        {
            bool feasibleInstructionMovement = false;
            
            RobotPosition estimatedPosition = EstimateInstructionMovement(robot.LastValidPosition);

            if (CheckValidPosition(estimatedPosition, grid))
            {
                robot.LastValidPosition = estimatedPosition;
                feasibleInstructionMovement = true;
            }

            return feasibleInstructionMovement;

        }

        private RobotPosition EstimateInstructionMovement(RobotPosition currentPosition)
        {
            RobotPosition estimatedPosition  = currentPosition.Clone();

            switch (currentPosition.RobotOrientation)
            {
                case Enums.OrientationTypes.N:
                    estimatedPosition.PositionCoordinates.Y_Coordinate++;
                    break;

                case Enums.OrientationTypes.S:
                    estimatedPosition.PositionCoordinates.Y_Coordinate--;
                    break;

                case Enums.OrientationTypes.E:
                    estimatedPosition.PositionCoordinates.X_Coordinate++;
                    break;

                case Enums.OrientationTypes.W:
                    estimatedPosition.PositionCoordinates.X_Coordinate--;
                    break;
            }

            return estimatedPosition;
        }

        /// <summary>
        /// Checks if movement resulting position is inside of grid limits 
        /// </summary>
        /// <param name="estimatedPosition"></param>
        /// <param name="grid"></param>
        /// <returns></returns>
        private bool CheckValidPosition(RobotPosition estimatedPosition , Grid grid)
        {
            bool validMovement = false;
            
            if(estimatedPosition.PositionCoordinates.X_Coordinate <= grid.GridLimit.X_Coordinate && estimatedPosition.PositionCoordinates.X_Coordinate >= grid.GridOrigen.X_Coordinate &&
               estimatedPosition.PositionCoordinates.Y_Coordinate <= grid.GridLimit.Y_Coordinate && estimatedPosition.PositionCoordinates.Y_Coordinate >= grid.GridOrigen.Y_Coordinate)
            {
                validMovement = true;
            }

            return validMovement;
        }

        #endregion

    }
}
