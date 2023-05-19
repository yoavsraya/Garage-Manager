using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    class vehicle
    {
        MotorType m_MotorType;
        readonly string r_LicensePlate;
        float m_EnergyLeftPercent;
        string m_ModelName;
        List<Wheel> m_Wheels;
    }
}
