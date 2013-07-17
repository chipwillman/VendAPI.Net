namespace VendAPI.Models
{
    using System.Runtime.Serialization;

    [DataContract]
    public class ProductList
    {
        [DataMember(Name = "pagination")]
        public Pagination Pagination { get; set; }

        [DataMember(Name = "products")]
        public Product[] Products { get; set; }
    }
}
