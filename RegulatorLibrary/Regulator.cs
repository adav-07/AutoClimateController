using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AutoClimateControllerConsoleApp
{
    public class Regulator : IRegulator
    {
        public void ChangeTemp(double newTemp)
        {
            Console.WriteLine($"Temperature changed to: {newTemp}");
        }
    }
}
