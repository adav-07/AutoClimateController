using Xunit;
using Moq;
using System;
using AutoClimateControllerConsoleApp;
using NewAutoClimateControllerLib.Tests;

// Assuming TestHelpers class is defined somewhere accessible

public class AutoClimateControllerTests
{
    [Fact]
    public void AutoClimateController_CanInstantiateWithDependencies()
    {
        // Arrange
        var occupancyMock = TestHelpers.CreateOccupancySensorMock(3); // Example value
        var tempMock = TestHelpers.CreateTempSensorMock(20); // Example value
        var calculatorMock = TestHelpers.CreateTempCalculatorMock(3, 20, 10.8); // Example value
        var regulatorMock = TestHelpers.CreateRegulatorMock();

        // Act
        var controller = new AutoClimateController(occupancyMock.Object, tempMock.Object, calculatorMock.Object, regulatorMock.Object);

        // Assert
        Assert.NotNull(controller); // Verify that the controller instance is not null
    }

    [Fact]
    public void AdjustClimate_NoOccupants_DoesNotChangeTemperature()
    {
        // Arrange
        var occupancyMock = TestHelpers.CreateOccupancySensorMock(0);
        var tempMock = TestHelpers.CreateTempSensorMock(20);
        var calculatorMock = TestHelpers.CreateTempCalculatorMock(0, 20, 0);
        var regulatorMock = TestHelpers.CreateRegulatorMock();

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
        var occupancyMock = TestHelpers.CreateOccupancySensorMock(numPeople);
        var tempMock = TestHelpers.CreateTempSensorMock(outsideTemp);
        var calculatorMock = TestHelpers.CreateTempCalculatorMock(numPeople, outsideTemp, expectedTemp);
        var regulatorMock = TestHelpers.CreateRegulatorMock();

        var controller = new AutoClimateController(occupancyMock.Object, tempMock.Object, calculatorMock.Object, regulatorMock.Object);

        // Act
        controller.AdjustClimate();

        // Assert
        regulatorMock.Verify(r => r.ChangeTemp(expectedTemp), Times.Once());
    }

    [Fact]
    public async Task AutoClimateControllerSimulator_RepeatedlyCallsAdjustClimate_ChangesTemperatureMultipleTimes()
    {
        // Arrange
        var tempMock = TestHelpers.CreateTempSensorMock(20); // Example value
        var occupancyMock = TestHelpers.CreateOccupancySensorMock(3); // Example value
        var calculatorMock = TestHelpers.CreateTempCalculatorMock(3, 20, 10.8); // Example value
        var regulatorMock = TestHelpers.CreateRegulatorMock();

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
    [InlineData(4, 20, 14.4)]
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
