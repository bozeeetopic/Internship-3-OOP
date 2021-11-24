using System;
using System.Collections.Generic;
using System.Text;
using PhoneBookApp.Enums;

namespace PhoneBookApp.Entities
{
    class Call
    {
        public DateTime _TimeOfCall;
        public int _Duration;
        public  Enums.Enums.Status _CallStatus { get; set; }
        public Call AddValue(DateTime TimeOfCall, Enums.Enums.Status CallStatus,int duration)
        {
            _TimeOfCall = TimeOfCall;
            _Duration = duration;
            _CallStatus = CallStatus;
            return this;
        }

        public override string ToString() => $"Call start: {_TimeOfCall} \tDuration: {_Duration}s     -     {_CallStatus}";
    }
}
