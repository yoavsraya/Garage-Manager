using System;

namespace Ex03.GarageLogic
{
    class MotorCycle : vehicle
    {
        public enum eLicenseType
        {
            A1 = 0,
            A2,
            AA,
            B1,
        }

        private int m_MotorVolume;
        eLicenseType m_LicenseType;

    }
}
