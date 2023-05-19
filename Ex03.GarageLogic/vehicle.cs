using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    abstract class vehicle
    {
        private readonly string r_LicensePlate;
        private readonly string r_ModelName;
        protected MotorType m_MotorType;
        private float m_EnergyMeterPercent;
        protected List<Wheel> m_Wheels;

        public vehicle(in string i_LicensePlate, in string i_ModelName)
        {
            r_LicensePlate = i_LicensePlate;
            r_ModelName = i_ModelName;
        }

        protected void UpdateWheelsInfo(in byte i_NumOfWheels, in string i_Manufacturer, in float i_CurrentAirPressure, in float i_MaxAirPressure)
        {
            for (byte i = 0; i < i_NumOfWheels; i++)
            {
                m_Wheels.Add(new Wheel(i_Manufacturer, i_CurrentAirPressure, i_MaxAirPressure));
            }
        }

        public abstract void gatherWheelsInfoByObject(in string i_Manufacturer, in float i_CurrentAirPressure);

        public void ReFillVehicle(in float i_NewCurrentEnergy, in MotorType.eEnergyType i_EnergyType)
        {
            m_MotorType.ReFill(i_NewCurrentEnergy, i_EnergyType);
            m_EnergyMeterPercent = m_MotorType.GetMaxEnergy() / i_NewCurrentEnergy;
        }

        public string LicensePlate
        {
            get
            {
                return r_LicensePlate;
            }
        }


    }
}
