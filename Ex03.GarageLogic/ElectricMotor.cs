using System;

namespace Ex03.GarageLogic
{
    internal class ElectricMotor : MotorType
    {
        private float m_BatteryHoursLeft = 0;
        private float m_BatteryMaxHours = 0;

        public override void ReFill(in float i_energy, in eEnergyType i_energyType)
        {
            if (i_energyType != EnergyType)
            {
                throw new ArgumentException("this car can get only energy");
            }
            else if (i_energy + m_BatteryHoursLeft > m_BatteryMaxHours)
            {
                m_BatteryHoursLeft = m_BatteryMaxHours;
                throw new ArgumentException("the energy can't be more then the max energy capacity, energy filled to maximum!");
            }
            else
            {
                m_BatteryHoursLeft += i_energy;
            }
        }

        public override float maxEnergy
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

        public ElectricMotor(eEnergyType i_EnergyType) : base(i_EnergyType)
        {}

        public override string ToString()
        {
            return "Electric Engine";
        }
    }
            
}
