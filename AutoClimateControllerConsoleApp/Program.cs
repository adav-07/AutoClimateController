namespace AutoClimateControllerConsoleApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Instantiate the simulator, which starts the timer and begins the simulation
            

            AutoClimateControllerSimulator simulator = new AutoClimateControllerSimulator();
            

            // Keep the main thread alive to prevent the application from exiting
            Console.WriteLine("Press any key to stop...");
            Console.ReadKey();
        }
    }
}
