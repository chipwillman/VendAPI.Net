namespace VendAPI.Models
{
    using System;
    using System.Runtime.Serialization;

    [DataContract]
    public class PriceBookEntry
    {
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public Guid Id { get; set; }

        [DataMember(Name = "product_id")]
        public Guid ProductId { get; set; }

        [DataMember(Name = "price_book_id")]
        public Guid PriceBookId { get; set; }

        [DataMember(Name = "price_book_name")]
        public string PriceBookName { get; set; }

        [DataMember(Name = "type")]
        public string Type { get; set; }

        [DataMember(Name = "outlet_name")]
        public string OutletName { get; set; }

        [DataMember(Name = "outlet_id")]
        public string OutletId { get; set; }

        [DataMember(Name = "customer_group_name")]
        public string CustomerGroupName { get; set; }

        [DataMember(Name = "customer_group_id")]
        public string CustomerGroupId { get; set; }

        [DataMember(Name = "price")]
        public decimal Price { get; set; }

        [DataMember(Name = "loyalty_value")]
        public decimal LoyaltyValue { get; set; }

        [DataMember(Name = "tax")]
        public decimal Tax { get; set; }

        [DataMember(Name = "tax_id")]
        public string TaxId { get; set; }

        [DataMember(Name = "tax_rate")]
        public string TaxRate { get; set; }

        [DataMember(Name = "tax_name")]
        public string TaxName { get; set; }

        [DataMember(Name = "display_retail_price_tax_inclusive")]
        public string DisplayRetailPriceTaxInclusive { get; set; }

        [DataMember(Name = "min_units")]
        public string MinUnits { get; set; }

        [DataMember(Name = "max_units")]
        public string MaxUnits { get; set; }

        [DataMember(Name = "valid_from")]
        public string ValidFrom { get; set; }

        [DataMember(Name = "valid_to")]
        public string ValidTo { get; set; }
    }
}
