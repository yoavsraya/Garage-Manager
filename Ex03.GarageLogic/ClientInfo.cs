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

        private readonly Vehicle r_Vehicle;
        private readonly string r_OwnerName;
        private readonly string r_OwnerPhoneNumber;
        private eClientStatus m_ClientStatus;

        public ClientInfo(in string i_OwnerName, string i_OwnerPhoneNumber, in Vehicle i_Vehicle)
        {
            r_OwnerName = i_OwnerName;
            r_OwnerPhoneNumber = i_OwnerPhoneNumber;
            r_Vehicle = i_Vehicle;
            m_ClientStatus = eClientStatus.InProgress;
        }

        public eClientStatus clientStatus 
        {
            get 
            {
                return m_ClientStatus;
            }
            set 
            {
                m_ClientStatus = value;
            }
        }

        public string GetVehiclePlateNumber() 
        {
            return r_Vehicle.LicensePlate;
        }

        public void FillWheelsAirToMax() 
        {
            r_Vehicle.FillWheelsAirToMax();
        }

        public void FillEnergyInVehicle(in float i_EnergyToAdd, in MotorType.eEnergyType i_EnergyType) 
        {
            r_Vehicle.ReFillVehicle(i_EnergyToAdd, i_EnergyType);
        }

        public List<string> GetInfo()
        {
            List<string> vehicleInfo = r_Vehicle.VehicleDetails();
            vehicleInfo.Add($"Owner Name: {r_OwnerName}");
            vehicleInfo.Add($"Owner Phone Number: {r_OwnerPhoneNumber}");
            vehicleInfo.Add($"Client Status: {m_ClientStatus}");

            return vehicleInfo;
        }
    }
}
