namespace VendAPI.Models
{
    using System;
    using System.Runtime.Serialization;

    [DataContract]
    public class Totals
    {
        [DataMember(Name = "total_tax")]
        public string TotalTax { get; set; }

        [DataMember(Name = "total_price")]
        public string TotalPrice { get; set; }

        [DataMember(Name = "total_payment")]
        public string TotalPayment { get; set; }

        [DataMember(Name = "total_to_pay")]
        public string TotalToPay { get; set; }
    }
}
