using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoClimateControllerConsoleApp
{
    public class OccupancySensor : ISensor
    {
    private static readonly Random _random = new Random();
        public int GetValue()
        {
            return _random.Next(1, 5);
        }
    }
}
