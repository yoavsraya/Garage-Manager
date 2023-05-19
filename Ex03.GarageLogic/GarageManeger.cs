using System.Collections.Generic;
using System;

namespace Ex03.GarageLogic
{
    class GarageManeger
    {
        private Dictionary<ClientInfo.eClientStatus, ClientInfo> m_clients;
        
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
                    filteredPlates.Add(vehicle.Value.getViheclePlateNumber());
                }
            }

            return filteredPlates;
        }

        public bool UpdateClientStatus(in string i_PlateNumber, in string i_NewStatus)
        {
            const bool v_Found = true;
            bool isPlateFound = !v_Found;

            if(Enum.TryParse(i_NewStatus, out ClientInfo.eClientStatus newClientStatus) == false) 
            {
                throw new Exception();
            }
            
            foreach (KeyValuePair<ClientInfo.eClientStatus, ClientInfo> vehicle in m_clients)
            {
                if (i_PlateNumber.Equals(vehicle.Value.getViheclePlateNumber())) 
                {
                    isPlateFound = v_Found;
                    vehicle.Value.clientStatus = newClientStatus;
                    break;
                }
            }

            return isPlateFound;
        }

        public bool FillWheelsToMax(string i_PlateNumber)
        {
            const bool v_Found = true;
            bool isPlateFound = !v_Found;

            foreach(KeyValuePair<ClientInfo.eClientStatus, ClientInfo> client in m_clients) 
            {
                if (client.Value.getViheclePlateNumber().Equals(i_PlateNumber)) 
                {
                    isPlateFound = true;
                    client.Value.FillWheelsAirToMax();
                    break;
                }
            }

            return isPlateFound;
        }


    }
}
