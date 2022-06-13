using System;
using System.Runtime.Serialization;

namespace Bars.Entities.Dto
{
    [DataContract]
    public class Contract
    {
        [DataMember]
        public int Number { get; set; }
        [DataMember]
        public DateTime Date { get; set; }
        [DataMember]
        public DateTime LastModifiedDate { get; set; }
        [DataMember]
        public bool IsActual { get; set; }
    }
}