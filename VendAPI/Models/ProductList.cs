namespace VendAPI.Models
{
    using System.Runtime.Serialization;

    [DataContract]
    public class ProductList
    {
        [DataMember(Name = "products")]
        public Product[] Products { get; set; }
    }
}
