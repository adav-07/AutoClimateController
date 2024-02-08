
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace AutoClimateControllerConsoleApp
{
    public class AutoClimateControllerSimulator
    {
        private System.Timers.Timer _timer;
        private AutoClimateController _controller;
        private TempSensor _tempSensor;
        private OccupancySensor _occupancySensor;

        public AutoClimateControllerSimulator()
        {
            _controller = new AutoClimateController();
            _tempSensor = new TempSensor();
            _occupancySensor = new OccupancySensor();

            // Initialize the timer to check every minute (60000 ms)
            _timer = new System.Timers.Timer(2000);
            _timer.Elapsed += OnTimerElapsed;
            _timer.Start();
        }

        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            // Read sensor values
            int outsideTemp = _tempSensor.GetValue();
            int occupancy = _occupancySensor.GetValue();

            // Output the current sensor readings to the console
            Console.WriteLine($"Current Outside Temperature: {outsideTemp}, Current Occupancy: {occupancy}");

            // Call the controller's adjust climate method
            _controller.AdjustClimate();
        }

    }
}
