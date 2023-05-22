using System;

namespace Ex03.GarageLogic
{
    public abstract class MotorType
    {
        public enum eEnergyType
        {
            Soler = 0,
            Octan95,
            Octan96,
            Octan98,
            Electric,
        }
        public eEnergyType EnergyType
        {
            get
            {
                return m_EnergyType;
            }
            set
            {
                if(Enum.IsDefined(typeof(eEnergyType), value))
                {
                    m_EnergyType = value;
                }
                else
                {
                    throw new AggregateException();
                }
            }
        }

        private eEnergyType m_EnergyType;

        public abstract void ReFill(in float i_energy, in eEnergyType i_energyType);

        public abstract float maxEnergy
        {
            get;
            set;
        }

        public abstract float currentEnergy
        {
            get;
            set;
        }

        public MotorType(eEnergyType i_EnergyType)
        {
            m_EnergyType = i_EnergyType;
        }

        

    }
}
