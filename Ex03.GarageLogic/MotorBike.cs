using System.Collections.Generic;
using System;

namespace Ex03.GarageLogic
{
    class MotorBike : vehicle
    {
        public enum eLicenseType
        {
            A1 = 0,
            A2,
            AA,
            B1,
        }

        private enum eRequirements
        {
            LicenseType = 0,
            EngineVolume,
            EngineType,
            TierManufacturer,
            CurrentTierPressure,
        }

        private int m_MotorVolume;
        private eLicenseType m_LicenseType;

        public MotorBike(in string i_LicensePlate, in string i_ModelName)
            : base(i_LicensePlate, i_ModelName)
        {
            m_NumOfRequirements = 5;
        }

        public override List<string> RequirementsList()
        {
            List<string> RequirementsList = new List<string>(5);

            RequirementsList.Add("License type (A1, A2, AA, B1)");
            RequirementsList.Add("Engine volume"); 
            RequirementsList.Add("Engine type (electric, fuel)"); 
            RequirementsList.Add("Tier manufacturer"); 
            RequirementsList.Add("Current tier pressure"); 

            return RequirementsList;
        }

        public override void BuildVehicle(in List<string> i_ListOfAnswers)
        {
            if (i_ListOfAnswers.Count != m_NumOfRequirements)
            {
                throw new ArgumentException("answer list is not having the full amount of answers");
            }

            if (Enum.TryParse(i_ListOfAnswers[((int)eRequirements.LicenseType)], out m_LicenseType) == !k_Valid)
            {
                throw new ArgumentException("license type is not valid!");
            }

            if (int.TryParse(i_ListOfAnswers[((int)eRequirements.EngineVolume)], out m_MotorVolume) == !k_Valid)
            {
                throw new ArgumentException("Engine volume must be number!");
            }

            try
            {
                CreateEngine(i_ListOfAnswers[((int)eRequirements.EngineType)]);
            }
            catch(Exception e)
            {
                throw e;
            }

            UpdateWheelsInfo(i_ListOfAnswers[((int)eRequirements.TierManufacturer)], float.Parse(i_ListOfAnswers[((int)eRequirements.CurrentTierPressure)]), 31);
            m_Wheels.NumOfWheels = 2;
            m_Wheels.MaxAirPressure = 31;
        }

        public override List<string> VehicleDetails()
        {
            List<string> details  = base.VehicleDetails();
            details.Add(string.Format("License Type: {0}", m_LicenseType));
            details.Add(string.Format("Engine volume: {0}", m_MotorVolume));


            return details;
        }
    }
}
