using System;
using System.Collections.Generic;
using System.Text;
using PhoneBookApp.Enums;

namespace PhoneBookApp.Entities
{
    class Call
    {
        public DateTime TimeOfCall;
        public int Duration;
        public  Enums.Enums.Status CallStatus { get; set; }
        public Call AddValue(DateTime TimeOfCall, Enums.Enums.Status callStatus,int duration)
        {
            this.TimeOfCall = TimeOfCall;
            Duration = duration;
            CallStatus = callStatus;
            return this;
        }

        public override string ToString() => $"Vrijeme poziva: {TimeOfCall} \tTrajanje: {Duration}s     -     {CallStatus}";
    }
}
