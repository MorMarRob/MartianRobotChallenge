using Model;
using Model.Interfaces;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class RobotMovementService : IRobotMovementService
    {

        #region IRobotMovementService implementation Methods


        public void MoveRobot(Grid grid, Robot robot)
        {
            foreach (IInstruction instruction in robot.InstructionsSet)
            {              
                if(!instruction.TryPerformInstructionMovement(robot, grid))               
                {
                    if (!grid.ScentPoints.Any(p => p.X_Coordinate == robot.LastValidPosition.PositionCoordinates.X_Coordinate &&
                                                p.Y_Coordinate == robot.LastValidPosition.PositionCoordinates.Y_Coordinate))
                    {
                        grid.ScentPoints.Add(robot.LastValidPosition.PositionCoordinates);
                        robot.IsLost = true;
                        break;
                    }
                }
            }
        }

        #endregion

    }
}
