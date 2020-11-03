using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Auxiliary
{
    public class RawInput : ObservableObject
    {

        #region fields

        private string robotInstructionList;
        private string robotPosition_Orientation;
        private string robotPosition_YCoordinate;
        private string robotPosition_XCoordinate;
        private string grid_XCoordinate;
        private string grid_YCoordinate;

        #endregion

        #region Properties

        public string Grid_XCoordinate
        {
            get { return grid_XCoordinate; }
            set 
            { 
                grid_XCoordinate = value;
                OnPropertyChanged(nameof(Grid_XCoordinate));
            }
        }

        public string Grid_YCoordinate
        {
            get { return grid_YCoordinate; }
            set
            {
                grid_YCoordinate = value;
                OnPropertyChanged(nameof(Grid_YCoordinate));
            }
        }


        public string RobotPosition_XCoordinate
        {
            get { return robotPosition_XCoordinate; }
            set
            {
                robotPosition_XCoordinate = value;
                OnPropertyChanged(nameof(RobotPosition_XCoordinate));
            }
        }


        public string RobotPosition_YCoordinate
        {
            get { return robotPosition_YCoordinate; }
            set
            {
                robotPosition_YCoordinate = value;
                OnPropertyChanged(nameof(RobotPosition_YCoordinate));
            }
        }


        public string RobotPosition_Orientation
        {
            get { return robotPosition_Orientation; }
            set
            {
                robotPosition_Orientation = value;
                OnPropertyChanged(nameof(RobotPosition_Orientation));
            }
        }


        public string RobotInstructionList
        {
            get { return robotInstructionList; }
            set 
            { 
                robotInstructionList = value;
                OnPropertyChanged(nameof(RobotInstructionList));

            }
        }

        #endregion
    }
}
