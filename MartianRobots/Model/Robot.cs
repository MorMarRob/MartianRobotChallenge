using Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Robot
    {
        #region Properties

        public int Id { get; set; }

        public RobotPosition LastValidPosition { get; set; }

        public List<IInstruction> InstructionsSet { get; set; }

        public bool IsLost { get; set; }

        #endregion


        #region Constructor

        public Robot()
        {
            LastValidPosition = new RobotPosition();
            InstructionsSet = new List<IInstruction>();
        }

        #endregion

    }
}
