using System;

namespace Ex03.GarageLogic
{
    internal class ElectricMotor : MotorType
    {
        private float m_BatteryHoursLeft;
        private float m_BatteryMaxHours;

        public override void ReFill(in float i_energy, in eEnergyType i_energyType)
        {
            if (i_energyType != EnergyType)
            {
                throw new energyTypeException();
            }
            else if (i_energy + m_BatteryHoursLeft > m_BatteryMaxHours)
            {
                throw new ArgumentException("the energy to add is more then the energy allow");
            }

            m_BatteryHoursLeft += i_energy;
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

        public ElectricMotor(eEnergyType i_EnergyType) : base(i_EnergyType){}

        public override string ToString()
        {
            return "Electric Engine";
        }
    }
            
}
