using System;

namespace Ex03.GarageLogic
{
    internal class Wheels
    {
        private string m_Manufacturer;
        private float m_CurrentAirPressure = 0;
        private float m_MaxAirPressure;
        private byte m_NumOfWheels = 0;


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
                m_CurrentAirPressure = value;
            }
        }

        internal float MaxAirPressure
        {
            get
            {
                return m_MaxAirPressure;
            }
            set
            {
                m_MaxAirPressure = value;
            }
        }

        internal byte NumOfWheels
        {
            get
            {
                return m_NumOfWheels;
            }
            set
            {
                m_NumOfWheels = value;
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

        internal Wheels(in string i_Manufacturer, in float i_CurrentAirPressure, in float i_MaxAirPressure)
        {
            if (i_CurrentAirPressure > i_MaxAirPressure)
            {
                throw new ArgumentException("Tier pressure can't be more then the maximum");
            }
            m_Manufacturer = i_Manufacturer;
            m_CurrentAirPressure = i_CurrentAirPressure;
            m_MaxAirPressure = i_MaxAirPressure;
        }


    }
}
