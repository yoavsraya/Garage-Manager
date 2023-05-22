using System.Collections.Generic;
using System;

namespace Ex03.GarageLogic
{
    public class GarageManeger
    {
        private Dictionary<ClientInfo.eClientStatus, ClientInfo> m_clients = new Dictionary<ClientInfo.eClientStatus, ClientInfo>();
        const bool k_Found = true;

        public bool isVehicleExist(in string i_plateNumber) 
        {
            bool found = !k_Found;

            foreach(KeyValuePair<ClientInfo.eClientStatus, ClientInfo> vehicle in m_clients) 
            {
                if(vehicle.Value.getVehiclePlateNumber() == i_plateNumber) 
                {
                    found = k_Found;
                }
            }

            return found;
        }

        private ClientInfo FindClientByPlateNumber(in string i_plateNumber)
        {
            ClientInfo client = null;
            bool found = !k_Found;

            foreach (KeyValuePair<ClientInfo.eClientStatus, ClientInfo> vehicle in m_clients)
            {
                if (i_plateNumber.Equals(vehicle.Value.getVehiclePlateNumber()))
                {
                    found = k_Found;
                    client = vehicle.Value;
                    break;
                }
            }

            if (found != k_Found)
            {
                throw new ArgumentException("plate number is not exist in the garage");
            }
            return client;
        }
        
        public List<string> ReturnListOfPlatesByFilter(in string i_condition)
        {
            List<string> filteredPlates = new List<string>();

            if (Enum.TryParse(i_condition, out ClientInfo.eClientStatus clientStatus) == false)
            {
                throw new Exception();
            }

            if(clientStatus == ClientInfo.eClientStatus.All)
            {
                foreach (KeyValuePair<ClientInfo.eClientStatus, ClientInfo> vehicle in m_clients)
                {
                    filteredPlates.Add(vehicle.Value.getVehiclePlateNumber());
                }
            }
            else
            {
                foreach (KeyValuePair<ClientInfo.eClientStatus, ClientInfo> vehicle in m_clients)
                {
                    if (vehicle.Key == clientStatus)
                    {
                        filteredPlates.Add(vehicle.Value.getVehiclePlateNumber());
                    }
                }
            }

            return filteredPlates;
        }

        public void UpdateClientStatus(in string i_plateNumber, in string i_newStatus)
        {
            if(Enum.TryParse(i_newStatus, out ClientInfo.eClientStatus newClientStatus) == false) 
            {
                throw new ArgumentException("status is not one of the status options!");
            }

            try
            {
                ClientInfo client = FindClientByPlateNumber(i_plateNumber);
                client.clientStatus = newClientStatus;
            }
            catch (Exception e)
            {
                throw e;
            }
      
        }

        public void FillWheelsToMax(string i_plateNumber)
        {
            try 
            {
                ClientInfo client = FindClientByPlateNumber(i_plateNumber);
                client.FillWheelsAirToMax();
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public void FillEnergyInVehicle(in string i_plateNumber, in string i_energyType, in string i_amoutOfEnergyToFill) 
        {
           MotorType.eEnergyType energyType = (MotorType.eEnergyType)Enum.Parse(typeof(MotorType.eEnergyType), i_energyType);
            try
            {
                ClientInfo client = FindClientByPlateNumber(i_plateNumber);
                client.FillEnergyInVehicle(float.Parse(i_amoutOfEnergyToFill), energyType);
            }
            catch(Exception e)
            {
                throw e;
            }

        }

        public List<string> getVehicleInfo(in string i_plateNumber) 
        {
            List<string> vehicleInfo;
            try
            {
                ClientInfo client = FindClientByPlateNumber(i_plateNumber);
                vehicleInfo = client.getInfo();
            }
            catch(Exception e)
            {
                throw e;
            }

            return vehicleInfo;
        }

        public List<string> getVehicleRequirement(vehicle vehicle) 
        {
            return vehicle.RequirementsList();
        }

        public List<string> getClientRequirement() 
        {
            List<string> requirementList = new List<string>(2);
            requirementList.Add("Owner Name");
            requirementList.Add("Owner Phone Number");
            return requirementList;
        }

        public void detailsToAddClient(vehicle vehicle, in List<string> i_vehicleDetailsList, in List<string> i_clientDetailsList) 
        {
            const int v_Name = 0, v_Number = 1; 
            vehicle.BuildVehicle(i_vehicleDetailsList);
            ClientInfo newClient = new ClientInfo(i_clientDetailsList[v_Name], i_clientDetailsList[v_Number], vehicle);
            m_clients.Add(newClient.clientStatus, newClient);
        }
        
        public List<string> GetGasTypeList()
        {
            List<string> energyTypes = new List<string>();

            foreach (MotorType.eEnergyType type in Enum.GetValues(typeof(MotorType.eEnergyType)))
            {
                string name = type.ToString();
                if (name != "Electric")
                {
                    energyTypes.Add(name);
                }
            }
            return energyTypes;
        }

        public List<string> GetVehicleOption()
        {
            List<string> vehicleTypes = new List<string>();
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly(); 
            Type[] types = assembly.GetTypes();
            string classString;


            foreach (Type type in types)
            {
                if (type.IsClass)
                {
                    classString = type.Name;
                    if (classString != "GarageManeger" && classString != "ClientInfo")
                    {
                        vehicleTypes.Add(classString);
                    }
                }
            }

            return vehicleTypes;
        }

        public bool isGasType(in string i_Type)
        {
            return i_Type != "Electric Engine";
        }
    }
}
