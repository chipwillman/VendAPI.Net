namespace VendAPI.Models
{
    using System;
    using System.Runtime.Serialization;

    [DataContract]
    public class RegisterSalePayment
    {
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public Guid Id { get; set; }

        [DataMember(Name = "payment_type_id")]
        public string PaymentTypeId { get; set; }

        [DataMember(Name = "retailer_payment_type_id")]
        public string RetailerPaymentTypeId { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "amount")]
        public string Amount { get; set; }
    }
}
