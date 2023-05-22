using System;
using System.Collections.Generic;
using static Ex03.GarageLogic.Track.eRequirements;

namespace Ex03.GarageLogic
{
    class Track : vehicle
    {
        public enum eRequirements
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
            m_NumOfRequirements = Enum.GetNames(typeof(eRequirements)).Length;
            m_Wheels = new Wheels(14, 26);
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

            if (float.TryParse(i_ListOfAnswers[((int)TrunkValume)], out m_TrunkVolume) != k_Valid)
            {
                throw new AggregateException("trunk volume is not valid");
            }

            try
            {
                m_Wheels.UpdateWheelDetails(float.Parse(i_ListOfAnswers[(int)CurrentTierPressure]), i_ListOfAnswers[(int)TierManufacturer]);
                updateMaxEnergy();
            }
            catch(Exception e)
            {
                throw e;
            }

            m_MotorType = new GasMotor(MotorType.eEnergyType.Soler);
      
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

        protected override void updateMaxEnergy()
        {
            if (m_MotorType == null)
            {
                throw new NullReferenceException("engine has not set yet!");
            }
            else
            {
                m_MotorType.maxEnergy = 135f;
            }
        }

        public override string ToString()
        {
            return "Car";
        }
    }
}
