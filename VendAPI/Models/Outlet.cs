namespace VendAPI.Models
{
    using System;
    using System.Runtime.Serialization;

    [DataContract]
    public class Outlet
    {
        [DataMember(Name = "id")]
        public Guid Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "time_zone")]
        public string TimeZone { get; set; }
    }
}
