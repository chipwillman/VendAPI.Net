namespace VendAPI.Models
{
    using System.Runtime.Serialization;

    [DataContract]
    public class ConsignmentWrapper
    {
        [DataMember(Name = "consignment")]
        public Consignment Consignment { get; set; }

        [DataMember(Name = "consignments")]
        public Consignment[] Consignments { get; set; }

        [DataMember(Name = "stock_movements")]
        public Consignment[] StockMovements { get; set; }
    }
}
