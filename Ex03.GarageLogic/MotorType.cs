using System;

namespace Ex03.GarageLogic
{
    public abstract class MotorType
    {
        private eEnergyType m_EnergyType;

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
                if(!Enum.IsDefined(typeof(eEnergyType), value))
                {
                    throw new FormatException("Energy type is not valid");
                }

                m_EnergyType = value;
            }
        }

        public abstract void ReFill(in float i_Energy, in eEnergyType i_EnergyType);

        public abstract float MaxEnergy
        {
            get;
            set;
        }

        public abstract float CurrentEnergy
        {
            get;
            set;
        }

        public float CalculateMeterPercent()
        {
            return CurrentEnergy / MaxEnergy;
        }
    }
}
