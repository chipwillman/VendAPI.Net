namespace VendAPI.Models
{
    using System.Runtime.Serialization;

    [DataContract]
    public class RegisterSaleWrapper
    {
        [DataMember(Name = "register_sale")]
        public RegisterSale RegisterSale { get; set; }
    }
}
