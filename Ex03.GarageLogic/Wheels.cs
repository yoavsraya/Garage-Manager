using System;

namespace Ex03.GarageLogic
{
    public class Wheels
    {
        private string m_Manufacturer = default;
        private float m_CurrentAirPressure = 0;
        private readonly float m_MaxAirPressure;
        private readonly byte m_NumOfWheels;

        internal string Manufacturer
        {
            get
            {
                return m_Manufacturer;
            }
            set
            {
                m_Manufacturer = value;
            }
        }

        internal float CurrentAirPressure
        {
            get
            {
                return m_CurrentAirPressure;
            }
            set
            {
                if (value > m_MaxAirPressure)
                {
                    throw new ArgumentException("air pressure can't be more then the max wheels air pressure");
                }
                m_CurrentAirPressure = value;
            }
        }

        internal float MaxAirPressure
        {
            get
            {
                return m_MaxAirPressure;
            }
        }

        internal byte NumOfWheels
        {
            get
            {
                return m_NumOfWheels;
            }
        }

        internal void FillAirWheel(in float i_airPressure)
        {
            if (i_airPressure + m_CurrentAirPressure > m_MaxAirPressure)
            {
                throw new ArgumentException(); 
            }

            m_CurrentAirPressure += i_airPressure;
        }

        internal Wheels(in byte i_NumOfWheels, in float i_MaxAirPressure)
        {
            m_MaxAirPressure = i_MaxAirPressure;
            m_NumOfWheels = i_NumOfWheels;
        }

        internal void UpdateWheelDetails(in float i_TierPressure, in string i_Manufactor)
        {
            m_Manufacturer = i_Manufactor;
            if (i_TierPressure > m_MaxAirPressure)
            {
                throw new ArgumentException("tier pressure can't be more then the wheel max pressure!");
            }
            m_CurrentAirPressure = i_TierPressure;
        }
    }
}
