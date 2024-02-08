using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoClimateControllerConsoleApp
{
    public class TempSensor : ISensor
    {
        private static readonly Random _random = new Random();
        public int GetValue()
        {
            // Logic to get the outside temperature
            return _random.Next(10, 30); // Example value
        }
    }
}
