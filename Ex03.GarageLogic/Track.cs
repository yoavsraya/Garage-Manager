﻿using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    class Track : vehicle
    {
        private enum eRequirements
        {
            TrunkToxic = 0,
            TrunkValume,
            TierManufacturer,
            CurrentTierPressure,
        }

        private const bool k_HasToxicTrunk = true;
        private bool m_ToxicTrunk = !k_HasToxicTrunk;
        private float m_TrunkVolume;

        public Track(in string i_LicensePlate, in string i_ModelName)
            : base(i_LicensePlate, i_ModelName)
        {
            m_NumOfRequirements = 4;
        }

        public override List<string> RequirementsList()
        {
            List<string> RequirementsList = new List<string>(4);

            RequirementsList.Add("hold toxic cargo (yes, no)"); 
            RequirementsList.Add("trunk volume"); 
            RequirementsList.Add("tier manufacturer");
            RequirementsList.Add("current tier pressure"); 

            return RequirementsList;
        }

        public override void BuildVehicle(in List<string> i_ListOfAnswers)
        {
            m_MotorType = new GasMotor(MotorType.eEnergyType.Soler);

            if (i_ListOfAnswers[((int)eRequirements.TrunkToxic)] == "yes")
            {
                m_ToxicTrunk = k_HasToxicTrunk;
            }
            else if(i_ListOfAnswers[((int)eRequirements.TrunkToxic)] == "no")
            {
                m_ToxicTrunk = !k_HasToxicTrunk;
            }
            else
            {
                throw new AggregateException("one of the inputs is not valid");
            }

            if (float.TryParse(i_ListOfAnswers[((int)eRequirements.TrunkValume)], out m_TrunkVolume) != k_Valid)
            {
                throw new AggregateException("one of the inputs is not valid");
            }

            UpdateWheelsInfo(i_ListOfAnswers[((int)eRequirements.TierManufacturer)], float.Parse(i_ListOfAnswers[((int)eRequirements.CurrentTierPressure)]), 26);
            m_Wheels.NumOfWheels = 14;
            m_Wheels.MaxAirPressure = 26;
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
    }
}
