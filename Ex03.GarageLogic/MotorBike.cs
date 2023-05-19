using System;

namespace Ex03.GarageLogic
{
    class MotorBike : vehicle
    {
        public enum eLicenseType
        {
            A1 = 0,
            A2,
            AA,
            B1,
        }

        private readonly int r_MotorVolume;
        private readonly eLicenseType r_LicenseType;

        public MotorBike(in string i_LicensePlate, in string i_ModelName, in eLicenseType i_LicenseType, in int i_MotorVolume)
            : base(i_LicensePlate, i_ModelName)
        {
            r_LicenseType = i_LicenseType;
            r_MotorVolume = i_MotorVolume;

            if (m_MotorType is GasMotor)
            {
                m_MotorType.EnergyType = MotorType.eEnergyType.Octan98;
                m_MotorType.maxEnergy = 6.4F;
            }
            else
            {
                m_MotorType.EnergyType = MotorType.eEnergyType.Electric;
                m_MotorType.maxEnergy = 2.6F;
            }
        }

        public override void gatherWheelsInfoByObject(in string i_Manufacturer, in float i_CurrentAirPressure)
        {
            UpdateWheelsInfo(2, i_Manufacturer, i_CurrentAirPressure, 31);
        }
      
    }
}
