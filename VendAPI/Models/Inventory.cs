namespace VendAPI.Models
{
    using System;
    using System.Runtime.Serialization;

    [DataContract]
    public class Inventory
    {
        [DataMember(Name = "id")]
        public Guid Id { get; set; }

        [DataMember(Name = "outlet_id")]
        public Guid OutletId { get; set; }

        [DataMember(Name = "outlet_name")]
        public string OutletName { get; set; }

        [DataMember(Name = "count")]
        public decimal Count { get; set; }

        [DataMember(Name = "reorder_point")]
        public string ReorderPoint { get; set; }

        [DataMember(Name = "restock_level")]
        public string RestockLevel { get; set; }

        [DataMember(Name = "product_id")]
        public string ProductId { get; set; }

        [DataMember(Name = "product")]
        public Product Product { get; set; }

        [DataMember(Name = "outlet")]
        public Outlet Outlet { get; set; }
    }
}
