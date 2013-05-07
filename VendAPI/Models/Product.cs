namespace VendAPI.Models
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.Serialization;

    [DataContract]
    public class Product
    {
        public enum OrderBy
        {
            [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1602:EnumerationItemsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
            updasted_at,
            id,
            name
        }

        [DataMember(Name = "id", EmitDefaultValue = false)]
        public Guid Id { get; set; }

        [DataMember(Name = "source_id")]
        public string SourceId { get; set; }

        [DataMember(Name = "variant_source_id")]
        public string VariantSourceId { get; set; }

        [DataMember(Name = "handle")]
        public string Handle { get; set; }

        [DataMember(Name = "type")]
        public string Type { get; set; }

        [DataMember(Name = "variant_parent_id")]
        public string VariantParentId { get; set; }

        [DataMember(Name = "variant_option_one_name")]
        public string VariantOptionOneName { get; set; }

        [DataMember(Name = "variant_option_one_value")]
        public string VariantOptionOneValue { get; set; }

        [DataMember(Name = "variant_option_two_name")]
        public string VariantOptionTwoName { get; set; }

        [DataMember(Name = "variant_option_two_value")]
        public string VariantOptionTwoValue { get; set; }

        [DataMember(Name = "variant_option_three_name")]
        public string VariantOptionThreeName { get; set; }

        [DataMember(Name = "variant_option_three_value")]
        public string VariantOptionThreeValue { get; set; }

        [DataMember(Name = "active")]
        public bool Active { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "image")]
        public string Image { get; set; }

        [DataMember(Name = "image_large")]
        public string ImageLarge { get; set; }

        [DataMember(Name = "sku")]
        public string Sku { get; set; }

        [DataMember(Name = "tags")]
        public string Tags { get; set; }

        [DataMember(Name = "brand_id")]
        public string BrandId { get; set; }

        [DataMember(Name = "brand_name")]
        public string BrandName { get; set; }

        [DataMember(Name = "supplier_name")]
        public string SupplierName { get; set; }

        [DataMember(Name = "supplier_code")]
        public string SupplierCode { get; set; }

        [DataMember(Name = "supply_price")]
        public string SupplyPrice { get; set; }

        [DataMember(Name = "account_code_purchase")]
        public string AccountCodePurchase { get; set; }

        [DataMember(Name = "account_code_sales")]
        public string AccountCodeSales { get; set; }

        [DataMember(Name = "inventory")]
        public Inventory[] Inventory  { get; set; }

        [DataMember(Name = "price_book_entries")]
        public PriceBookEntry[] PriceBookEntries { get; set; }

        [DataMember(Name = "price")]
        public decimal Price { get; set; }

        [DataMember(Name = "tax")]
        public decimal Tax { get; set; }

        [DataMember(Name = "tax_id")]
        public string TaxId { get; set; }

        [DataMember(Name = "tax_rate")]
        public decimal TaxRate { get; set; }

        [DataMember(Name = "tax_name")]
        public string TaxName { get; set; }

        [DataMember(Name = "display_retail_price_tax_inclusive")]
        public string DisplayRetailPriceTaxInclusive { get; set; }

        [DataMember(Name = "updated_at")]
        public string UpdatedAt { get; set; }

        [DataMember(Name = "deleted_at")]
        public string DeletedAt { get; set; }
    }
}
