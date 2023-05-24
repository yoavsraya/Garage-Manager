using System;

namespace Ex03.GarageLogic
{
    public class CarExistException : Exception
    {
        public override string Message 
        {
            get 
            {
                return "The vehicle is already in the garage! the status changed to InProgress!";
            }
        }
    }
}
