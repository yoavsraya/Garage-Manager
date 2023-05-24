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
            else
            {
                CurrentEnergy += i_energy;
            }
        }

        public override float MaxEnergy
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

        public override string ToString()
        {
            return "Fuel Engine";
        }

        public override float CurrentEnergy
        {
            get
            {
                return m_CurrentGasCapacity;
            }
            set
            {
                if (value > m_MaxGasCspscity || value < 0)
                {
                    throw new ValueOutOfRangeException(0, m_MaxGasCspscity, "fuel capacity");
                }
                m_CurrentGasCapacity = value;
            }
        }
    }
}
