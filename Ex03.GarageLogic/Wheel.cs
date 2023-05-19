using System;

namespace Ex03.GarageLogic
{
    class Wheel
    {
        private string m_Manufacturer;
        float m_CurrentAirPressure;
        float m_MaxAirPressure;


        public void FillAirWheel(in float i_airPressure)
        {
            if (i_airPressure + m_CurrentAirPressure > m_MaxAirPressure)
            {
                throw new ArgumentException(); 
            }

            m_CurrentAirPressure += i_airPressure;
        }
    }
}
