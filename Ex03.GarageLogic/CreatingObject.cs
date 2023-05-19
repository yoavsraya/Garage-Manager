using System.Collections.Generic;
using System;

namespace Ex03.GarageLogic
{
    
    public class CreatingObject
    {
        private vehicle m_newVehicle;

        public void createNewVehicle(in string i_plateNumber, in string i_vehicleModel, in string i_vehicleType, GarageManeger i_garage)
        {
            if(i_garage.isVehicleExist(i_plateNumber) == true) 
            {
                i_garage.UpdateClientStatus(i_plateNumber, "in progress");
            }
            else 
            {
                switch (i_vehicleType) 
                {
                    case "car":
                        Car newCar = new Car(i_plateNumber, i_vehicleModel);
                        m_newVehicle = newCar;
                        break;
                    case "motorbike":
                        MotorBike newMotorBike = new MotorBike(i_plateNumber, i_vehicleModel);
                        m_newVehicle = newMotorBike;
                        break;
                    case "track":
                        Track newTrack = new Track(i_plateNumber, i_vehicleModel);
                        m_newVehicle = newTrack;
                        break;
                    default:
                        throw new Exception();
                }
            }
        }

        public List<string> myVehicleRequirements(GarageManeger garage)
        {
            return garage.getVehicleRequirement(m_newVehicle);
        }
        
        public List<string> myClientInfoRequirements(GarageManeger garage)
        {
            return garage.getClientRequirement();
        }

        public void createMyClientInfoCard(GarageManeger garage, List<string> i_vehicleDetailsList, List<string> i_clientDetailsList) 
        {
            garage.detailsToAddClient(m_newVehicle, i_vehicleDetailsList, i_clientDetailsList);
        }
    }
}
