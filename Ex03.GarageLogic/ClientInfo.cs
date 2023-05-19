﻿using System.Collections.Generic;
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
        }
        private vehicle m_vehicle;
        private string m_OwnerName;
        private string m_OwnerPhoneNumber;
        private eClientStatus m_clientStatus = eClientStatus.InProgress;

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

        public string getVehiclePlateNumber() 
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

        public List<string> getInfo() 
        {
            List<string> vehicleInfo = m_vehicle.getDetails();
            vehicleInfo.Add("Owner Name: " + m_OwnerName);
            vehicleInfo.Add("Owner Phone Number: " + m_OwnerPhoneNumber);
            vehicleInfo.Add("Client Status: " + fromEnumToString(m_clientStatus));

            return vehicleInfo;
        }
    }
}
