

namespace AutoClimateControllerConsoleApp
{
    public class AutoClimateController
    {
        private readonly ISensor _occupancySensor;
        private readonly ISensor _tempSensor;
        private readonly ITempCalculator _tempCalculator;
        private readonly IRegulator _regulator;

        // Refactored constructor accepts interfaces as parameters
        public AutoClimateController(ISensor occupancySensor, ISensor tempSensor, ITempCalculator tempCalculator, IRegulator regulator)
        {
            _occupancySensor = occupancySensor;
            _tempSensor = tempSensor;
            _tempCalculator = tempCalculator;
            _regulator = regulator;
        }

        public void AdjustClimate()
        {
            int totalPerson = _occupancySensor.GetValue();
            int outsideTemp = _tempSensor.GetValue();
            Console.WriteLine($"Current Temp: {outsideTemp}");
            Console.WriteLine($"Current Temp: {totalPerson}");
            double newTemperature = _tempCalculator.CalculateNewTemperature(totalPerson, outsideTemp);
            _regulator.ChangeTemp(newTemperature);
        }
    }
}
