namespace VendAPI.Models
{
    using System.Runtime.Serialization;

    [DataContract]
    public class Pagination
    {
        [DataMember(Name = "results")]
        public int Results { get; set; }

        [DataMember(Name = "page")]
        public int Page { get; set; }

        [DataMember(Name = "page_size")]
        public int PageSize { get; set; }

        [DataMember(Name = "pages")]
        public int Pages { get; set; }
    }
}
