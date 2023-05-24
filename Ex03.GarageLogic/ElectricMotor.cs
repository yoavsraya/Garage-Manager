using System;

namespace Ex03.GarageLogic
{
    internal class ElectricMotor : MotorType
    {
        private float m_BatteryHoursLeft = 0;
        private float m_BatteryMaxHours = 0;

        public ElectricMotor()
        {
            EnergyType = eEnergyType.Electric;
        }

        public override void ReFill(in float i_Energy, in eEnergyType i_EnergyType)
        {
            if (i_EnergyType != EnergyType)
            {
                throw new ArgumentException("this car can get only energy");
            }
            else
            {
                CurrentEnergy += i_Energy;
            }
        }

        public override float MaxEnergy
        {
            get
            {
                return m_BatteryMaxHours;
            }
            set
            {
                m_BatteryMaxHours = value;
            }
        }

        public override string ToString()
        {
            return "Electric Engine";
        }

        public override float CurrentEnergy
        {
            get
            {
                return m_BatteryHoursLeft;
            }
            set
            {
                if (value > m_BatteryMaxHours || value < 0)
                {
                    throw new ValueOutOfRangeException(0, m_BatteryMaxHours, "battery capacity");
                }

                m_BatteryHoursLeft = value;
            }
        }
    }
            
}
