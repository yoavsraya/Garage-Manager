using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    class Track : vehicle
    {
        private const bool k_HasToxicTrunk = true;
        private bool m_ToxicTrunk = !k_HasToxicTrunk;
        private float m_TrunkVolume;

        public Track(in string i_LicensePlate, in string i_ModelName)
            : base(i_LicensePlate, i_ModelName)
        {
            m_NumOfWheels = 14;
            m_MaxTirePressure = 26;
        }

        public override List<string> RequirementsList()
        {
            List<string> RequirementsList = new List<string>(4);

            RequirementsList.Add("yes if you hold toxic cargo"); //0
            RequirementsList.Add("trunk volume"); // 1
            RequirementsList.Add("tier manufacturer"); //2
            RequirementsList.Add("current tier pressure"); //3

            return RequirementsList;
        }

        public override void BuildVehicle(in List<string> i_ListOfAnswers)
        {
            if (i_ListOfAnswers[0] == "yes")
            {
                m_ToxicTrunk = k_HasToxicTrunk;
            }
            else if(i_ListOfAnswers[0] == "no")
            {
                m_ToxicTrunk = !k_HasToxicTrunk;
            }
            else
            {
                throw new AggregateException("one of the inputs is not valid");
            }

            if (float.TryParse(i_ListOfAnswers[1], out m_TrunkVolume) != k_Valid)
            {
                throw new AggregateException("one of the inputs is not valid");
            }

            UpdateWheelsInfo(i_ListOfAnswers[2], float.Parse(i_ListOfAnswers[3]), m_MaxTirePressure);
        }
    }
}
