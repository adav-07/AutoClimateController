using System;
using System.Timers;

namespace AutoClimateControllerConsoleApp
{
    public class AutoClimateControllerSimulator
    {
        private System.Timers.Timer _timer;
        private AutoClimateController _controller;

        public AutoClimateControllerSimulator(AutoClimateController controller)
        {
            _controller = controller;

            // Initialize the timer to check every second (1000 ms)
            _timer = new System.Timers.Timer(1000);
            _timer.Elapsed += OnTimerElapsed;
            _timer.Start();
        }

        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {

            // Call the controller's adjust climate method
            _controller.AdjustClimate();
        }
    }
}
