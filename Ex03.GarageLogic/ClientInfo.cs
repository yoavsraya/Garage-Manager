

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
        vehicle m_vehicle;
        string m_OwnerName;
        string m_OwnerPhoneNumber;
        eClientStatus m_clientStatus = eClientStatus.InProgress;

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

        public string getViheclePlateNumber() 
        {
            return m_vehicle.LicensePlate;
        }

        public void FillWheelsAirToMax() 
        {
            m_vehicle.FillWheelsAirToMax();
        }
    }
}
