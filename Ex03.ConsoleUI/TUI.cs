using System;
using System.Collections.Generic;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    class TUI
    {
        private GarageManeger m_myGarage = new GarageManeger();
        private CreatingObject factory = new CreatingObject();

        public void runGarage()
        {
            Console.WriteLine("Wellcome to the best garage in Jaffa!!");
            Console.WriteLine("If you'r here you probably want to put your cars in our hands");
            Console.WriteLine("So first of all...");

            Console.WriteLine("Please give us your vehicle plate number:");
            string plateNumber = Console.ReadLine();
            Console.WriteLine("Also your vehicle model:");
            string vehicleModel = Console.ReadLine();
            Console.WriteLine("Choose your vehicle from the option below... please write correct");
            printVehicleOptions();
            string vehicleType = Console.ReadLine();

            factory.createNewVehicle(plateNumber, vehicleModel, vehicleType, m_myGarage);
            Console.WriteLine(string.Format($"Your vehicle has been sign to our garage... we need a few more details about your {vehicleType}"));
            printListOfString(factory.myVehicleRequirements(m_myGarage));

            Console.WriteLine("Please Press Enter To Exit...");
            Console.ReadLine();
        }

        private void printListOfString(List<string> list) 
        {
            foreach (string str in list)
            {
                Console.WriteLine(str);
            }
        }

        private void printVehicleOptions() 
        {
            List<string> vehicleOptions = new List<string>();
            vehicleOptions.Add("car");
            vehicleOptions.Add("motorbike");
            vehicleOptions.Add("track");

            printListOfString(vehicleOptions);
        }

    }
}
