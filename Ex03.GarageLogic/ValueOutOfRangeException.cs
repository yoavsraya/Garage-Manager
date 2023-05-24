using System;

namespace Ex03.GarageLogic
{
    class ValueOutOfRangeException :Exception
    {
     
        public ValueOutOfRangeException(in string i_message) : base(i_message){}

        public static float m_MaxValue { get; set; }
        public static float m_MinValue { get; set; }

    }
}
