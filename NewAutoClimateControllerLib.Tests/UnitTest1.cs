using Xunit;
using Moq;
using System;
using AutoClimateControllerConsoleApp;

public class AutoClimateControllerTests
{
    
    private Mock<ISensor> CreateTempSensorMock(int returnValue)
    {
        var tempMock = new Mock<ISensor>();
        tempMock.Setup(ts => ts.GetValue()).Returns(returnValue);
        return tempMock;
    }

    private Mock<ISensor> CreateOccupancySensorMock(int returnValue)
    {
        var occupancyMock = new Mock<ISensor>();
        occupancyMock.Setup(os => os.GetValue()).Returns(returnValue);
        return occupancyMock;
    }

    private Mock<ITempCalculator> CreateTempCalculatorMock(int numPeople, int outsideTemp, double expectedTemp)
    {
        var calculatorMock = new Mock<ITempCalculator>();
        calculatorMock.Setup(calc => calc.CalculateNewTemperature(numPeople, outsideTemp)).Returns(expectedTemp);
        return calculatorMock;
    }

    private Mock<IRegulator> CreateRegulatorMock()
    {
        return new Mock<IRegulator>();
    }

    [Fact]
    public void AutoClimateController_CanInstantiateWithDependencies()
    {
        // Arrange
        var occupancyMock = CreateOccupancySensorMock(3); // Example value
        var tempMock = CreateTempSensorMock(20); // Example value
        var calculatorMock = CreateTempCalculatorMock(3, 20, 10.8); // Example value
        var regulatorMock = CreateRegulatorMock();

        // Act
        var controller = new AutoClimateController(occupancyMock.Object, tempMock.Object, calculatorMock.Object, regulatorMock.Object);

        // Assert
        Assert.NotNull(controller); // Verify that the controller instance is not null
    }

    [Fact]
    public void AdjustClimate_NoOccupants_DoesNotChangeTemperature()
    {
        // Arrange
        var occupancyMock = CreateOccupancySensorMock(0);
        var tempMock = CreateTempSensorMock(20); 
        var calculatorMock = CreateTempCalculatorMock(0, 20, 0);
        var regulatorMock = CreateRegulatorMock();

        var controller = new AutoClimateController(occupancyMock.Object, tempMock.Object, calculatorMock.Object, regulatorMock.Object);

        // Act
        controller.AdjustClimate();

        // Assert
        regulatorMock.Verify(r => r.ChangeTemp(0), Times.Once());
    }

    [Theory]
    [InlineData(3, 20, 10.8)]
    [InlineData(5, 22, 16.5)]
    [InlineData(0, 20, 0)]
    public void AdjustClimate_CalculationResultsInDifferentTemperature_ChangesTemperature(int numPeople, int outsideTemp, double expectedTemp)
    {
        // Arrange
        var occupancyMock = CreateOccupancySensorMock(numPeople);
        var tempMock = CreateTempSensorMock(outsideTemp);
        var calculatorMock = CreateTempCalculatorMock(numPeople, outsideTemp, expectedTemp);
        var regulatorMock = CreateRegulatorMock();

        var controller = new AutoClimateController(occupancyMock.Object, tempMock.Object, calculatorMock.Object, regulatorMock.Object);

        // Act
        controller.AdjustClimate();

        // Assert
        regulatorMock.Verify(r => r.ChangeTemp(expectedTemp), Times.Once());
    }


    [Fact]
    public async Task AutoClimateControllerSimulator_RepeatedlyCallsAdjustClimate_ChangesTemperatureMultipleTimes()
    {
        

        var tempMock = CreateTempSensorMock(20); // Example value
        var occupancyMock = CreateOccupancySensorMock(3); // Example value
        var calculatorMock = CreateTempCalculatorMock(3, 20, 10.8); // Example value
        var regulatorMock = CreateRegulatorMock();

        var controller = new AutoClimateController(occupancyMock.Object, tempMock.Object, calculatorMock.Object, regulatorMock.Object);
        var simulator = new AutoClimateControllerSimulator(controller);

        // Act
        await Task.Delay(3000); // Wait for the simulator to call AdjustClimate three times

        // Assert
        regulatorMock.Verify(r => r.ChangeTemp(10.8), Times.AtLeast(3)); // Expect ChangeTemp to be called at least three times
    }
    [Theory]
    [InlineData(0, 20, 0)] // No occupants, no change in temperature
    [InlineData(1, 20, 3.6)] // One person, base factor applied
    [InlineData(4,20,14.4)]
    
    public void CalculateNewTemperature_GivenNumberOfPeopleAndOutsideTemp_ReturnsExpectedTemperature(
    int numPeople, int outsideTemp, double expectedTemp)
    {
        // Arrange
        var calculator = new TemperatureCalculator();

        // Act
        var result = calculator.CalculateNewTemperature(numPeople, outsideTemp);

        // Assert
        Assert.Equal(expectedTemp, result, 2); // Allow for small rounding differences
    }


 



}
