﻿using System.Collections.Generic;
using System;

namespace Ex03.GarageLogic
{
    
    public class CreatingObject
    {
        private enum eVehiclesType
        {
            Car = 0,
            MotorBike,
            Track,
        }

        private Vehicle m_NewVehicle;
        private byte m_Counter = 0;

        public void CreateNewVehicle(in string i_PlateNumber, in string i_VehicleModel, in string i_VehicleType, in GarageManager i_Garage)
        {
            if(i_Garage.IsVehicleExist(i_PlateNumber)) 
            {
                i_Garage.UpdateClientStatus(i_PlateNumber, "InProgress");
                throw new CarExistException();
            }
            else 
            {
                if (!Enum.TryParse(i_VehicleType, out eVehiclesType vehiclesType))
                {
                    throw new FormatException("this is not option vehicle");
                }

                m_Counter++;
                switch (vehiclesType) 
                {
                    case eVehiclesType.Car:
                        Car newCar = new Car(i_PlateNumber, i_VehicleModel);
                        m_NewVehicle = newCar;
                        break;
                    case eVehiclesType.MotorBike:
                        MotorBike newMotorBike = new MotorBike(i_PlateNumber, i_VehicleModel);
                        m_NewVehicle = newMotorBike;
                        break;
                    case eVehiclesType.Track:
                        Track newTrack = new Track(i_PlateNumber, i_VehicleModel);
                        m_NewVehicle = newTrack;
                        break;
                }
            }
        }

        public List<string> MyVehicleRequirements(in GarageManager i_Garage)
        {
            return i_Garage.GetVehicleRequirement(m_NewVehicle);
        }
        
        public List<string> MyClientInfoRequirements(in GarageManager i_Garage)
        {
            return i_Garage.GetClientRequirement();
        }

        public void CreateMyClientInfoCard(in GarageManager i_Garage, in List<string> i_VehicleDetailsList, in List<string> i_ClientDetailsList) 
        {
            i_Garage.DetailsToAddClient(m_NewVehicle, i_VehicleDetailsList, i_ClientDetailsList);
        }

        public bool IsGarageEmpty()
        {
            return m_Counter == 0;
        }
    }
}
