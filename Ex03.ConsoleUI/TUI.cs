using System;
using System.Collections.Generic;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    class TUI
    {
        private GarageManeger m_myGarage = new GarageManeger();
        private CreatingObject factory = new CreatingObject();
        const bool k_Deploy = true;

        public void runGarage()
        {
            wellcoming();
            bool firstCarDeploy = !k_Deploy;

            while(firstCarDeploy == !k_Deploy)
            {
                try 
                {
                    putVehicleInGarage();
                    firstCarDeploy = k_Deploy;
                }
                catch(Exception e) 
                {
                    Console.WriteLine(e.Message);
                }
            }

            eChoiceFromMenu eChoice = eChoiceFromMenu.notChosen;

            while (eChoice != eChoiceFromMenu.exit) 
            {

                eChoice = getUserChoiceFromMenu();

                try
                {
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
                        case eChoiceFromMenu.fillGasToMax:
                            fillGasToMax();
                            break;
                        case eChoiceFromMenu.fillElectricToMax:
                            fillElectricToMax();
                            break;
                        case eChoiceFromMenu.getFullDetails:
                            getFullDetailsOnVehicle();
                            break;
                        case eChoiceFromMenu.exit:
                            break;
                        default:
                            eChoice = eChoiceFromMenu.notChosen;
                            Console.WriteLine("Invalid choice... try again");
                            break;
                    }
                }
                catch(Exception e) 
                {
                    Console.WriteLine(e.Message);
                }
            }

            Console.WriteLine("Thank you for visiting our garage!");
            Console.WriteLine("Please Press Enter To Exit...");
            Console.ReadLine();
        }

        public enum eChoiceFromMenu
        {
            notChosen = 0,
            enterNewCar = 1,
            getListByFilter = 2,
            changeVehicleCondition = 3,
            fillAirToMax = 4,
            fillGasToMax = 5,
            fillElectricToMax = 6,
            getFullDetails = 7,
            exit = 8,

        }

        private void getFullDetailsOnVehicle() 
        {
            Console.WriteLine("Please enter plate number:");
            string plateNumberToGetDetails = Console.ReadLine();
            printListOfString(m_myGarage.getVehicleInfo(plateNumberToGetDetails));
        }

        private void fillElectricToMax()
        {
            Console.WriteLine("Please enter plate number:");
            string plateNumberToFillElectric = Console.ReadLine();

            Console.WriteLine("Please enter number of hours to fill:");
            string numberOfHoursToFill = Console.ReadLine();

            m_myGarage.FillEnergyInVehicle(plateNumberToFillElectric, "Electric", numberOfHoursToFill);
        }

        private void fillGasToMax() 
        {
            Console.WriteLine("Please enter plate number:");
            string plateNumberToFillGas = Console.ReadLine();
            Console.WriteLine("Please enter gas type from the option below:");
            printListOfString(m_myGarage.GetGasTypeList());
            string gasType = Console.ReadLine();
            Console.WriteLine("Please enter number of liters to fill:");
            string numberOfLitersToFill = Console.ReadLine();

            m_myGarage.FillEnergyInVehicle(plateNumberToFillGas, gasType, numberOfLitersToFill);
        }

        private void fillAirToMax()
        {
            Console.WriteLine("Please enter plate number:");
            string plateNumberToFillAir = Console.ReadLine();
            m_myGarage.FillWheelsToMax(plateNumberToFillAir);
        }

        private void changeVehicleCondition()
        {
            Console.WriteLine("Please enter the plate number of the vehicle you want to change his condition");
            string plateNumber = Console.ReadLine();
            Console.WriteLine("Please enter the new condition you wish(Paid, InProgress, Fixed)");
            string newCondition = Console.ReadLine();

            m_myGarage.UpdateClientStatus(plateNumber, newCondition);
        }

        private void getListOfVehiclesByFilter() 
        {
            Console.WriteLine("Please enter the condition of vehicles you want the list to include, the option are: All, Paid, InProgress, Fixed.");
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
7. Get full details of car from the garage by plate number.
8. If you're not interested you can leave we're won't be offended"));

            eChoiceFromMenu eChoice;
            while (Enum.TryParse(Console.ReadLine(),out eChoice))
            {
                Console.WriteLine("Wrong input try again...");
            }

            return eChoice;
        }

        private void wellcoming() 
        {
            Console.WriteLine("Welcome to the best garage in the UNIVERSE!!");
            Console.WriteLine("If you're here you probably want to put your cars in our hands");
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
            printListOfString(m_myGarage.GetVehicleOptions());
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
                Console.Write(str);
                if(str != list[list.Count - 1])
                {
                    Console.Write(", ");
                }
            }
            Console.WriteLine(" ");
        }

    }
}
