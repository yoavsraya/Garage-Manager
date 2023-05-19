using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    class Car : vehicle
    {
        public enum eNumOfDoors
        {
            Two =2,
            Three,
            Four,
            Five,
        }

        public enum eCarColor
        {
            Black,
            White,
            Yellow,
            Red,
        }

        private  eNumOfDoors m_numOfDoors;
        private  eCarColor m_CarColor;

        public Car(in string i_LicensePlate, in string i_ModelName)
            : base(i_LicensePlate, i_ModelName)
        {
            m_NumOfWheels = 4;
            m_MaxTirePressure = 33;
        }

        public override List<string> RequirementsList()
        {
            List<string> RequirementsList = new List<string>(5);

            RequirementsList.Add("number of doors"); //0
            RequirementsList.Add("car color"); // 1
            RequirementsList.Add("Engine type"); // 2
            RequirementsList.Add("tier manufacturer"); //3
            RequirementsList.Add("current tier pressure"); //4

            return RequirementsList;
        }

        public override void BuildVehicle(in List<string> i_ListOfAnswers)
        {
            if (Enum.TryParse(i_ListOfAnswers[0], out m_numOfDoors) != k_Valid)
            {
                throw new Exception();
            }

            if(Enum.TryParse(i_ListOfAnswers[1], out m_CarColor) != k_Valid)
            {
                throw new Exception();
            }

            try
            {
                CreateEngine(i_ListOfAnswers[2]);
            }
            catch (Exception e)
            {
                throw e;
            }

            UpdateWheelsInfo(i_ListOfAnswers[3], float.Parse(i_ListOfAnswers[3]), m_MaxTirePressure);
        }




    }
}
