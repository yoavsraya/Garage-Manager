using System;

namespace Ex03.GarageLogic
{
    abstract class MotorType
    {
        public enum eEnergyType
        {
            Soler = 0,
            Octan95,
            Octan96,
            Octan98,
            Electric,
        }

        private eEnergyType m_GasType;

        public eEnergyType GasType
        {
            get
            {
                return m_GasType;
            }
            set
            {
                m_GasType = value;
            }
        }

        public abstract void FillEnergy(in float i_energy, in eEnergyType i_energyType);
   



    }
}
