namespace VendAPI.Models
{
    using System.Runtime.Serialization;

    [DataContract]
    public class RegisterSaleList
    {
        [DataMember(Name = "register_sales")]
        public RegisterSale[] RegisterSales { get; set; }
    }
}
