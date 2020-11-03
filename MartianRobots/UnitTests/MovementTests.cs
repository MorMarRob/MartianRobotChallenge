using Infrastructure.Factories.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Model.Enums;
using Model.Interfaces;
using Moq;
using Services.Interfaces;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests
{
    [TestClass]
    public class MovementTests
    {
        #region fields
        private IParseInputsService _parseInputsService;
        private IRobotMovementService _robotMovementService;
        private Mock<IInstructionFactory> _instructionFactory;

        #endregion


        #region TestMethods

        [TestInitialize]
        public void Initialization()
        {
            _instructionFactory = new Mock<IInstructionFactory>();
            _instructionFactory.Setup(f => f.CreateInstruction('L')).Returns(new RotateLeftInstruction());
            _instructionFactory.Setup(f => f.CreateInstruction('R')).Returns(new RotateRightInstruction());
            _instructionFactory.Setup(f => f.CreateInstruction('F')).Returns(new GoForwardInstruction());

            _robotMovementService = new RobotMovementService();

            _parseInputsService = new ParseInputsService(_instructionFactory.Object);

        }

        private Robot InicializateRobot(int posX, int posY, OrientationTypes orientation, string instructions)
        {
            Robot robot = new Robot()
            {
                Id = 0,
                LastValidPosition = new RobotPosition()
                {
                    PositionCoordinates = new GridPoint(posX, posY),
                    RobotOrientation = orientation
                },

                InstructionsSet = _parseInputsService.ParseInstructionSet(instructions),
            };

            return robot;          

        }

        [DataRow(0, 0, OrientationTypes.N, "L", 0, 0, OrientationTypes.W)]
        [DataRow(0, 0, OrientationTypes.S, "L", 0, 0, OrientationTypes.E)]
        [DataRow(0, 0, OrientationTypes.E, "L", 0, 0, OrientationTypes.N)]
        [DataRow(0, 0, OrientationTypes.W, "L", 0, 0, OrientationTypes.S)]
        [DataTestMethod]
        public void LeftInstructionMovementIsCorrect(int posX, int posY, OrientationTypes orientation, string instructions, int newPosX, int newPosY, OrientationTypes newOrientation)
        {
            Initialization();

            Robot robot = InicializateRobot(posX, posY, orientation, instructions);

            Grid grid = new Grid() { GridLimit = new GridPoint(20, 20) };

            _robotMovementService.MoveRobot(grid, robot);
            
            Assert.AreEqual(newPosX, robot.LastValidPosition.PositionCoordinates.X_Coordinate);
            Assert.AreEqual(newPosY, robot.LastValidPosition.PositionCoordinates.Y_Coordinate);
            Assert.AreEqual(newOrientation, robot.LastValidPosition.RobotOrientation);
        }

        [DataRow(0, 0, OrientationTypes.N, "R", 0, 0, OrientationTypes.E)]
        [DataRow(0, 0, OrientationTypes.S, "R", 0, 0, OrientationTypes.W)]
        [DataRow(0, 0, OrientationTypes.E, "R", 0, 0, OrientationTypes.S)]
        [DataRow(0, 0, OrientationTypes.W, "R", 0, 0, OrientationTypes.N)]
        [DataTestMethod]
        public void RightInstructionMovementIsCorrect(int posX, int posY, OrientationTypes orientation, string instructions, int newPosX, int newPosY, OrientationTypes newOrientation)
        {
            Initialization();

            Robot robot = InicializateRobot(posX, posY, orientation, instructions);

            Grid grid = new Grid() { GridLimit = new GridPoint(20, 20) };

            _robotMovementService.MoveRobot(grid, robot);

            Assert.AreEqual(newPosX, robot.LastValidPosition.PositionCoordinates.X_Coordinate);
            Assert.AreEqual(newPosY, robot.LastValidPosition.PositionCoordinates.Y_Coordinate);
            Assert.AreEqual(newOrientation, robot.LastValidPosition.RobotOrientation);
        }

        [DataRow(5, 5, OrientationTypes.N, "F", 5, 6, OrientationTypes.N)]
        [DataRow(5, 5, OrientationTypes.S, "F", 5, 4, OrientationTypes.S)]
        [DataRow(5, 5, OrientationTypes.E, "F", 6, 5, OrientationTypes.E)]
        [DataRow(5, 5, OrientationTypes.W, "F", 4, 5, OrientationTypes.W)]
        [DataTestMethod]
        public void ForwardInstructionMovementIsCorrect(int posX, int posY, OrientationTypes orientation, string instructions, int newPosX, int newPosY, OrientationTypes newOrientation)
        {
            Initialization();

            Robot robot = InicializateRobot(posX, posY, orientation, instructions);

            Grid grid = new Grid() { GridLimit = new GridPoint(20, 20) };

            _robotMovementService.MoveRobot(grid, robot);

            Assert.AreEqual(newPosX, robot.LastValidPosition.PositionCoordinates.X_Coordinate);
            Assert.AreEqual(newPosY, robot.LastValidPosition.PositionCoordinates.Y_Coordinate);
            Assert.AreEqual(newOrientation, robot.LastValidPosition.RobotOrientation);
        }

        [DataRow(20, 20, OrientationTypes.N, "F")]
        [DataTestMethod]
        public void RobotFallsIfOutOfGridAndNotScentPoint(int posX, int posY, OrientationTypes orientation, string instructions)
        {
            Initialization();

            Robot robot = InicializateRobot(posX, posY, orientation, instructions);

            Grid grid = new Grid() { GridLimit = new GridPoint(20, 20) };

            _robotMovementService.MoveRobot(grid, robot);

            Assert.AreEqual(posX, robot.LastValidPosition.PositionCoordinates.X_Coordinate);
            Assert.AreEqual(posY, robot.LastValidPosition.PositionCoordinates.Y_Coordinate);
            Assert.AreEqual(true, robot.IsLost);
            Assert.AreEqual(1, grid.ScentPoints.Count);
            Assert.AreEqual(posX, grid.ScentPoints[0].X_Coordinate);
            Assert.AreEqual(posY, grid.ScentPoints[0].X_Coordinate);

        }

        [DataRow(20, 20, OrientationTypes.N, "F")]
        [DataTestMethod]
        public void RobotNotFallIfOutOfGridAndScentPoint(int posX, int posY, OrientationTypes orientation, string instructions)
        {
            Initialization();

            Robot robot = InicializateRobot(posX, posY, orientation, instructions);

            Grid grid = new Grid() 
            {
                GridLimit = new GridPoint(20, 20),
                ScentPoints = new List<GridPoint>() { new GridPoint(20, 20) }
            };

            _robotMovementService.MoveRobot(grid, robot);

            Assert.AreEqual(posX, robot.LastValidPosition.PositionCoordinates.X_Coordinate);
            Assert.AreEqual(posY, robot.LastValidPosition.PositionCoordinates.Y_Coordinate);
            Assert.AreEqual(false, robot.IsLost);
            Assert.AreEqual(1, grid.ScentPoints.Count);
            Assert.AreEqual(posX, grid.ScentPoints[0].X_Coordinate);
            Assert.AreEqual(posY, grid.ScentPoints[0].X_Coordinate);

        }
        [DataRow(1, 1, OrientationTypes.N, "FLF", 0, 2, OrientationTypes.W, false)]
        [DataRow(1, 1, OrientationTypes.N, "FLFF", 0, 2, OrientationTypes.W, true)]
        [DataRow(1, 1, OrientationTypes.N, "LFFFRF", 0, 2, OrientationTypes.N, false)]
        [DataRow(1, 1, OrientationTypes.N, "FRFFRFLFLFFLFRF", 3, 4, OrientationTypes.N, false)]
        [DataRow(1, 1, OrientationTypes.N, "FRFFRFLFLFFLFRF", 3, 4, OrientationTypes.N, false)]
        [DataRow(1, 1, OrientationTypes.N, "LFFLFLFFRFRFRFFLFFRFFR", 0, 2, OrientationTypes.W, true)]
        [DataTestMethod]
        public void MovementIsCorrect(int posX, int posY, OrientationTypes orientation, string instructions, int newPosX, int newPosY, OrientationTypes newOrientation, bool isLostRobot)
        {
            Initialization();

            Robot robot = InicializateRobot(posX, posY, orientation, instructions);

            Grid grid = new Grid()
            {
                GridLimit = new GridPoint(5, 5),
                ScentPoints = new List<GridPoint>() { new GridPoint(2, 0), new GridPoint(0, 1) }
            };

            _robotMovementService.MoveRobot(grid, robot);

            Assert.AreEqual(newPosX, robot.LastValidPosition.PositionCoordinates.X_Coordinate);
            Assert.AreEqual(newPosY, robot.LastValidPosition.PositionCoordinates.Y_Coordinate);
            Assert.AreEqual(newOrientation, robot.LastValidPosition.RobotOrientation);
            Assert.AreEqual(isLostRobot, robot.IsLost);
        }

        #endregion

    }
}
