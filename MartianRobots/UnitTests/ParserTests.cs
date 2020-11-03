using Infrastructure.Factories.Implementations;
using Infrastructure.Factories.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Model.Enums;
using Model.Interfaces;
using Moq;
using Resources.Converters;
using Services.Interfaces;
using Services.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace UnitTests
{
    [TestClass]
    public class ParserTests
    {

        #region fields

        Mock<IInstructionFactory> _instructionFactory;
        IParseInputsService _parseInputsService;

        #endregion


        #region TestMethods


        [TestInitialize]
        public void Initialization()
        {    
            _instructionFactory = new Mock<IInstructionFactory>();
            _parseInputsService = new ParseInputsService(_instructionFactory.Object);

            _instructionFactory.Setup(f => f.CreateInstruction('L')).Returns(new RotateLeftInstruction());
            _instructionFactory.Setup(f => f.CreateInstruction('R')).Returns(new RotateRightInstruction());
            _instructionFactory.Setup(f => f.CreateInstruction('F')).Returns(new GoForwardInstruction());


        }

        [DataRow("1 10", 1, 10)]
        [DataRow("1 2", 1, 2)]
        [DataRow("5 5", 5, 5)]
        [DataRow("50 50", 50, 50)]
        [DataTestMethod]
        public void GridInputCorrectlyParsed(string gridInput, int XCoordinateParsed, int YCoordinateParsed)
        {
            Initialization();
            Grid grid = _parseInputsService.ParseGridLimits(gridInput);

            Assert.AreEqual(XCoordinateParsed, grid.GridLimit.X_Coordinate);
            Assert.AreEqual(YCoordinateParsed, grid.GridLimit.Y_Coordinate);
            Assert.AreEqual(false, _parseInputsService.ParseMessages.Any());
        }

        [DataRow("0 0")]
        [DataRow("10")]
        [DataRow("51")]
        [DataRow("b")]
        [DataRow("")]
        [DataRow(" ")]
        [DataTestMethod]
        public void GridInputIncorrectFormatInput(string gridInput)
        {
            Grid grid = _parseInputsService.ParseGridLimits(gridInput);

            Assert.IsNull(grid);
            Assert.AreEqual(1, _parseInputsService.ParseMessages.Count);
        }

        [DataRow("1 10 E", 1, 10, OrientationTypes.E)]
        [DataRow("1 2 N", 1, 2, OrientationTypes.N)]
        [DataRow("5 5 W", 5, 5, OrientationTypes.W)]
        [DataRow("50 50 S", 50, 50, OrientationTypes.S)]
        [TestMethod]
        public void RobotPositionInputCorrectlyParsed(string robotPositionInput, int XCoordinateParsed, int YCoordinateParsed, OrientationTypes orientationParsed)
        {
            Grid grid = new Grid() { GridLimit = new GridPoint(50, 50) };
            RobotPosition robotPosition = _parseInputsService.ParseRobotPosition(robotPositionInput, grid);

            Assert.AreEqual(XCoordinateParsed, robotPosition.PositionCoordinates.X_Coordinate);
            Assert.AreEqual(YCoordinateParsed, robotPosition.PositionCoordinates.Y_Coordinate);
            Assert.AreEqual(orientationParsed, robotPosition.RobotOrientation);
        }


        [DataRow("1 10")]
        [DataRow("1 N")]
        [DataRow("W")]
        [DataRow("500 50 S")]
        [DataRow("5 50 4 S ")]
        [DataRow("1W")]
        [DataRow("10 10 ")]
        [DataRow("10   W ")]
        [DataRow("  10  N")]
        [DataRow("10 b N ")]        
        [DataRow("5 6 t")]
        [DataRow("31 20 N")]
        [DataRow("20 31 N")]
        [DataRow("31 31 N")]
        [DataRow(" ")]
        [DataRow("")]
        [TestMethod]
        public void RobotPositionInputIncorrectFormat(string robotPositionInput)
        {
            Grid grid = new Grid() { GridLimit = new GridPoint(30, 30) };
            RobotPosition robotPosition = _parseInputsService.ParseRobotPosition(robotPositionInput, grid);

            Assert.IsNull(robotPosition);
            Assert.AreEqual(1, _parseInputsService.ParseMessages.Count);
        }




        [DataRow("L", 1)]
        [DataRow("R", 1)]
        [DataRow("F", 1)]
        [DataRow("LRF", 3)]
        [DataRow("LRLFLRFFFLLLRFR", 15)]
        [TestMethod]
        public void InstructionSetInputNumberCorrectlyParsed(string instructions, int instructionsNumber)
        {
            List<IInstruction> instructionsSet = _parseInputsService.ParseInstructionSet(instructions);

            Assert.AreEqual(instructionsNumber, instructionsSet.Count);

        }


        [TestMethod]
        public void InstructionSetInputCorrectlyParsed()
        {
            string instructionsSetInput = "LRFFRL";
            List<IInstruction> expectedInstructionsSet = new List<IInstruction>() { new RotateLeftInstruction(), new RotateRightInstruction(), new GoForwardInstruction(),
                                                                                    new GoForwardInstruction(), new RotateRightInstruction(), new RotateLeftInstruction()};

            List<IInstruction> instructionsSet = _parseInputsService.ParseInstructionSet(instructionsSetInput);

            Assert.AreEqual(expectedInstructionsSet.Count, instructionsSet.Count);

            for (int i = 0; i < instructionsSet.Count; i++)
            {
                Assert.AreEqual(expectedInstructionsSet[i].Identificator, instructionsSet[i].Identificator);
            }

        }

        [DataRow("L ")]
        [DataRow("R L")]
        [DataRow(" L")]
        [DataRow("RL4")]
        [DataRow("RFLT")]
        [DataRow("BBL")]
        [DataRow("RFRf")]
        [DataRow("RLRLRLLLLFFFFFFFFFFFLLLLLLLLLLLLLLLLLRRRRRRRRRRRRRLLLLLLLLLLFFFFFFFFFFLLLLLLLLLLRRRRRRRRRRFFFFFFFFFFLLLRRR")]
        [DataRow(" ")]
        [DataRow("")]
        [TestMethod]
        public void InstructionSetInputIncorrectFormat(string instructions)
        {           
            List<IInstruction> instructionsSet = _parseInputsService.ParseInstructionSet(instructions);

            Assert.IsNull(instructionsSet);
            Assert.AreEqual(1, _parseInputsService.ParseMessages.Count);
        }


        #endregion


    }
}