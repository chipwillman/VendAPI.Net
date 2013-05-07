namespace VendAPI.Models
{
    using System;
    using System.Runtime.Serialization;

    [DataContract]
    public class RegisterSaleProduct
    {
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public Guid Id { get; set; }

        [DataMember(Name = "product_id")]
        public Guid ProductId { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "quantity")]
        public int Quantity { get; set; }

        [DataMember(Name = "price")]
        public decimal Price { get; set; }

        [DataMember(Name = "tax")]
        public decimal Tax { get; set; }

        [DataMember(Name = "tax_id")]
        public string TaxId { get; set; }

        [DataMember(Name = "tax_rate")]
        public string TaxRate { get; set; }

        [DataMember(Name = "tax_total")]
        public decimal TaxTotal { get; set; }

        [DataMember(Name = "price_total")]
        public string PriceTotal { get; set; }

        [DataMember(Name = "display_retail_price_tax_inclusive")]
        public string DisplayRetailPriceTaxInclusive { get; set; }
    }
}
