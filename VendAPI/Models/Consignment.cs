namespace VendAPI.Models
{
    using System;
    using System.Runtime.Serialization;

    [DataContract]
    public class Consignment
    {
        [DataMember(Name = "id")]
        public Guid Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "type")]
        public string Type { get; set; }

        [DataMember(Name = "date")]
        public string Date { get; set; }

        [DataMember(Name = "outlet_id")]
        public string OutletId { get; set; }

        [DataMember(Name = "source_outlet_id")]
        public string SourceOutletId { get; set; }

        [DataMember(Name = "supplier_id")]
        public string SupplierId { get; set; }

        [DataMember(Name = "status")]
        public string Status { get; set; }

        [DataMember(Name = "received_at")]
        public string ReceivedAt { get; set; }

        [DataMember(Name = "created_at")]
        public string CreatedAt { get; set; }

        [DataMember(Name = "updated_at")]
        public string UpdatedAt { get; set; }

        [DataMember(Name = "products")]
        public ConsignmentProduct[] Products { get; set; }
    }
}
