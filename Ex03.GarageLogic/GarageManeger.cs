using System.Collections.Generic;
using System;

namespace Ex03.GarageLogic
{
    public class GarageManager
    {
        private Dictionary<ClientInfo.eClientStatus, List<ClientInfo>> m_clients = new Dictionary<ClientInfo.eClientStatus, List<ClientInfo>>();
        const bool k_Found = true;
        
        public bool isVehicleExist(in string i_plateNumber) 
        {
            bool found = !k_Found;

            foreach (KeyValuePair<ClientInfo.eClientStatus, List<ClientInfo>> status in m_clients)
            {
                foreach (ClientInfo vehicle in status.Value)
                {
                    if (vehicle.GetVehiclePlateNumber() == i_plateNumber)
                    {
                        found = k_Found;
                    }
                }
            }

            return found;
        }

        private ClientInfo FindClientByPlateNumber(in string i_plateNumber)
        {
            ClientInfo client = null;
            bool found = !k_Found;

            foreach (KeyValuePair<ClientInfo.eClientStatus,List<ClientInfo>> status in m_clients)
            {
                foreach(ClientInfo tmpClient in status.Value) 
                {
                    if (i_plateNumber.Equals(tmpClient.GetVehiclePlateNumber()))
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
        
        public List<string> ReturnListOfPlatesByFilter(in string i_condition)
        {
            List<string> filteredPlates = new List<string>();

            if (Enum.TryParse(i_condition, out ClientInfo.eClientStatus clientStatus) == false)
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
                updateListOfClients(client);
            }
            catch (Exception e)
            {
                throw e;
            }
      
        }

        private void updateListOfClients(ClientInfo i_client) 
        {
            foreach(KeyValuePair<ClientInfo.eClientStatus, List<ClientInfo>> status in m_clients) 
            {
                status.Value.Remove(i_client);
            }

            if(m_clients.ContainsKey(i_client.clientStatus) == false) 
            {
                List<ClientInfo> listOfClientByStatus = new List<ClientInfo>();
                listOfClientByStatus.Add(i_client);
                m_clients.Add(i_client.clientStatus, listOfClientByStatus);
            }
            else 
            {
                m_clients[i_client.clientStatus].Add(i_client);
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

        public void FillEnergyInVehicle(in string i_plateNumber, in string i_energyType, in string i_amountOfEnergyToFill) 
        {
           MotorType.eEnergyType energyType = (MotorType.eEnergyType)Enum.Parse(typeof(MotorType.eEnergyType), i_energyType);
            try
            {
                ClientInfo client = FindClientByPlateNumber(i_plateNumber);
                client.FillEnergyInVehicle(float.Parse(i_amountOfEnergyToFill), energyType);
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
                vehicleInfo = client.GetInfo();
            }
            catch(Exception e)
            {
                throw e;
            }

            return vehicleInfo;
        }

        public List<string> getVehicleRequirement(in Vehicle i_vehicle) 
        {
            if (i_vehicle == null)
            {
                throw new ArgumentNullException();
            }
            return i_vehicle.RequirementsList();
        }

        public List<string> getClientRequirement() 
        {
            List<string> requirementList = new List<string>(2);
            requirementList.Add("Owner Name");
            requirementList.Add("Owner Phone Number");
            return requirementList;
        }

        public void detailsToAddClient(Vehicle vehicle, in List<string> i_vehicleDetailsList, in List<string> i_clientDetailsList)
        {
            const int v_Name = 0, v_Number = 1;
            vehicle.BuildVehicle(i_vehicleDetailsList);
            ClientInfo newClient = new ClientInfo(i_clientDetailsList[v_Name], i_clientDetailsList[v_Number], vehicle);
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
