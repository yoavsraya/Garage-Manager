using System.Collections.Generic;
using System;

namespace Ex03.GarageLogic
{
    public class GarageManager
    {
        private Dictionary<ClientInfo.eClientStatus, List<ClientInfo>> m_clients = new Dictionary<ClientInfo.eClientStatus, List<ClientInfo>>();
        const bool k_Found = true;
        
        public bool isVehicleExist(in string i_PlateNumber) 
        {
            bool found = !k_Found;

            foreach (KeyValuePair<ClientInfo.eClientStatus, List<ClientInfo>> status in m_clients)
            {
                foreach (ClientInfo vehicle in status.Value)
                {
                    if (vehicle.GetVehiclePlateNumber() == i_PlateNumber)
                    {
                        found = k_Found;
                    }
                }
            }

            return found;
        }

        private ClientInfo FindClientByPlateNumber(in string i_PlateNumber)
        {
            ClientInfo client = null;
            bool found = !k_Found;

            foreach (KeyValuePair<ClientInfo.eClientStatus,List<ClientInfo>> status in m_clients)
            {
                foreach(ClientInfo tmpClient in status.Value) 
                {
                    if (i_PlateNumber.Equals(tmpClient.GetVehiclePlateNumber()))
                    {
                        found = k_Found;
                        client = tmpClient;
                        break;
                    }
                }
            }

            if (found != k_Found)
            {
                throw new ArgumentException("plate number is not exist in the garage");
            }
            return client;
        }
        
        public List<string> ReturnListOfPlatesByFilter(in string i_Condition)
        {
            List<string> filteredPlates = new List<string>();

            if (Enum.TryParse(i_Condition, out ClientInfo.eClientStatus clientStatus) == false)
            {
                throw new Exception("status is not valid!");
            }

            if(clientStatus == ClientInfo.eClientStatus.All)
            {
                foreach (KeyValuePair<ClientInfo.eClientStatus, List<ClientInfo>> status in m_clients)
                {
                    foreach(ClientInfo client in status.Value) 
                    {
                        filteredPlates.Add(client.GetVehiclePlateNumber());
                    }
                }
            }
            else
            {
                foreach (KeyValuePair<ClientInfo.eClientStatus, List<ClientInfo>> status in m_clients)
                {
                    if (status.Key == clientStatus)
                    {
                        foreach(ClientInfo client in status.Value) 
                        {
                            filteredPlates.Add(client.GetVehiclePlateNumber());
                        }

                        break;
                    }
                }
            }

            return filteredPlates;
        }

        public void UpdateClientStatus(in string i_PlateNumber, in string i_NewStatus)
        {
            if(Enum.TryParse(i_NewStatus, out ClientInfo.eClientStatus newClientStatus) == false) 
            {
                throw new ArgumentException("status is not one of the status options!");
            }

            try
            {
                ClientInfo client = FindClientByPlateNumber(i_PlateNumber);
                client.clientStatus = newClientStatus;
                updateListOfClients(client);
            }
            catch (Exception e)
            {
                throw e;
            }
      
        }

        private void updateListOfClients(ClientInfo i_Client) 
        {
            foreach(KeyValuePair<ClientInfo.eClientStatus, List<ClientInfo>> status in m_clients) 
            {
                status.Value.Remove(i_Client);
            }

            if(m_clients.ContainsKey(i_Client.clientStatus) == false) 
            {
                List<ClientInfo> listOfClientByStatus = new List<ClientInfo>();
                listOfClientByStatus.Add(i_Client);
                m_clients.Add(i_Client.clientStatus, listOfClientByStatus);
            }
            else 
            {
                m_clients[i_Client.clientStatus].Add(i_Client);
            }
        }

        public void FillWheelsToMax(string i_PlateNumber)
        {
            try 
            {
                ClientInfo client = FindClientByPlateNumber(i_PlateNumber);
                client.FillWheelsAirToMax();
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public void FillEnergyInVehicle(in string i_PlateNumber, in string i_EnergyType, in string i_AmountOfEnergyToFill) 
        {
            if (!Enum.TryParse(i_EnergyType, out MotorType.eEnergyType energyType))
            {
                throw new ArgumentException("energy type is not valid!");
            }
            try
            {
                ClientInfo client = FindClientByPlateNumber(i_PlateNumber);
                if (float.TryParse(i_AmountOfEnergyToFill, out float floatFuel))
                {
                    client.FillEnergyInVehicle(floatFuel, energyType);
                }
                else
                {
                    throw new ArgumentException("amount of energy must be a number");
                }
            }
            catch(Exception e)
            {
                throw e;
            }

        }

        public List<string> getVehicleInfo(in string i_PlateNumber) 
        {
            List<string> vehicleInfo;
            try
            {
                ClientInfo client = FindClientByPlateNumber(i_PlateNumber);
                vehicleInfo = client.GetInfo();
            }
            catch(Exception e)
            {
                throw e;
            }

            return vehicleInfo;
        }

        public List<string> getVehicleRequirement(in Vehicle i_Vehicle) 
        {
            if (i_Vehicle == null)
            {
                throw new ArgumentNullException();
            }
            return i_Vehicle.RequirementsList();
        }

        public List<string> getClientRequirement() 
        {
            List<string> requirementList = new List<string>(2);
            requirementList.Add("Owner Name");
            requirementList.Add("Owner Phone Number");
            return requirementList;
        }

        public void detailsToAddClient(Vehicle i_Vehicle, in List<string> i_VehicleDetailsList, in List<string> i_ClientDetailsList)
        {
            const int v_Name = 0, v_Number = 1;
            i_Vehicle.BuildVehicle(i_VehicleDetailsList);
            ClientInfo newClient = new ClientInfo(i_ClientDetailsList[v_Name], i_ClientDetailsList[v_Number], i_Vehicle);
            if(m_clients.ContainsKey(newClient.clientStatus) == false)
            {
                List<ClientInfo> listOfClientsByStatus = new List<ClientInfo>();
                listOfClientsByStatus.Add(newClient);
                m_clients.Add(newClient.clientStatus, listOfClientsByStatus);
            }
            else 
            {
                m_clients[newClient.clientStatus].Add(newClient);
            }
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

        public List<string> GetVehicleOptions()
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
                    if (typeof(Vehicle).IsAssignableFrom(type) && type != typeof(Vehicle))
                    {
                        vehicleTypes.Add(classString);
                    }
                }
            }

            return vehicleTypes;
        }

        public void isGasType(in string i_Type)
        {
            if (i_Type == "Electric Engine")
            {
                throw new ArgumentException("this is not a valid fuel type");
            }
        }
    }
}
