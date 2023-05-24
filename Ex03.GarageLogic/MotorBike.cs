using System.Collections.Generic;
using System;
using static Ex03.GarageLogic.MotorBike.eRequirements;

namespace Ex03.GarageLogic
{
    class MotorBike : Vehicle
    {
        public enum eLicenseType
        {
            A1 = 0,
            A2,
            AA,
            B1,
        }

        public enum eRequirements
        {
            LicenseType = 0,
            EngineVolume,
            EngineType,
            TierManufacturer,
            CurrentTierPressure,
            currentEnergy,
        }

        private int m_MotorVolume;
        private eLicenseType m_LicenseType;

        public MotorBike(in string i_LicensePlate, in string i_ModelName)
            : base(i_LicensePlate, i_ModelName)
        {
            m_NumOfRequirements = (Enum.GetNames(typeof(eRequirements)).Length);
            m_Wheels = new Wheels(2, 31);
        }

        public override List<string> RequirementsList()
        {
            List<string> RequirementsList = new List<string>(m_NumOfRequirements)
            {
              "License type (A1/A2/AA/B1)",
              "Engine volume",
              "Engine type (electric/fuel)",
              "Tier manufacturer",
             $"Current tier pressure (max: {m_Wheels.MaxAirPressure})",
             $"Power left (in hours for electric max: {2.6} / in liter for fuel max: {6.4})",
            };

            return RequirementsList;
        }

        public override void BuildVehicle(in List<string> i_ListOfAnswers)
        {
            if (i_ListOfAnswers.Count != m_NumOfRequirements)
            {
                throw new ArgumentException("answer list is not having the full amount of answers");
            }

            if (!Enum.TryParse(i_ListOfAnswers[(int)LicenseType], out m_LicenseType))
            {
                throw new FormatException("license type is not valid!");
            }

            if (!int.TryParse(i_ListOfAnswers[(int)EngineVolume], out m_MotorVolume))
            {
                throw new FormatException("Engine volume must be number!");
            }

            try
            {
                if(!float.TryParse(i_ListOfAnswers[(int)CurrentTierPressure], out float currTierPressure))
                {
                    throw new FormatException("tier pressure must be a number");
                }

                m_Wheels.UpdateWheelDetails(currTierPressure, i_ListOfAnswers[(int)TierManufacturer]);
                CreateEngine(i_ListOfAnswers[(int)EngineType]);

                if(!float.TryParse(i_ListOfAnswers[(int)currentEnergy], out float currEnergy))
                {
                    throw new FormatException("energy must be a number");
                }

                UpdateEnergyDetails(currEnergy);

            }
            catch(Exception e)
            {
                throw e;
            }
            
        }

        public override List<string> VehicleDetails()
        {
            List<string> details  = base.VehicleDetails();
            details.Add(string.Format("License Type: {0}", m_LicenseType));
            details.Add(string.Format("Engine volume: {0}", m_MotorVolume));

            return details;
        }

        protected override void UpdateEnergyDetails(in float i_currentEnergy)
        {
            if (m_MotorType == null)
            {
                throw new NullReferenceException("engine has not set yet!");
            }

            else if(m_MotorType is GasMotor)
            {
                m_MotorType.MaxEnergy = 6.4f;
            }
            else // is electric
            {
                m_MotorType.MaxEnergy = 2.6f;
            }

            m_MotorType.CurrentEnergy = i_currentEnergy;
            EnergyMeterPercent = m_MotorType.CalculateMeterPercent();

        }

        protected override void UpdateFuelType()
        {
            m_MotorType.EnergyType = MotorType.eEnergyType.Octan98;
        }
    }
}
