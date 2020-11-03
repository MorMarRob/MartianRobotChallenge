using Composer;
using Model;
using Ninject;
using Services.Interfaces;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MartianRobotsConsole
{
    class Program
    {
        static IParseInputsService _parseInputsService;
        static RobotMovementService _processRobotMovementService;

        static void Main(string[] args)
        {
            
            InitializeServices();

            string line;

            while (true)
            {
                Console.WriteLine(Resources.ResourceFiles.ConsoleMessages.ReadyMessage);
                Console.WriteLine("\r");

                //read console input while it´s not an empty line
                while (!string.IsNullOrEmpty(line = Console.ReadLine()))
                {

                    if (DataContextClass.IsGridInputLine())
                    {
                        ReadGridInputLine(line);
                        continue;
                    }

                    if (DataContextClass.IsRobotPositionInputLine())
                    {
                        ReadRobotPositionInputLine(line);
                        continue;
                    }

                    if (DataContextClass.IsRobotInstructionsInputLine())
                    {
                        ReadRobotInstructionsInputLine(line);
                        continue;
                    }
                }

                //If input line is an empty line try to process output
                ProcessOutput();
            }

        }

        private static void InitializeServices()
        {
            IKernel kernel = new StandardKernel(new NinjectComposer());
            _parseInputsService = kernel.Get<ParseInputsService>();
            _processRobotMovementService = kernel.Get<RobotMovementService>();
        }

        private static void ReadGridInputLine(string line)
        {
            DataContextClass.MarsGrid = _parseInputsService.ParseGridLimits(line);
            if (DataContextClass.MarsGrid == null)
            {
                Console.WriteLine(_parseInputsService.ParseMessages[0]);
                Console.WriteLine("\r");
                Console.WriteLine(Resources.ResourceFiles.ConsoleMessages.RepeatGridInput);
            }
        }

        private static void ReadRobotPositionInputLine(string line)
        {
            DataContextClass.PositionInput = _parseInputsService.ParseRobotPosition(line, DataContextClass.MarsGrid);
            if (DataContextClass.PositionInput == null)
            {
                Console.WriteLine(_parseInputsService.ParseMessages[0]);
                Console.WriteLine("\r");
                Console.WriteLine(Resources.ResourceFiles.ConsoleMessages.RepeatRobotPositionInput);
            }
        }


        private static void ReadRobotInstructionsInputLine(string line)
        {
            DataContextClass.RobotInstructionsSet = _parseInputsService.ParseInstructionSet(line);
            if (DataContextClass.RobotInstructionsSet != null)
            {
                DataContextClass.InputRobots.Add(new Robot() { Id = DataContextClass.RobotId, LastValidPosition = DataContextClass.PositionInput, InstructionsSet = DataContextClass.RobotInstructionsSet });

                DataContextClass.RobotId++;
                DataContextClass.PositionInput = null;
            }
            else
            {
                Console.WriteLine(_parseInputsService.ParseMessages[0]);
                Console.WriteLine("\r");
                Console.WriteLine(Resources.ResourceFiles.ConsoleMessages.RepeatInstructionsSetInput);
            }
        }

        /// <summary>
        /// Try to generate outputs if there are existing inputs
        /// </summary>
        private static void ProcessOutput()
        {
            
            if (DataContextClass.InputRobots.Any())
            {
                foreach (Robot InputRobot in DataContextClass.InputRobots)
                {
                    _processRobotMovementService.MoveRobot(DataContextClass.MarsGrid, InputRobot);
                    DataContextClass.RobotOutputsSet.Add(new RobotOutput(InputRobot));
                    Console.WriteLine(DataContextClass.ComposeStringOutput(DataContextClass.RobotOutputsSet.Last()));
                }

            }
            else
            {
                Console.WriteLine(Resources.ResourceFiles.ConsoleMessages.NoInputsToProcess);               
            }

            DataContextClass.InitializeConditions();
            Console.WriteLine("\r");
        }

    }
}
