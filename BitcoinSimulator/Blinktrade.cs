using System;
using System.Runtime.Serialization;

namespace BitcoinSimulator
{
    [DataContract]
    public class Blinktrade
    {
        [DataMember(Name = "buy")]
        public double Buy { get; set; }
    }
}
