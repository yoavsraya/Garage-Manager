using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    abstract class vehicle
    {
        private readonly string r_LicensePlate;
        private readonly string r_ModelName;
        private float m_EnergyMeterPercent;
        protected MotorType m_MotorType = null;      
        protected Wheels m_Wheels;
        protected const bool k_Valid = true;

        public vehicle(in string i_LicensePlate, in string i_ModelName)
        {
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

        protected void UpdateWheelsInfo(in string i_Manufacturer, in float i_CurrentAirPressure, in float i_MaxTirePressure)
        {
            m_Wheels = new Wheels(i_Manufacturer, i_CurrentAirPressure, i_MaxTirePressure); 
        }

        protected void UpdateMotorInfo(MotorType.eEnergyType i_EnergyType)
        {
            if (i_EnergyType == MotorType.eEnergyType.Electric)
            {
                m_MotorType = new ElectricMotor(i_EnergyType);
            }
            else
            {
                m_MotorType = new GasMotor(i_EnergyType);
            }

            
        }

        public void ReFillVehicle(in float i_NewCurrentEnergy, in MotorType.eEnergyType i_EnergyType)
        {
            if(m_MotorType == null)
            {
                throw new ArgumentNullException();
            }
            else
            {
                m_MotorType.ReFill(i_NewCurrentEnergy, i_EnergyType);
                m_EnergyMeterPercent = i_NewCurrentEnergy / m_MotorType.maxEnergy;
            }
        }

        public void FillWheelsAirToMax()
        {
            FillWheelsAir(m_Wheels.MaxAirPressure);
        }

        public void FillWheelsAir(in float i_pressure)
        {
            if (m_Wheels.NumOfWheels == 0)
            {
                throw new ArgumentNullException();
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
                throw new ArgumentException();
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
    }
}
