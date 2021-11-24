using System;
using System.Collections.Generic;
using System.Text;
using PhoneBookApp.Enums;

namespace PhoneBookApp.Entities
{
    public struct TimeStruct
    {
        public DateTime time;
        public int duration;
    }
    class Call
    {
        public TimeStruct _TimeOfCall;
        public  Enums.Enums.Status _CallStatus { get; set; }
        public Call AddValue(DateTime TimeOfCall, Enums.Enums.Status CallStatus,int duration)
        {
            _TimeOfCall.time = TimeOfCall;
            _TimeOfCall.duration = duration;
            _CallStatus = CallStatus;
            return this;
        }

        public override string ToString() => $"Call start: {_TimeOfCall} \tDuration: {_TimeOfCall}     -     {_CallStatus}";
    }
}
