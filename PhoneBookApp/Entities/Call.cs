﻿using System;

namespace PhoneBookApp.Entities
{
    class Call
    {
        public DateTime TimeOfCall;
        public int Duration;
        public  Enums.Status CallStatus { get; set; }
        public Call AddValue(DateTime TimeOfCall, Enums.Status callStatus,int duration)
        {
            this.TimeOfCall = TimeOfCall;
            Duration = duration;
            CallStatus = callStatus;
            return this;
        }

        public override string ToString() => $"Vrijeme poziva: {TimeOfCall} \tTrajanje: {Duration}s     -     {CallStatus}";
    }
}
