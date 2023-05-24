using System;
using System.Collections.Generic;
using static Ex03.GarageLogic.Track.eRequirements;

namespace Ex03.GarageLogic
{
    class Track : Vehicle
    {
        public enum eRequirements
        {
            TrunkToxic = 0,
            TrunkValume,
            TierManufacturer,
            CurrentTierPressure,
            currentEnergy,
        }

        private const bool k_HasToxicTrunk = true;
        private bool m_ToxicTrunk = !k_HasToxicTrunk;
        private float m_TrunkVolume;

        public Track(in string i_LicensePlate, in string i_ModelName)
            : base(i_LicensePlate, i_ModelName)
        {
            m_NumOfRequirements = Enum.GetNames(typeof(eRequirements)).Length;
            m_Wheels = new Wheels(14, 26);
        }

        public override List<string> RequirementsList()
        {
            List<string> RequirementsList = new List<string>(m_NumOfRequirements)
            {
                "hold toxic cargo (yes, no)",
                "trunk volume",
                "tier manufacturer",
                $"Current tier pressure (max: {m_Wheels.MaxAirPressure})",
                $"current fuel in liter (max : {135})"
            };

            return RequirementsList;
        }

        public override void BuildVehicle(in List<string> i_ListOfAnswers)
        {
            if (i_ListOfAnswers[((int)TrunkToxic)] == "yes")
            {
                m_ToxicTrunk = k_HasToxicTrunk;
            }
            else if(i_ListOfAnswers[((int)TrunkToxic)] == "no")
            {
                m_ToxicTrunk = !k_HasToxicTrunk;
            }
            else
            {
                throw new AggregateException("trunk toxic need to answer only by yes/no");
            }

            if (!float.TryParse(i_ListOfAnswers[(int)TrunkValume], out m_TrunkVolume))
            {
                throw new FormatException("trunk volume is not valid");
            }

            try
            {
                if (!float.TryParse(i_ListOfAnswers[(int)CurrentTierPressure], out float currTierPressure))
                {
                    throw new FormatException("tier pressure must be a number");
                }

                m_Wheels.UpdateWheelDetails(currTierPressure, i_ListOfAnswers[(int)TierManufacturer]);
            
                if (!float.TryParse(i_ListOfAnswers[(int)currentEnergy], out float currEnergy))
                {
                    throw new FormatException("energy must be a number");
                }

                m_MotorType = new GasMotor();
                UpdateFuelType();
                UpdateEnergyDetails(currEnergy);
            }
            catch(Exception e)
            {
                throw e;
            }

        }

        public override List<string> VehicleDetails()
        {
            List<string> details = base.VehicleDetails();
            details.Add(string.Format("Trunk volume: {0}", m_TrunkVolume));

            if (m_ToxicTrunk)
            {
                details.Add(string.Format("cargo containing toxic"));
            }
            else
            {
                details.Add(string.Format("cargo is clean from toxic"));
            }
  
            return details;
        }

        protected override void UpdateEnergyDetails(in float i_currentEnergy)
        {
            if (m_MotorType == null)
            {
                throw new NullReferenceException("engine has not set yet!");
            }
            else
            {
                m_MotorType.MaxEnergy = 135f;
                m_MotorType.CurrentEnergy = i_currentEnergy;
                EnergyMeterPercent = m_MotorType.CalculateMeterPercent();
            }
        }

        protected override void UpdateFuelType()
        {
            m_MotorType.EnergyType = MotorType.eEnergyType.Soler;
        }

    }
}
