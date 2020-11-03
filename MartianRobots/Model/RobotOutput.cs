using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class RobotOutput
    {

        #region Properties

        public int RobotId { get; set; }


        public RobotPosition FinalPosition { get; set; }


        public string RobotStatus { get; private set; }

        #endregion

        #region Constructor

        public RobotOutput(Robot robot)
        {
            RobotId = robot.Id;
            FinalPosition = robot.LastValidPosition;
            RobotStatus = robot.IsLost ? "LOST" : string.Empty;
        }

        #endregion
    }
}
