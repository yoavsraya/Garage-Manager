using System;

namespace Ex03.GarageLogic
{
    internal class Wheel
    {
        private string m_Manufacturer;
        private float m_CurrentAirPressure;
        private float m_MaxAirPressure;


        internal void FillAirWheel(in float i_airPressure)
        {
            if (i_airPressure + m_CurrentAirPressure > m_MaxAirPressure)
            {
                throw new ArgumentException(); 
            }

            m_CurrentAirPressure += i_airPressure;
        }

        internal Wheel(in string i_Manufacturer, in float i_CurrentAirPressure, in float i_MaxAirPressure)
        {
            m_Manufacturer = i_Manufacturer;
            m_CurrentAirPressure = i_CurrentAirPressure;
            m_MaxAirPressure = i_MaxAirPressure;
        }

        public float MaxAirPressure
        {
            get
            {
                return m_MaxAirPressure;
            }
        }
    }
}
