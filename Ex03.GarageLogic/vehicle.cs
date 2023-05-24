using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        protected const bool k_Valid = true;
        private readonly string r_LicensePlate;
        private readonly string r_ModelName;
        private float m_EnergyMeterPercent = 0;
        protected int m_NumOfRequirements = 0;
        protected MotorType m_MotorType = null;      
        protected Wheels m_Wheels = null;

        public Vehicle(in string i_LicensePlate, in string i_ModelName)
        {
            if(i_LicensePlate.Length == 0 || i_ModelName.Length == 0) 
            {
                throw new ArgumentException("license plate or model name cant be empty");
            }
            r_LicensePlate = i_LicensePlate;
            r_ModelName = i_ModelName;
        }

        public string LicensePlate
        {
            get
            {
                return r_LicensePlate;
            }
        }

        public float EnergyMeterPercent
        {
            get
            {
                return m_EnergyMeterPercent;
            }

            set
            {
                m_EnergyMeterPercent = value;
            }
        }


        public void ReFillVehicle(in float i_NewCurrentEnergy, in MotorType.eEnergyType i_EnergyType)
        {
            if(m_MotorType == null)
            {
                throw new ArgumentNullException("engine details has not created yet!");
            }
            else
            {
                try
                {
                    m_MotorType.ReFill(i_NewCurrentEnergy, i_EnergyType);
                    m_EnergyMeterPercent = m_MotorType.calculateMeterPercent();
                }
                catch (fillEnergyToMaxException e)
                {
                    m_EnergyMeterPercent = m_MotorType.calculateMeterPercent();
                    throw e;
                }
            }
        }

        public void FillWheelsAirToMax()
        {
            if (m_Wheels == null)
            {
                throw new NullReferenceException("wheels details has not entered yet");
            }
            m_Wheels.CurrentAirPressure = m_Wheels.MaxAirPressure;
        }

        public void FillWheelsAir(in float i_pressure)
        {
            if (m_Wheels == null)
            {
                throw new ArgumentNullException("wheels details has not created yet!");
            }

            m_Wheels.FillAirWheel(i_pressure);
        }

        public abstract List<string> RequirementsList();

        public abstract void BuildVehicle(in List<string> i_ListOfAnswers);

        protected void CreateEngine(in string i_EngineType)
        {
            if (i_EngineType.ToLower() == "electric")
            {
                m_MotorType = new ElectricMotor(MotorType.eEnergyType.Electric);
            }
            else if (i_EngineType.ToLower() == "fuel")
            {
                m_MotorType = new GasMotor(MotorType.eEnergyType.Octan98);
            }
            else
            {
                throw new ArgumentException("engine type must be electric/fuel only!");
            }
        }

        public virtual List<string> VehicleDetails()
        {
            List<string> details = new List<string>();

            details.Add(string.Format("License plate: {0}", r_LicensePlate));
            details.Add(string.Format("Model name: {0}", r_ModelName));
            details.Add(string.Format("Engine type: {0}", m_MotorType.ToString()));

            if (m_MotorType is GasMotor)
            {
                details.Add(string.Format("Fuel type: {0}", m_MotorType.EnergyType));
                details.Add(string.Format("Fuel left: {0:p2}", m_EnergyMeterPercent));
            }
            else
            {
                details.Add(string.Format("Energy left: {0:p2}", m_EnergyMeterPercent));
            }

            details.Add(string.Format("Tier manufacturer: {0}", m_Wheels.Manufacturer));
            details.Add(string.Format("Tier current pressure: {0}", m_Wheels.CurrentAirPressure));

            return details;
        }

        protected abstract void updateEnergyDetails(in float i_currentEnergy);
    }
}
