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

            eChoiceFromMenu eChoice = eChoiceFromMenu.notChosen;
            int choice = 0;

            while (eChoice != eChoiceFromMenu.exit) 
            {

                choice = getUserChoiceFromMenu();
                eChoice = (eChoiceFromMenu)choice;
                if (factory.isGarageEmpty() && eChoice != eChoiceFromMenu.enterNewCar)
                {
                    Console.WriteLine("the garage is empty!");
                    continue;
                }

                try
                {
                    switch (eChoice) 
                    {
                        case (eChoiceFromMenu)1:
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
                            Console.WriteLine("Invalid choice. try again");
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
            Console.WriteLine("Electric fill to max successfully!");
        }

        private void fillGasToMax() 
        {
            Console.WriteLine("Please enter plate number:");
            string plateNumberToFillGas = Console.ReadLine();
            Console.WriteLine("Please enter gas type from the option below:");
            printListOfString(m_myGarage.GetGasTypeList());
            string gasType = Console.ReadLine();
            m_myGarage.isGasType(gasType);
            Console.WriteLine("Please enter number of liters to fill:");
            string numberOfLitersToFill = Console.ReadLine();

            m_myGarage.FillEnergyInVehicle(plateNumberToFillGas, gasType, numberOfLitersToFill);
            Console.WriteLine("Gas fill to max successfully!");
        }

        private void fillAirToMax()
        {
            Console.WriteLine("Please enter plate number:");
            string plateNumberToFillAir = Console.ReadLine();
            m_myGarage.FillWheelsToMax(plateNumberToFillAir);
            Console.WriteLine("Air fill to max successfully!");
        }

        private void changeVehicleCondition()
        {
            Console.WriteLine("Please enter the plate number of the vehicle you want to change his condition");
            string plateNumber = Console.ReadLine();
            Console.WriteLine("Please enter the new condition you wish(Paid, InProgress, Fixed)");
            string newCondition = Console.ReadLine();

            m_myGarage.UpdateClientStatus(plateNumber, newCondition);
            Console.WriteLine("Condition change successfully!");
        }

        private void getListOfVehiclesByFilter() 
        {
            Console.WriteLine("Please enter the condition of vehicles you want the list to include, the option are: All, Paid, InProgress, Fixed.");
            string condition = Console.ReadLine();
            printListOfString(m_myGarage.ReturnListOfPlatesByFilter(condition));
        }

        private int getUserChoiceFromMenu()
        {
            Console.WriteLine(string.Format(@"
We offer in our garage these services:
1. Enter a vehicle to the garage.
2. Get a list of the plates number of the vehicle in the garage by sort.
3. Change vehicle condition from the garage.
4. Fill air to maximum to vehicle from the garage.
5. Fill vehicle gas to max from the garage.
6. Fill vehicle electric to max from the garage.
7. Get full details of car from the garage by plate number.
8. If you're not interested you can leave we're won't be offended
"));

            int eChoice;
            while (int.TryParse(Console.ReadLine(),out eChoice) && eChoice < 1 || eChoice > Enum.GetNames(typeof(eChoiceFromMenu)).Length)
            {
                Console.WriteLine("Wrong input try again.");
            }

            return eChoice;
        }

        private void wellcoming() 
        {
            Console.WriteLine("Welcome to the best garage in the UNIVERSE!!");
            Console.WriteLine("If you're here you probably want to put your cars in our hands");
        }

        private void putVehicleInGarage()
        {

            bool firstCarDeploy = !k_Deploy;

            while (firstCarDeploy == !k_Deploy)
            {
                try
                {
                    putFlatVehicleInGarage();
                    firstCarDeploy = k_Deploy;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            firstCarDeploy = !k_Deploy;
            while (firstCarDeploy == !k_Deploy)
            {
                try
                {
                    putDetailsOfFlatVehicleAndClient();
                    firstCarDeploy = k_Deploy;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                Console.WriteLine("the car was added to the garage!");
            }
        }

        private void putDetailsOfFlatVehicleAndClient() 
        {
            Console.WriteLine(@"
We need a few more details about your vehicle, please enter by order of the printing:");
            printListOfString(factory.myVehicleRequirements(m_myGarage));
            List<string> listOfRequirementsForVehicle = new List<string>();
            List<string> listOfRequirementsForClientInfo = new List<string>();
            try 
            {
                listOfRequirementsForVehicle = userInputForVehicleRequirements();
            }
            catch(Exception e) 
            {
                throw e;
            }
            try
            {
                listOfRequirementsForClientInfo = userInputForClientInfoRequirements();
            }
            catch(Exception e) 
            {
                throw e;
            }

            factory.createMyClientInfoCard(m_myGarage, listOfRequirementsForVehicle, listOfRequirementsForClientInfo);
        }

        private void putFlatVehicleInGarage() 
        {
            Console.WriteLine(@"
Please give us your vehicle plate number:");
            string plateNumber = Console.ReadLine();
            Console.WriteLine("Also your vehicle model:");
            string vehicleModel = Console.ReadLine();
            Console.WriteLine(@"
Choose your vehicle from the option below. please mind Capital Letters:");
            printListOfString(m_myGarage.GetVehicleOptions());
            string vehicleType = Console.ReadLine();
            factory.createNewVehicle(plateNumber, vehicleModel, vehicleType, m_myGarage);
            Console.WriteLine(@"
Your vehicle has been sign to our garage!");
        }

        private List<string> userInputForClientInfoRequirements() 
        {
            List<string> listOfClientInfoFromUser = new List<string>();
            Console.WriteLine(@"
In addition we need this details about you:");
            printListOfString(factory.myClientInfoRequirements(m_myGarage));
            Console.WriteLine("Please enter the details in the same order printed");

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
                    Console.WriteLine(", ");
                }
            }
            Console.WriteLine(@"
");
        }

    }
}
