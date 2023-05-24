using System;

namespace Ex03.GarageLogic
{
    public class Wheels
    {
        private string m_Manufacturer = default;
        private float m_CurrentAirPressure = 0;
        private readonly float r_MaxAirPressure;
        private readonly byte r_NumOfWheels;

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
                if (value > r_MaxAirPressure || value < 0)
                {
                    throw new ValueOutOfRangeException(0, r_MaxAirPressure, "tier pressure");
                }

                m_CurrentAirPressure = value;
            }
        }

        internal float MaxAirPressure
        {
            get
            {
                return r_MaxAirPressure;
            }
        }

        internal Wheels(in byte i_NumOfWheels, in float i_MaxAirPressure)
        {
            r_MaxAirPressure = i_MaxAirPressure;
            r_NumOfWheels = i_NumOfWheels;
        }

        internal void UpdateWheelDetails(in float i_TierPressure, in string i_Manufactor)
        {
            m_Manufacturer = i_Manufactor;
            CurrentAirPressure = i_TierPressure;
        }
    }
}
