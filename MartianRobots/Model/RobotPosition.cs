using Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class RobotPosition
    {

        #region Properties

        public GridPoint PositionCoordinates { get; set; }

        
        public OrientationTypes RobotOrientation { get; set; }

        #endregion

        #region Constructor
        public RobotPosition()
        {

        }


        #endregion

        #region Methods

        public RobotPosition Clone()
        {
            RobotPosition robotPosition = (RobotPosition)this.MemberwiseClone();
            robotPosition.PositionCoordinates = robotPosition.PositionCoordinates.Clone();
            return robotPosition;            
        }

        #endregion

    }
}
