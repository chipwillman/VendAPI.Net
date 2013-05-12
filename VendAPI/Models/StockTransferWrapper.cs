namespace VendAPI.Models
{
    using System.Runtime.Serialization;

    [DataContract]
    public class StockTransferWrapper
    {
        [DataMember(Name = "stock_movements")]
        public Consignment[] StockMovements { get; set; }
        
        [DataMember(Name = "stock_movement")]
        public Consignment StockMovement { get; set; }
    }
}
