using System;

namespace AutoClimateControllerConsoleApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Instantiate the simulator with the required dependencies, which could be real or mocked depending on the context
            // For the sake of demonstration, we're assuming we have real implementations for the dependencies
            var occupancySensor = new OccupancySensor();
            var tempSensor = new TempSensor();
            var tempCalculator = new TemperatureCalculator();
            var regulator = new Regulator();
            var controller = new AutoClimateController(occupancySensor, tempSensor, tempCalculator, regulator);

            AutoClimateControllerSimulator simulator = new AutoClimateControllerSimulator(controller);

            // Keep the main thread alive to prevent the application from exiting
            Console.WriteLine("Press any key to stop...");
            Console.ReadKey();
        }
    }
}
