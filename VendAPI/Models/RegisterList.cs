namespace VendAPI.Models
{
    using System.Runtime.Serialization;

    [DataContract]
    public class RegisterList
    {
        [DataMember(Name = "registers")]
        public Register[] Registers { get; set; }
    }
}
