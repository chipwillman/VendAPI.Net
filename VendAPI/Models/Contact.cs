namespace VendAPI.Models
{
    using System.Runtime.Serialization;

    [DataContract]
    public class Contact
    {
        [DataMember(Name = "first_name")]
        public string FirstName { get; set; }

        [DataMember(Name = "last_name")]
        public string LastName { get; set; }

        [DataMember(Name = "company_name")]
        public string CompanyName { get; set; }

        [DataMember(Name = "phone")]
        public string Phone { get; set; }

        [DataMember(Name = "mobile")]
        public string Mobile { get; set; }

        [DataMember(Name = "fax")]
        public string Fax { get; set; }

        [DataMember(Name = "email")]
        public string Email { get; set; }

        [DataMember(Name = "twitter")]
        public string Twitter { get; set; }

        [DataMember(Name = "website")]
        public string Website { get; set; }

        [DataMember(Name = "physical_address1")]
        public string PhysicalAddress1 { get; set; }

        [DataMember(Name = "physical_address2")]
        public string PhysicalAddress2 { get; set; }

        [DataMember(Name = "physical_suburb")]
        public string PhysicalSuburb { get; set; }

        [DataMember(Name = "physical_city")]
        public string PhysicalCity { get; set; }

        [DataMember(Name = "physical_postcode")]
        public string PhysicalPostcode { get; set; }

        [DataMember(Name = "physical_state")]
        public string PhysicalState { get; set; }

        [DataMember(Name = "physical_country_id")]
        public string PhysicalCountryId { get; set; }

        [DataMember(Name = "postal_address1")]
        public string PostalAddress1 { get; set; }

        [DataMember(Name = "postal_address2")]
        public string PostalAddress2 { get; set; }

        [DataMember(Name = "postal_suburb")]
        public string PostalSuburb { get; set; }

        [DataMember(Name = "postal_city")]
        public string PostalCity { get; set; }

        [DataMember(Name = "postal_postcode")]
        public string PostalPostcode { get; set; }

        [DataMember(Name = "postal_state")]
        public string PostalState { get; set; }

        [DataMember(Name = "postal_country_id")]
        public string PostalCountryId { get; set; }
    }
}
