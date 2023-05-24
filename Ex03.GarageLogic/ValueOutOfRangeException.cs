using System;

namespace Ex03.GarageLogic
{
    class ValueOutOfRangeException :Exception
    {
        public override string Message
        {
            get
            {
                return "value is out of Range!";
            }
        }

        private float m_MaxValue { get; set; }
        private float m_MinValue { get; set; }
    }
}
