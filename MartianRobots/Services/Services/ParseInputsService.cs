using EnumsNET;
using Infrastructure.Factories.Interfaces;
using Model;
using Model.Enums;
using Model.Interfaces;
using Resources;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class ParseInputsService : IParseInputsService
    {

        #region fields

        private readonly IInstructionFactory _instructionFactory;
        private const int MAX_COORDINATE_VALUE = 50;

        #endregion


        #region Properties

        public ObservableCollection<string> ParseMessages { get; set; }

        #endregion


        #region Constructor

        public ParseInputsService(IInstructionFactory instructionFactory)
        {
            _instructionFactory = instructionFactory;
            ParseMessages = new ObservableCollection<string>();
        }



        #endregion


        #region IInstruction implementation methods

        /// <summary>
        /// Gets Grid from input
        /// </summary>
        /// <param name="gridImput"></param>
        /// <returns></returns>
        public Grid ParseGridLimits(string gridInput)
        {
            Grid ParsedGrid = null;
            ParseMessages.Clear();
            
            if(gridInput != null)
            {
                var gridImputElements = gridInput.Split(' ');

                if(gridImputElements.Count() == 2)
                {
                    int XCoordinate = 0;
                    int YCoordinate = 0;
                    
                    if(int.TryParse(gridImputElements[0], out XCoordinate) && int.TryParse(gridImputElements[1], out YCoordinate))
                    {
                        if (XCoordinate > 0 && XCoordinate <= MAX_COORDINATE_VALUE &&
                            YCoordinate > 0 && YCoordinate <= MAX_COORDINATE_VALUE)
                        {
                            ParsedGrid = new Grid();
                            ParsedGrid.GridLimit = new GridPoint(XCoordinate, YCoordinate);                           
                        }
                        else
                            ParseMessages.Add(Resources.ResourceFiles.ParseErrorMessages.GridCoordinatesShoudBeBetween0_50);

                    }
                    else
                        ParseMessages.Add(Resources.ResourceFiles.ParseErrorMessages.GridCoordinatesBadFormat);
                }
                else
                    ParseMessages.Add(Resources.ResourceFiles.ParseErrorMessages.GridCoordinatesWrongElementsNumber);
            }

            return ParsedGrid;
            
        }


        /// <summary>
        /// Gets robot position from input
        /// </summary>
        /// <param name="robotPositionInput"></param>
        /// <returns></returns>
        public RobotPosition ParseRobotPosition(string robotPositionInput, Grid grid)
        {
            RobotPosition ParsedRobotPosition = null;
            ParseMessages.Clear();

            if (robotPositionInput != null)
            {
                var robotPositionImputElements = robotPositionInput.Split(' ');

                if (robotPositionImputElements.Count() == 3 && !robotPositionImputElements.Any(e =>(string.IsNullOrEmpty(e))))
                {
                    int XCoordinate = 0;
                    int YCoordinate = 0;
                    string Orientation = string.Empty;

                    if (int.TryParse(robotPositionImputElements[0], out XCoordinate) && int.TryParse(robotPositionImputElements[1], out YCoordinate))
                    {
                        if (XCoordinate >= 0 && XCoordinate <= MAX_COORDINATE_VALUE &&
                            YCoordinate >= 0 && YCoordinate <= MAX_COORDINATE_VALUE)
                        {
                            if(XCoordinate <= grid.GridLimit.X_Coordinate &&
                             YCoordinate <= grid.GridLimit.Y_Coordinate)
                            {
                                ParsedRobotPosition = new RobotPosition();
                                ParsedRobotPosition.PositionCoordinates = new GridPoint(XCoordinate, YCoordinate);
                            }
                            else
                            {
                                ParseMessages.Add(Resources.ResourceFiles.ParseErrorMessages.RobotPositionInputOutOfGrid);
                                return null;
                            }                                                  
                        }
                        else
                        {
                            ParseMessages.Add(Resources.ResourceFiles.ParseErrorMessages.RobotPositionInputShoudBeBetween0_50);
                            return null;
                        }                            
                    }
                    else
                    {
                        ParseMessages.Add(Resources.ResourceFiles.ParseErrorMessages.RobotPositionCoordinatesBadFormat);
                        return null;
                    }
                        

                    if(robotPositionImputElements[2].Length == 1)                  
                    {
                        OrientationTypes orientation;
                        if (Enum.TryParse<OrientationTypes>(robotPositionImputElements[2], out orientation))
                        {
                            ParsedRobotPosition.RobotOrientation = orientation;
                            return ParsedRobotPosition;
                        }
                        else
                        {
                            ParseMessages.Add(Resources.ResourceFiles.ParseErrorMessages.RobotPositionOrientationInputWrongFormat);
                            return null;
                        }                       
                    }
                }
                else
                {
                    ParseMessages.Add(Resources.ResourceFiles.ParseErrorMessages.RobotPositionInputWrongElementsNumber);
                    return null;
                }
                    
            }

            return ParsedRobotPosition;
        }


        /// <summary>
        /// Gets a list of movement instructions from input
        /// </summary>
        /// <param name="instructions"></param>
        /// <returns></returns>
        public List<IInstruction> ParseInstructionSet(string instructions)
        {
            List<IInstruction> instructionsList = null;
            IInstruction instructionCreated = null;
            ParseMessages.Clear();

            if (string.IsNullOrEmpty(instructions))
            {
                ParseMessages.Add(Resources.ResourceFiles.ParseErrorMessages.InstructionsNumberIsNull);
                return null;
            }

            else if (instructions.Length > 100)
            {
                ParseMessages.Add(Resources.ResourceFiles.ParseErrorMessages.InstructionsNumberGreaterThanMaximum);
                return null;
            }

            else
            {
                instructionsList = new List<IInstruction>();

                for (int i = 0; i < instructions.Length; i++)
                {
                    instructionCreated = _instructionFactory.CreateInstruction(instructions[i]);
                    if (instructionCreated != null)
                        instructionsList.Add(instructionCreated);
                    else
                    {
                        ParseMessages.Add(Resources.ResourceFiles.ParseErrorMessages.InstructionsSetWithInvalidInstructionInput);
                        return null;
                    }
                }
            }

            return instructionsList;
        }

        #endregion
    
    }
}
