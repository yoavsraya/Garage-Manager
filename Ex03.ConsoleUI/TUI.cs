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
            wellcoming();
            eChoiceFromMenu eChoice = eChoiceFromMenu.notChoicenYet;

            while (eChoice != eChoiceFromMenu.exit) 
            {
                putVehicleInGarage();

                eChoice = getUserChoiceFromMenu();

                switch (eChoice) 
                {
                    case eChoiceFromMenu.enterNewCar:
                        putVehicleInGarage();
                        break;
                    case eChoiceFromMenu.getListByFilter:
                        getListOfVehiclesByFilter();
                        break;
                    case eChoiceFromMenu.changeVehicleCondition:
                        changeVehicleCondition();
                        break;
                    case eChoiceFromMenu.fillAirToMax:
                        fillAirToMax();
                        break;


                }
            }
            

            //Console.WriteLine("Please Press Enter To Exit...");
            //Console.ReadLine();
        }

        public enum eChoiceFromMenu
        {
            notChoicenYet = 0,
            enterNewCar = 1,
            getListByFilter = 2,
            changeVehicleCondition = 3,
            fillAirToMax = 4,
            fillGasToMax = 5,
            fillElectricToMax = 6,
            getFullDetails = 7,
            exit = 8,

        }

        private void fillAirToMax()
        {
            Console.WriteLine("Please enter plate number");
            string plateNumberToFill = Console.ReadLine();
            m_myGarage.FillWheelsToMax(plateNumberToFill);
        }

        private void changeVehicleCondition()
        {
            Console.WriteLine("Please enter the plate number of the vehicle you want to change his condotion");
            string plateNumber = Console.ReadLine();
            Console.WriteLine("Please enter the new condition you wish(Paid, InProgress, Fixed)");
            string newCondition = Console.ReadLine();

            if(m_myGarage.UpdateClientStatus(plateNumber, newCondition) == false)
            {
                Console.WriteLine("The plate number you entered isn't exist here...");
            }
            else 
            {
                Console.WriteLine("Condition changed succesfully");
            }
        }

        private void getListOfVehiclesByFilter() 
        {
            Console.WriteLine("Please enter the condiotion of vehicles you want the list to include, the option are: All, Paid, InProgress, Fixed.");
            string condition = Console.ReadLine();
            printListOfString(m_myGarage.ReturnListOfPlatesByFilter(condition));
        }

        private eChoiceFromMenu getUserChoiceFromMenu()
        {
            Console.WriteLine(string.Format(@"We offer in our garage these services:
1. Enter a vehicle to the garage.
2. Get a list of the plates number of the vehicle in the garage by sort.
3. Change vehicle condition from the garage.
4. Fill air to maximum to vehicle from the garage.
5. Fill vehicle gas to max from the garage.
6. Fill vehicle electric to max from the garage.
7. Get full detatils of car from the garage by playe number.
8. If you'r not interested you can leave we're won't be offended"));

            eChoiceFromMenu eChoice;
            while (Enum.TryParse(Console.ReadLine(),out eChoice))
            {
                Console.WriteLine("Wrong input try again...");
            }

            return eChoice;
        }

        private void wellcoming() 
        {
            Console.WriteLine("Wellcome to the best garage in Jaffa!!");
            Console.WriteLine("If you'r here you probably want to put your cars in our hands");
            Console.WriteLine("So first of all...");
        }

        private void putVehicleInGarage()
        {
            putFlatVehicleInGarage();
            putDetailsOfFlatVehicleAndClient();
        }

        private void putDetailsOfFlatVehicleAndClient() 
        {
            Console.WriteLine("Your vehicle has been sign to our garage... we need a few more details about your vehicle");
            printListOfString(factory.myVehicleRequirements(m_myGarage));
            List<string> listOfRequirementsForVehicle = userInputForVehicleRequirements();
            List<string> listOfRequirementsForClientInfo = userInputForClientInfoRequirements();

            factory.createMyClientInfoCard(m_myGarage, listOfRequirementsForVehicle, listOfRequirementsForClientInfo);
        }

        private void putFlatVehicleInGarage() 
        {
            Console.WriteLine("Please give us your vehicle plate number:");
            string plateNumber = Console.ReadLine();
            Console.WriteLine("Also your vehicle model:");
            string vehicleModel = Console.ReadLine();
            Console.WriteLine("Choose your vehicle from the option below... please write correct");
            printVehicleOptions();
            string vehicleType = Console.ReadLine();
            factory.createNewVehicle(plateNumber, vehicleModel, vehicleType, m_myGarage);
        }

        private List<string> userInputForClientInfoRequirements() 
        {
            List<string> listOfClientInfoFromUser = new List<string>();
            Console.WriteLine("In addition we need this details about you:");
            printListOfString(factory.myClientInfoRequirements(m_myGarage));
            Console.WriteLine("Please enter the details in the same order as we ask for them!");

            for(int i = 0; i < factory.myClientInfoRequirements(m_myGarage).Count; i++)
            {
                listOfClientInfoFromUser.Add(Console.ReadLine());
            }

            return listOfClientInfoFromUser;
        }
        private List<string> userInputForVehicleRequirements()
        {
            List<string> listOfInputsFromUser = new List<string>();
            Console.WriteLine("Please Enter the details in the same order as we ask for them!");

            for(int i = 0; i < factory.myVehicleRequirements(m_myGarage).Count; i++) 
            {
                listOfInputsFromUser.Add(Console.ReadLine());
            }

            return listOfInputsFromUser;
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
