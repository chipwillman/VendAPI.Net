namespace VendAPI.Models
{
    using System.Runtime.Serialization;

    [DataContract]
    public class ProductWrapper
    {
        [DataMember(Name = "product")]
        public Product Product { get; set; }
    }
}
