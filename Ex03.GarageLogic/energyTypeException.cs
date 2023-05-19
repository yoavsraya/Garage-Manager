using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    public class energyTypeException : Exception
    {
        public energyTypeException()
        : base("the energy type is not matching the vehicle")
        {}

    }
}
