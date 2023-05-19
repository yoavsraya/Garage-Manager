using System.Collections.Generic;
using System;

namespace Ex03.GarageLogic
{
    class GarageManeger
    {
        private Dictionary<ClientInfo.eClientStatus, ClientInfo> m_clients;

        private ClientInfo FindClientByPlateNumber(in string i_plateNumber, out bool io_found)
        {
            ClientInfo client = null;
            io_found = false;

            foreach (KeyValuePair<ClientInfo.eClientStatus, ClientInfo> vehicle in m_clients)
            {
                if (i_plateNumber.Equals(vehicle.Value.getVehiclePlateNumber()))
                {
                    io_found = true;
                    client = vehicle.Value;
                    break;
                }
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

            foreach (KeyValuePair<ClientInfo.eClientStatus, ClientInfo> vehicle in m_clients)
            {
                if (vehicle.Key == clientStatus)
                {
                    filteredPlates.Add(vehicle.Value.getVehiclePlateNumber());
                }
            }

            return filteredPlates;
        }

        public bool UpdateClientStatus(in string i_plateNumber, in string i_newStatus)
        {

            if(Enum.TryParse(i_newStatus, out ClientInfo.eClientStatus newClientStatus) == false) 
            {
                throw new Exception();
            }

            ClientInfo client = FindClientByPlateNumber(i_plateNumber, out bool v_Found);
            
            if (client != null)
            {
                client.clientStatus = newClientStatus;
            }

            return v_Found;
        }

        public bool FillWheelsToMax(string i_plateNumber)
        {
            ClientInfo client = FindClientByPlateNumber(i_plateNumber, out bool found);

            if(client != null) 
            {
                client.FillWheelsAirToMax();
            }

            return found;
        }

        public bool FillEnergyInVehicle(in string i_plateNumber, in string i_energyType, in string i_amoutOfEnergyToFill) 
        {
            if(Enum.TryParse(i_energyType, out MotorType.eEnergyType energyType) == false) 
            {
                throw new Exception();
            }
            
            ClientInfo client = FindClientByPlateNumber(i_plateNumber, out bool found);

            if (client != null)
            {
                client.FillEnergyInVehicle(float.Parse(i_amoutOfEnergyToFill), energyType);
            }

            return found;
        }

        public List<string> getVehicleInfo(in string i_plateNumber) 
        {
            List<string> vehicleInfo = null;

            ClientInfo client = FindClientByPlateNumber(i_plateNumber, out bool found);

            if(client != null)
            {
                vehicleInfo = client.getInfo();
            }

            return vehicleInfo;
        }
    }
}
