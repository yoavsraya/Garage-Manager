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
            List<string> RequirementsList = new List<string>(2);

            RequirementsList.Add("License type");
            RequirementsList.Add("Engine volume");
            RequirementsList.Add("Engine type");

            return RequirementsList;
        }

        public override void BuildVehicle(in List<string> i_ListOfAnswers)
        {
            const bool V_Valid = true;

            if (Enum.TryParse(i_ListOfAnswers[0], out m_LicenseType) == !V_Valid)
            {
                throw new Exception();
            }

            if (int.TryParse(i_ListOfAnswers[1], out m_MotorVolume) == !V_Valid)
            {
                throw new Exception();
            }

            if (i_ListOfAnswers[2].ToLower() == "electric")
            {
                m_MotorType = new ElectricMotor(MotorType.eEnergyType.Electric);
            }
            else if (i_ListOfAnswers[2].ToLower() == "fuel")
            {
                m_MotorType = new GasMotor(MotorType.eEnergyType.Octan98);
            }
            else
            {
                throw new ArgumentException();
            }


        }
    }
}
