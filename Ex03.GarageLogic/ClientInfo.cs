using System.Collections.Generic;
using System;

namespace Ex03.GarageLogic
{
    class ClientInfo
    {
        public enum eClientStatus
        {
            InProgress = 0, 
            Fixed,
            Paid,
            All,
        }
        private Vehicle m_vehicle;
        private string m_OwnerName;
        private string m_OwnerPhoneNumber;
        private eClientStatus m_clientStatus = eClientStatus.InProgress;

        public ClientInfo(string i_ownerName, string i_ownerPhoneNumber, Vehicle vehicle)
        {
            m_OwnerName = i_ownerName;
            m_OwnerPhoneNumber = i_ownerPhoneNumber;
            m_vehicle = vehicle;
            m_clientStatus = eClientStatus.InProgress;
        }

        public string OwnerName 
        {
            set
            {
                m_OwnerName = value;
            }
        }
        public string OwnerPhoneNumber 
        {
            set
            {
                m_OwnerPhoneNumber = value;
            }
        }

        public eClientStatus clientStatus 
        {
            get 
            {
                return m_clientStatus;
            }
            set 
            {
                m_clientStatus = value;
            }
        }

        private string fromEnumToString(eClientStatus i_clientStatus)
        {
            string statusInString = default;

            switch(i_clientStatus)
            {
                case eClientStatus.Fixed:
                    statusInString = "Fixed";
                    break;
                case eClientStatus.InProgress:
                    statusInString = "In Progress";
                    break;
                case eClientStatus.Paid:
                    statusInString = "Paid";
                    break;
            }

            return statusInString;
        }

        public string GetVehiclePlateNumber() 
        {
            return m_vehicle.LicensePlate;
        }

        public void FillWheelsAirToMax() 
        {
            m_vehicle.FillWheelsAirToMax();
        }

        public void FillEnergyInVehicle(in float i_energyToAdd, in MotorType.eEnergyType i_energyType) 
        {
            m_vehicle.ReFillVehicle(i_energyToAdd, i_energyType);
        }

        public List<string> GetInfo() 
        {
            List<string> vehicleInfo = m_vehicle.VehicleDetails();
            vehicleInfo.Add("Owner Name: " + m_OwnerName);
            vehicleInfo.Add("Owner Phone Number: " + m_OwnerPhoneNumber);
            vehicleInfo.Add("Client Status: " + fromEnumToString(m_clientStatus));

            return vehicleInfo;
        }
    }
}
