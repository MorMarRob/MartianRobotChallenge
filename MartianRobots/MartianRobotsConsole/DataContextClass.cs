using Model;
using Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MartianRobotsConsole
{
    static class DataContextClass
    {

        #region Properties

        public static Grid MarsGrid { get; set; }

        public static RobotPosition PositionInput { get; set; }

        public static List<Robot> InputRobots { get; set; }

        public static int RobotId { get; set; }

        public static List<IInstruction> RobotInstructionsSet { get; set; }
     
        public static List<RobotOutput> RobotOutputsSet { get; set; }

        #endregion


        #region Constructor

        static DataContextClass()
        {
            RobotId = 0;
            InputRobots = new List<Robot>();          
            RobotOutputsSet = new List<RobotOutput>();
        }

        #endregion


        #region Public Methods


        public static bool IsGridInputLine()
        {
            return MarsGrid == null;
        }


        public static bool IsRobotPositionInputLine()
        {
            return MarsGrid != null && PositionInput == null;
        }


        public static bool IsRobotInstructionsInputLine()
        {
            return PositionInput != null;
        }

        /// <summary>
        /// Create output line to be shown
        /// </summary>
        /// <param name="robotOutput"></param>
        /// <returns></returns>
        public static string ComposeStringOutput(RobotOutput robotOutput)
        {
            return robotOutput.FinalPosition.PositionCoordinates.X_Coordinate + " " +
                    robotOutput.FinalPosition.PositionCoordinates.Y_Coordinate + " " +
                    robotOutput.FinalPosition.RobotOrientation + " " +
                     robotOutput.RobotStatus;
        }


        public static void InitializeConditions()
        {
            MarsGrid = null;
            RobotId = 0;
            PositionInput = null;
            InputRobots.Clear();
        }


        #endregion

    }
}
