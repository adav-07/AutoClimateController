using AutoClimateControllerConsoleApp;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewAutoClimateControllerLib.Tests
{
    public static class TestHelpers
    {
        public static Mock<ISensor> CreateTempSensorMock(int returnValue)
        {
            var tempMock = new Mock<ISensor>();
            tempMock.Setup(ts => ts.GetValue()).Returns(returnValue);
            return tempMock;
        }

        public static Mock<ISensor> CreateOccupancySensorMock(int returnValue)
        {
            var occupancyMock = new Mock<ISensor>();
            occupancyMock.Setup(os => os.GetValue()).Returns(returnValue);
            return occupancyMock;
        }

        public static Mock<ITempCalculator> CreateTempCalculatorMock(int numPeople, int outsideTemp, double expectedTemp)
        {
            var calculatorMock = new Mock<ITempCalculator>();
            calculatorMock.Setup(calc => calc.CalculateNewTemperature(numPeople, outsideTemp)).Returns(expectedTemp);
            return calculatorMock;
        }

        public static Mock<IRegulator> CreateRegulatorMock()
        {
            return new Mock<IRegulator>();
        }
    }

}
