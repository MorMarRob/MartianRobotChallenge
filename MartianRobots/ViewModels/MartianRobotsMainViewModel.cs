using Model;
using Model.Enums;
using Model.Interfaces;
using Prism.Commands;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ViewModels.Auxiliary;

namespace ViewModels
{
    public class MartianRobotsMainViewModel : ObservableObject
    {

        #region fields
    
        private readonly IRobotMovementService _processRobotMovement;
        private readonly IParseInputsService _parseInputsService;    
        
        private Grid marsGrid;
        private RobotPosition positionInput;
        private string instructionsSet;
        private int robotId;

        private ObservableCollection<RobotOutput> robotsProcessedOutputs;
        private ObservableCollection<string> logMessages;
        private ObservableCollection<Robot> inputRobots;

        private bool validGrid;
        private bool robotPositionInputEnabled;
        private bool robotGeneralInputEnabled;
        private bool instructionsSequenceEnabled;
        private bool logMessagesVisibility;

        #endregion



        #region Properties

        public RawInput PlainInput { get; set; }


        public RobotPosition PositionInput
        {
            get { return positionInput; }
            set
            {
                positionInput = value;
                OnPropertyChanged(nameof(PositionInput));
                OnPropertyChanged(nameof(InstructionsSequenceEnabled));
                OnPropertyChanged(nameof(LogMessagesVisibility));
                OnPropertyChanged(nameof(RobotPositionInputEnabled));
            }
        }


        public Grid MarsGrid
        {
            get { return marsGrid; }
            set
            {
                marsGrid = value;
                OnPropertyChanged(nameof(MarsGrid));
                OnPropertyChanged(nameof(ValidGrid));
                OnPropertyChanged(nameof(LogMessagesVisibility));
                OnPropertyChanged(nameof(RobotPositionInputEnabled));
                OnPropertyChanged(nameof(RobotGeneralInputEnabled));
                ((DelegateCommand)RestartCommand).RaiseCanExecuteChanged();
            }
        }


        public string InstructionsSet
        {
            get { return instructionsSet; }
            set
            {
                instructionsSet = value;
                OnPropertyChanged(nameof(InstructionsSet));
                ((DelegateCommand)AcceptInstructionsInputCommand).RaiseCanExecuteChanged();
            }
        }

  
        public int RobotId
        {
            get { return robotId; }
            set 
            { 
                robotId = value;
                OnPropertyChanged(nameof(RobotId));
            
            }
        }


        public OrientationTypes OrientationType { get; set; }


        public ObservableCollection<Robot> InputRobots
        {
            get { return inputRobots; }
            set
            {
                inputRobots = value;
                OnPropertyChanged(nameof(InputRobots));
                ((DelegateCommand)ProcessOutputCommand).RaiseCanExecuteChanged();
            }
        }


        public ObservableCollection<string> LogMessages
        {
            get { return logMessages; }
            set
            {
                logMessages = value;
                OnPropertyChanged(nameof(LogMessages));
                OnPropertyChanged(nameof(LogMessagesVisibility));
            }
        }


        public ObservableCollection<RobotOutput> RobotsProcessedOutputs
        {
            get
            {
                return robotsProcessedOutputs;
            }
            set
            {
                robotsProcessedOutputs = value;
                OnPropertyChanged(nameof(RobotsProcessedOutputs));
                OnPropertyChanged(nameof(RobotGeneralInputEnabled));
            }
        }


        public bool RobotGeneralInputEnabled
        {
            get
            {
                return ValidGrid && !RobotsProcessedOutputs.Any();
            }
            set
            {
                robotGeneralInputEnabled = value;
                OnPropertyChanged(nameof(RobotGeneralInputEnabled));

            }
        }


        public bool RobotPositionInputEnabled
        {
            get 
            {
                return PositionInput == null && MarsGrid != null;
            }
            set 
            { 
                robotPositionInputEnabled = value;
                OnPropertyChanged(nameof(RobotPositionInputEnabled));
                
            }
        }
       
        
        public bool InstructionsSequenceEnabled
        {
            get 
            {
                return PositionInput != null; 
            }
            set
            { 
                instructionsSequenceEnabled = value;
                OnPropertyChanged(nameof(InstructionsSequenceEnabled));
            }
        }


        public bool ValidGrid
        {
            get 
            { 
                return MarsGrid != null; 
            }
            set 
            { 
                validGrid = value;
                OnPropertyChanged(nameof(ValidGrid));
                OnPropertyChanged(nameof(RobotPositionInputEnabled));
                OnPropertyChanged(nameof(RobotGeneralInputEnabled));
            }
        }

       
        public bool LogMessagesVisibility
        {
            get { return LogMessages.Any(); }
            set 
            { 
                logMessagesVisibility = value;
                OnPropertyChanged(nameof(LogMessagesVisibility));
            }
        }
   
        #endregion


        #region Constructor

        public MartianRobotsMainViewModel(IRobotMovementService processRobotMovement, IParseInputsService parseInputsService)
        {    
            _processRobotMovement = processRobotMovement;
            _parseInputsService = parseInputsService;

            InitializeCommands();

            PlainInput = new RawInput();
            InputRobots = new ObservableCollection<Robot>();           
            RobotsProcessedOutputs = new ObservableCollection<RobotOutput>();

            RobotId = 0;
            OrientationType = OrientationTypes.N;              

            LogMessages = _parseInputsService.ParseMessages;           
        }

        #endregion


        #region Commands

        public ICommand LeftInstructionCommand { get; set; }

        public ICommand RightInstructionCommand { get; set; }

        public ICommand ForwardInstructionCommand { get; set; }

        public ICommand AcceptInstructionsInputCommand { get; set; }

        public ICommand AcceptPositionInputCommand { get; set; }

        public ICommand CreateGridCommand { get; set; }

        public ICommand ProcessOutputCommand { get; set; }

        public ICommand RestartCommand { get; set; }

        #endregion


        #region private Methods


        private void InitializeCommands()
        {
            CreateGridCommand = new DelegateCommand(CreateMarsGrid);
            LeftInstructionCommand = new DelegateCommand(CreateLeftInstruction);
            RightInstructionCommand = new DelegateCommand(CreateRightInstruction);
            ForwardInstructionCommand = new DelegateCommand(CreateForwardInstruction);
            AcceptInstructionsInputCommand = new DelegateCommand(GenerateRobotInput);
            ProcessOutputCommand = new DelegateCommand(ProcessOutput, ProcessOutputCanExecuteCommand );
            AcceptPositionInputCommand = new DelegateCommand(GeneratePositionInput);
            RestartCommand = new DelegateCommand(DoRestart, RestartCanExecuteCommand);
        }

       
        private void InitializeConditions()
        {
            MarsGrid = null;     
            PlainInput.Grid_XCoordinate = string.Empty;
            PlainInput.Grid_YCoordinate = string.Empty;

            PlainInput.RobotInstructionList = string.Empty;
            PlainInput.RobotPosition_Orientation = string.Empty;
            PlainInput.RobotPosition_XCoordinate = string.Empty;
            PlainInput.RobotPosition_YCoordinate = string.Empty;
            
            RobotId = 0;

            PositionInput = null;
            InputRobots.Clear();
            RobotsProcessedOutputs.Clear();
            LogMessages.Clear();           
        }

              
        private void CreateMarsGrid()
        {
            string inputMarsGrid = PlainInput.Grid_XCoordinate + " " + PlainInput.Grid_YCoordinate;
            MarsGrid = _parseInputsService.ParseGridLimits(inputMarsGrid);                 
        }
     

        private void GeneratePositionInput()
        {
            string positionRobotInput = PlainInput.RobotPosition_XCoordinate + " " + PlainInput.RobotPosition_YCoordinate + " " + PlainInput.RobotPosition_Orientation;
            PositionInput = _parseInputsService.ParseRobotPosition(positionRobotInput, MarsGrid);
            ((DelegateCommand)ProcessOutputCommand).RaiseCanExecuteChanged();
        }


        private void GenerateRobotInput()
        {
            List<IInstruction> robotInstructionsList = _parseInputsService.ParseInstructionSet(PlainInput.RobotInstructionList);
            
            if (robotInstructionsList != null && robotInstructionsList.Any())
            {
                InputRobots.Add(new Robot() { Id = RobotId, LastValidPosition = PositionInput, InstructionsSet = robotInstructionsList });
                ((DelegateCommand)ProcessOutputCommand).RaiseCanExecuteChanged();
                RobotId++;
                PlainInput.RobotInstructionList = string.Empty;
                PlainInput.RobotPosition_Orientation = string.Empty;
                PlainInput.RobotPosition_XCoordinate = string.Empty;
                PlainInput.RobotPosition_YCoordinate = string.Empty;
                PositionInput = null;
            }

            else
                OnPropertyChanged(nameof(LogMessagesVisibility));
        }

      
        private void CreateLeftInstruction()
        {
            PlainInput.RobotInstructionList += "L";
        }


        private void CreateRightInstruction()
        {
            PlainInput.RobotInstructionList += "R";
        }


        private void CreateForwardInstruction()
        {
            PlainInput.RobotInstructionList += "F";
        }


        private void ProcessOutput()
        {
            if (InputRobots != null && InputRobots.Any())
            {
                foreach (Robot inputRobot in InputRobots)
                {                   
                    _processRobotMovement.MoveRobot(MarsGrid, inputRobot);
                    RobotsProcessedOutputs.Add(new RobotOutput(inputRobot));
                }

                OnPropertyChanged(nameof(RobotGeneralInputEnabled));
                ((DelegateCommand)ProcessOutputCommand).RaiseCanExecuteChanged();
            }           
        }


        private bool ProcessOutputCanExecuteCommand()
        {
            return InputRobots != null && InputRobots.Any() && RobotPositionInputEnabled && !RobotsProcessedOutputs.Any();
        }


        private void DoRestart()
        {
            InitializeConditions();
            ((DelegateCommand)ProcessOutputCommand).RaiseCanExecuteChanged();
        }

        private bool RestartCanExecuteCommand()
        {
            return MarsGrid != null;
        }

        #endregion

    }
}
