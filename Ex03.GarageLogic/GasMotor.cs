using System;

namespace Ex03.GarageLogic
{
    internal class GasMotor : MotorType
    {
        private float m_CurrentGasCapacity;
        private float m_MaxGasCspscity;

        public override void ReFill(in float i_energy, in eEnergyType i_energyType)
        {
            if (EnergyType != i_energyType)
            {
                throw new energyTypeException();
            }

            else if (i_energy + m_CurrentGasCapacity > m_MaxGasCspscity)
            {
                throw new ArgumentException();
            }
            
            m_CurrentGasCapacity += i_energy;
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
    }
}
