using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    abstract class vehicle
    {
        private readonly string r_LicensePlate;
        private readonly string r_ModelName;
        private float m_EnergyMeterPercent;
        protected MotorType m_MotorType = null;
        protected byte m_NumOfWheels;
        protected byte m_MaxTirePressure;
        protected List<Wheel> m_Wheels;

        public vehicle(in string i_LicensePlate, in string i_ModelName)
        {
            r_LicensePlate = i_LicensePlate;
            r_ModelName = i_ModelName;
        }

        public string LicensePlate
        {
            get
            {
                return r_LicensePlate;
            }
        }

        protected void UpdateWheelsInfo(in string i_Manufacturer, in float i_CurrentAirPressure)
        {
            for (byte i = 0; i < m_NumOfWheels; i++)
            {
                m_Wheels.Add(new Wheel(i_Manufacturer, i_CurrentAirPressure, m_MaxTirePressure));
            }
        }

        protected void UpdateMotorInfo(MotorType.eEnergyType i_EnergyType)
        {
            if (i_EnergyType == MotorType.eEnergyType.Electric)
            {
                m_MotorType = new ElectricMotor(i_EnergyType);
            }
            else
            {
                m_MotorType = new GasMotor(i_EnergyType);
            }

            
        }

        public void ReFillVehicle(in float i_NewCurrentEnergy, in MotorType.eEnergyType i_EnergyType)
        {
            if(m_MotorType == null)
            {
                throw new ArgumentNullException();
            }
            else
            {
                m_MotorType.ReFill(i_NewCurrentEnergy, i_EnergyType);
                m_EnergyMeterPercent = i_NewCurrentEnergy / m_MotorType.maxEnergy;
            }
        }

        public void FillWheelsAirToMax()
        {
            if (m_Wheels.Count == 0)
            {
                throw new ArgumentNullException();
            }
            foreach( Wheel wheel in m_Wheels)
            {
                wheel.FillAirWheel(wheel.MaxAirPressure);
            }
        }

        public abstract List<string> RequirementsList();

        public abstract void BuildVehicle(in List<string> i_ListOfAnswers);
        

    }
}
