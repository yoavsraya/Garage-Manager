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

        private int m_MotorVolume;
        private eLicenseType m_LicenseType;

        public MotorBike(in string i_LicensePlate, in string i_ModelName)
            : base(i_LicensePlate, i_ModelName)
        {
            m_NumOfWheels = 2;
            m_MaxTirePressure = 31;
        }

        public override List<string> RequirementsList()
        {
            List<string> RequirementsList = new List<string>(4);

            RequirementsList.Add("License type");
            RequirementsList.Add("Engine volume");
            RequirementsList.Add("Engine type");
            RequirementsList.Add("current tier pressure");

            return RequirementsList;
        }

        public override void BuildVehicle(in List<string> i_ListOfAnswers)
        {
            if (Enum.TryParse(i_ListOfAnswers[0], out m_LicenseType) == !k_Valid)
            {
                throw new Exception();
            }

            if (int.TryParse(i_ListOfAnswers[1], out m_MotorVolume) == !k_Valid)
            {
                throw new Exception();
            }

            try
            {
                CreateEngine(i_ListOfAnswers[2]);
            }
            catch(Exception e)
            {
                throw e;
            }

            UpdateWheelsInfo(i_ListOfAnswers[3], float.Parse(i_ListOfAnswers[3]), m_MaxTirePressure);
        }
    }
}
