using System.Collections.Generic;
using System;

namespace Ex03.GarageLogic
{
    
    public class CreatingObject
    {
        private Vehicle m_newVehicle;
        private enum eVehiclesType
        {
            Car = 0,
            MotorBike,
            Track,
        }
        private byte m_Counter = 0;

        public void CreateNewVehicle(in string i_PlateNumber, in string i_VehicleModel, in string i_VehicleType, in GarageManager i_Garage)
        {
            if(i_Garage.isVehicleExist(i_PlateNumber)) 
            {
                i_Garage.UpdateClientStatus(i_PlateNumber, "InProgress");
                throw new CarExistException();
            }
            else 
            {
                if(Enum.TryParse(i_VehicleType, out eVehiclesType vehiclesType) == false)
                {
                    throw new ArgumentException("this is not a possible vehicle");
                }
                m_Counter++;
                switch (vehiclesType) 
                {
                    case eVehiclesType.Car:
                        Car newCar = new Car(i_PlateNumber, i_VehicleModel);
                        m_newVehicle = newCar;
                        break;
                    case eVehiclesType.MotorBike:
                        MotorBike newMotorBike = new MotorBike(i_PlateNumber, i_VehicleModel);
                        m_newVehicle = newMotorBike;
                        break;
                    case eVehiclesType.Track:
                        Track newTrack = new Track(i_PlateNumber, i_VehicleModel);
                        m_newVehicle = newTrack;
                        break;
                }
            }
        }

        public List<string> MyVehicleRequirements(in GarageManager i_Garage)
        {
            return i_Garage.getVehicleRequirement(m_newVehicle);
        }
        
        public List<string> MyClientInfoRequirements(in GarageManager i_Garage)
        {
            return i_Garage.getClientRequirement();
        }

        public void CreateMyClientInfoCard(in GarageManager i_Garage, List<string> i_VehicleDetailsList, List<string> i_ClientDetailsList) 
        {
            i_Garage.detailsToAddClient(m_newVehicle, i_VehicleDetailsList, i_ClientDetailsList);
        }

        public bool IsGarageEmpty()
        {
            return m_Counter == 0;
        }
    }
}
