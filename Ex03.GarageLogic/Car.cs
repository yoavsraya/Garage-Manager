using System;
using System.Collections.Generic;
using static Ex03.GarageLogic.Car.eRequirements;

namespace Ex03.GarageLogic
{
    class Car : Vehicle
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
            currentEnergy,
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
            List<string> RequirementsList = new List<string>(m_NumOfRequirements);
            RequirementsList.Add("Number of doors (2-5)"); 
            RequirementsList.Add("Car color (Black ,White, Yellow, Red)");
            RequirementsList.Add("Engine type (electric, fuel)");
            RequirementsList.Add("Tier manufacturer");
            RequirementsList.Add($"Current tier pressure (max : {m_Wheels.MaxAirPressure})");
            RequirementsList.Add($"Power left (in hours for electric max: {5.2} / in liter for fuel max: {46})"); 
            

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
                throw new ValueOutOfRangeException("number of doors must be between 2-5");
            }

            if(Enum.TryParse(i_ListOfAnswers[((int)CarColor)], out m_CarColor) != k_Valid)
            {
                throw new FormatException("the color is not valid");
            }

            try
            {
                if (!float.TryParse(i_ListOfAnswers[(int)CurrentTierPressure], out float currTierPressure))
                {
                    throw new FormatException("tier pressure must be a number");
                }
                m_Wheels.UpdateWheelDetails(currTierPressure, i_ListOfAnswers[(int)TierManufacturer]);
                CreateEngine(i_ListOfAnswers[((int)EngineType)]);

                if (!float.TryParse(i_ListOfAnswers[(int)currentEnergy], out float currEnergy))
                {
                    throw new FormatException("energy must be a number");
                }
                updateEnergyDetails(currEnergy);
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

        protected override void updateEnergyDetails(in float i_currentEnergy)
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

            m_MotorType.currentEnergy = i_currentEnergy;
            EnergyMeterPercent = m_MotorType.calculateMeterPercent();
        }

    }
}
