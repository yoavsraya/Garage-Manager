using System;
using System.Collections.Generic;
using static Ex03.GarageLogic.Car.eRequirements;

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

        public enum eRequirements
        {
            NumOfDoors = 0,
            CarColor,
            EngineType,
            TierManufacturer,
            CurrentTierPressure,
        }

        private  eNumOfDoors m_numOfDoors;
        private  eCarColor m_CarColor;

        public Car(in string i_LicensePlate, in string i_ModelName)
            : base(i_LicensePlate, i_ModelName)
        {
            m_NumOfRequirements = Enum.GetNames(typeof(eRequirements)).Length;
            m_Wheels = new Wheels(5, 33);
        }

        public override List<string> RequirementsList()
        {
            List<string> RequirementsList = new List<string>(5);
            RequirementsList.Add("number of doors (2-5)"); 
            RequirementsList.Add("car color (Black ,White, Yellow, Red)");
            RequirementsList.Add("Engine type (electric, fuel)");
            RequirementsList.Add("tier manufacturer");
            RequirementsList.Add("current tier pressure"); 

            return RequirementsList;
        }

        public override void BuildVehicle(in List<string> i_ListOfAnswers)
        {
            if (i_ListOfAnswers.Count != m_NumOfRequirements)
            {
                throw new ArgumentException("answer list is not having the full amount of answers");
            }

            if (Enum.TryParse(i_ListOfAnswers[((int)NumOfDoors)], out m_numOfDoors) != k_Valid)
            {
                throw new ArgumentException("num of doors is not valid");
            }

            if(Enum.TryParse(i_ListOfAnswers[((int)CarColor)], out m_CarColor) != k_Valid)
            {
                throw new ArgumentException("the color is not valid");
            }

            try
            {
                m_Wheels.UpdateWheelDetails(float.Parse(i_ListOfAnswers[((int)CurrentTierPressure)]), i_ListOfAnswers[((int)TierManufacturer)]);
                CreateEngine(i_ListOfAnswers[((int)EngineType)]);
                updateMaxEnergy();
                
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public override List<string> VehicleDetails()
        {
            List<string> details = base.VehicleDetails();
            details.Add(string.Format("number of doors: {0}", m_numOfDoors));
            details.Add(string.Format("The car color: {0}", m_CarColor));

            return details;
        }

        protected override void updateMaxEnergy()
        {
            if (m_MotorType == null)
            {
                throw new NullReferenceException("engine has not set yet!");
            }

            else if (m_MotorType is GasMotor)
            {
                m_MotorType.maxEnergy = 46f;
            }
            else // is electric
            {
                m_MotorType.maxEnergy = 5.2f;
            }

        }
    }
}
