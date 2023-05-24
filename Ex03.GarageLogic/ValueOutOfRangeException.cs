using System;

namespace Ex03.GarageLogic
{
    class ValueOutOfRangeException :Exception
    {
        private readonly float r_MaxValue;
        private readonly float r_MinValue;
        private readonly string r_valueType;

        public ValueOutOfRangeException(in float i_min, in float i_max, in string i_Value)
        {
            r_MaxValue = i_max;
            r_MinValue = i_min;
            r_valueType = i_Value;
        }

        public override string Message
        {
            get
            {
                return $"{r_valueType} out of range. Allowed range: {r_MinValue} - {r_MaxValue}.";
            }
        }

    }
}
