using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoClimateControllerConsoleApp
{
    internal class AutoClimateController
    {
        private readonly ISensor _occupancySensor;
        private readonly ISensor _tempSensor;
        private readonly ITempCalculator _tempCalculator;
        private readonly IRegulator _regulator;

        public AutoClimateController()
        {
            _occupancySensor = new OccupancySensor();
            _tempSensor = new TempSensor();
            _tempCalculator = new TemperatureCalculator();
            _regulator = new Regulator();
        }

        public void AdjustClimate()
        {
            int totalPerson = _occupancySensor.GetValue();
            int outsideTemp = _tempSensor.GetValue();
            double newTemperature = _tempCalculator.CalculateNewTemperature(totalPerson, outsideTemp);
            _regulator.ChangeTemp(newTemperature);
        }
    }
}
