using System.Collections.Generic;
using System;

namespace Ex03.GarageLogic
{
    
    public class CreatingObject
    {
        private vehicle m_newVehicle;
        private enum eVehiclesType
        {
            car = 0,
            motorbike,
            track,
        }

        public void createNewVehicle(in string i_plateNumber, in string i_vehicleModel, in string i_vehicleType, in GarageManeger i_garage)
        {
            if(i_garage.isVehicleExist(i_plateNumber) == true) 
            {
                i_garage.UpdateClientStatus(i_plateNumber, "inProgress");
            }
            else 
            {
                if(Enum.TryParse(i_vehicleType, out eVehiclesType vehiclesType) == false)
                {
                    throw new Exception();
                }

                switch (vehiclesType) 
                {
                    case eVehiclesType.car:
                        Car newCar = new Car(i_plateNumber, i_vehicleModel);
                        m_newVehicle = newCar;
                        break;
                    case eVehiclesType.motorbike:
                        MotorBike newMotorBike = new MotorBike(i_plateNumber, i_vehicleModel);
                        m_newVehicle = newMotorBike;
                        break;
                    case eVehiclesType.track:
                        Track newTrack = new Track(i_plateNumber, i_vehicleModel);
                        m_newVehicle = newTrack;
                        break;
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
