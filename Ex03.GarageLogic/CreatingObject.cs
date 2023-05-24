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

        public void CreateNewVehicle(in string i_plateNumber, in string i_vehicleModel, in string i_vehicleType, in GarageManeger i_garage)
        {
            if(i_garage.isVehicleExist(i_plateNumber)) 
            {
                i_garage.UpdateClientStatus(i_plateNumber, "inProgress");
            }
            else 
            {
                if(Enum.TryParse(i_vehicleType, out eVehiclesType vehiclesType) == false)
                {
                    throw new ArgumentException("this is not a possible vehicle");
                }
                m_Counter++;
                switch (vehiclesType) 
                {
                    case eVehiclesType.Car:
                        Car newCar = new Car(i_plateNumber, i_vehicleModel);
                        m_newVehicle = newCar;
                        break;
                    case eVehiclesType.MotorBike:
                        MotorBike newMotorBike = new MotorBike(i_plateNumber, i_vehicleModel);
                        m_newVehicle = newMotorBike;
                        break;
                    case eVehiclesType.Track:
                        Track newTrack = new Track(i_plateNumber, i_vehicleModel);
                        m_newVehicle = newTrack;
                        break;
                }
            }
        }

        public List<string> MyVehicleRequirements(in GarageManeger i_garage)
        {
            return i_garage.getVehicleRequirement(m_newVehicle);
        }
        
        public List<string> MyClientInfoRequirements(in GarageManeger i_garage)
        {
            return i_garage.getClientRequirement();
        }

        public void CreateMyClientInfoCard(in GarageManeger i_garage, List<string> i_vehicleDetailsList, List<string> i_clientDetailsList) 
        {
            i_garage.detailsToAddClient(m_newVehicle, i_vehicleDetailsList, i_clientDetailsList);
        }

        public bool IsGarageEmpty()
        {
            return m_Counter == 0;
        }
    }
}
