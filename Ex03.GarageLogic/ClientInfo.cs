using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    class ClientInfo
    {
        public enum eClientStatus
        {
            InProgress, 
            Fixed,
            Paid,
        }
        vehicle m_vehicle;
        string m_OwnerName;
        string m_OwnerPhoneNumber;
        eClientStatus m_clientStatus = eClientStatus.InProgress;


    }
}
