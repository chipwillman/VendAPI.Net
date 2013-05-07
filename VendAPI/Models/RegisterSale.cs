namespace VendAPI.Models
{
    using System;
    using System.Runtime.Serialization;

    [DataContract]
    public class RegisterSale
    {
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public Guid Id { get; set; }

        [DataMember(Name = "register_id")]
        public Guid RegisterId { get; set; }

        [DataMember(Name = "market_id")]
        public string MarketId { get; set; }

        [DataMember(Name = "customer_id")]
        public string CustomerId { get; set; }

        [DataMember(Name = "customer_name")]
        public string CustomerName { get; set; }

        [DataMember(Name = "user_id")]
        public Guid UserId { get; set; }

        [DataMember(Name = "user_name")]
        public string UserName { get; set; }

        [DataMember(Name = "sale_date")]
        public string SaleDate { get; set; }

        [DataMember(Name = "total_price")]
        public decimal TotalPrice { get; set; }

        [DataMember(Name = "total_tax")]
        public decimal TotalTax { get; set; }

        [DataMember(Name = "tax_name")]
        public string TaxName { get; set; }

        [DataMember(Name = "note")]
        public string Note { get; set; }

        [DataMember(Name = "status")]
        public string Status { get; set; }

        [DataMember(Name = "short_code")]
        public string ShortCode { get; set; }

        [DataMember(Name = "invoice_number")]
        public string InvoiceNumber { get; set; }

        [DataMember(Name = "invoice_sequence")]
        public int InvoiceSequence { get; set; }

        [DataMember(Name = "register_sale_products")]
        public RegisterSaleProduct[] RegisterSaleProducts { get; set; }

        [DataMember(Name = "totals")]
        public Totals Totals { get; set; }

        [DataMember(Name = "register_sale_payments")]
        public RegisterSalePayment[] RegisterSalePayments { get; set; }
    }
}
