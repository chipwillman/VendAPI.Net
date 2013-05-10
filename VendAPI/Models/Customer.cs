namespace VendAPI.Models
{
    using System;
    using System.Runtime.Serialization;

    [DataContract]
    public class Customer
    {
        [DataMember(Name = "id")]
        public Guid Id { get; set; }

        [DataMember(Name = "retailer_id")]
        public string RetailerId { get; set; }

        [DataMember(Name = "customer_code")]
        public string CustomerCode { get; set; }

        [DataMember(Name = "balance")]
        public string Balance { get; set; }

        [DataMember(Name = "points")]
        public string Points { get; set; }

        [DataMember(Name = "note")]
        public string Note { get; set; }

        [DataMember(Name = "year_to_date")]
        public string YearToDate { get; set; }

        [DataMember(Name = "sex")]
        public string Sex { get; set; }

        [DataMember(Name = "date_of_birth")]
        public string DateOfBirth { get; set; }

        [DataMember(Name = "custom_field_1")]
        public string CustomField1 { get; set; }

        [DataMember(Name = "custom_field_2")]
        public string CustomField2 { get; set; }

        [DataMember(Name = "custom_field_3")]
        public string CustomField3 { get; set; }

        [DataMember(Name = "custom_field_4")]
        public string CustomField4 { get; set; }

        [DataMember(Name = "updated_at")]
        public string UpdatedAt { get; set; }

        [DataMember(Name = "created_at")]
        public string CreatedAt { get; set; }

        [DataMember(Name = "contact")]
        public Contact Contact { get; set; }

        [DataMember(Name = "contact_first_name")]
        public string ContactFirstName { get; set; }

        [DataMember(Name = "contact_last_name")]
        public string ContactLastName { get; set; }
    }
}
