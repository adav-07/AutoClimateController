using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoClimateControllerConsoleApp
{
    public class TemperatureCalculator : ITempCalculator
    {
        private double BaseFactor = 0.18;
        public double CalculateNewTemperature(int numPeople, int outsideTemp)
        {
            // Logic to calculate the new temperature
            return Convert.ToDouble(numPeople) * Convert.ToDouble(outsideTemp) * BaseFactor; // Example calculation
        }
    }
}
