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

        private readonly eNumOfDoors r_numOfDoors;
        private readonly eCarColor r_CarColor;

        public Car(in string i_LicensePlate, in string i_ModelName, in eNumOfDoors i_NumOfDoors, in eCarColor i_Color)
            : base(i_LicensePlate, i_ModelName)
        {
            r_CarColor = i_Color;
            r_numOfDoors = i_NumOfDoors;
        }

        public override List<string> RequirementsList()
        {
           
        }

        public override void BuildVehicle(in List<string> i_ListOfAnswers)
        {
            
        }




    }
}
