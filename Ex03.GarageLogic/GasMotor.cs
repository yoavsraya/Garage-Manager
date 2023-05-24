using System;

namespace Ex03.GarageLogic
{
    internal class GasMotor : MotorType
    {
        private float m_CurrentGasCapacity = 0;
        private float m_MaxGasCspscity = 0;

        public override void ReFill(in float i_energy, in eEnergyType i_energyType)
        {
            if (EnergyType != i_energyType)
            {
                throw new ArgumentException("fuel type is not matching the car fuel type!");
            }
            else if (i_energy + m_CurrentGasCapacity > m_MaxGasCspscity)
            {
                m_CurrentGasCapacity = m_MaxGasCspscity;
                throw new fillEnergyToMaxException();
            }
            else
            {
                m_CurrentGasCapacity += i_energy;
            }
        }

        public override float maxEnergy
        {
            get
            {
                return m_MaxGasCspscity;
            }
            set
            {
                m_MaxGasCspscity = value;
            }
        }

        public GasMotor(eEnergyType i_EnergyType) : base(i_EnergyType)
        {}

        public override string ToString()
        {
            return "Fuel Engine";
        }

        public override float currentEnergy
        {
            get
            {
                return m_CurrentGasCapacity;
            }
            set
            {
                if (value > m_MaxGasCspscity)
                {
                    throw new ArgumentException("current fuel cant be more then the max");
                }
                m_CurrentGasCapacity = value;
            }
        }

    }
}
