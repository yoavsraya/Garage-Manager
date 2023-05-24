using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    class fillEnergyToMaxException : Exception
    {
        public override string Message
        {
            get
            {
                return "can't feel more then the max fuel capacity. fuel filled to maximum!";
            }
        }
    }
}