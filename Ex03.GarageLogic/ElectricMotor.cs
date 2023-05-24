﻿using System;

namespace Ex03.GarageLogic
{
    internal class ElectricMotor : MotorType
    {
        private float m_BatteryHoursLeft = 0;
        private float m_BatteryMaxHours = 0;

        public override void ReFill(in float i_energy, in eEnergyType i_energyType)
        {
            if (i_energyType != EnergyType)
            {
                throw new ArgumentException("this car can get only energy");
            }
            else if (i_energy + m_BatteryHoursLeft > m_BatteryMaxHours)
            {
                m_BatteryHoursLeft = m_BatteryMaxHours;
                throw new fillEnergyToMaxException();
            }
            else
            {
                m_BatteryHoursLeft += i_energy;
            }
        }

        public override float maxEnergy
        {
            get
            {
                return m_BatteryMaxHours;
            }
            set
            {
                m_BatteryMaxHours = value;
            }
        }

        public ElectricMotor(eEnergyType i_EnergyType) : base(i_EnergyType)
        {}

        public override string ToString()
        {
            return "Electric Engine";
        }

        public override float currentEnergy
        {
            get
            {
                return m_BatteryHoursLeft;
            }
            set
            {
                if (value > m_BatteryMaxHours)
                {
                    throw new ArgumentException("current battery cant be more then the max");
                }
                m_BatteryHoursLeft = value;
            }
        }
    }
            
}
