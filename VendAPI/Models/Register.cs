namespace VendAPI.Models
{
    using System;
    using System.Runtime.Serialization;

    [DataContract]
    public class Register
    {
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public Guid Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "outlet_id")]
        public string OutletId { get; set; }

        [DataMember(Name = "print_receipt")]
        public string PrintReceipt { get; set; }

        [DataMember(Name = "receipt_header")]
        public string ReceiptHeader { get; set; }

        [DataMember(Name = "receipt_footer")]
        public string ReceiptFooter { get; set; }

        [DataMember(Name = "receipt_style_sheet")]
        public string ReceiptStyleSheet { get; set; }

        [DataMember(Name = "invoice_prefix")]
        public string InvoicePrefix { get; set; }

        [DataMember(Name = "invoice_suffix")]
        public string InvoiceSuffix { get; set; }

        [DataMember(Name = "invoice_sequence")]
        public string InvoiceSequence { get; set; }

        [DataMember(Name = "register_open_count_sequence")]
        public string RegisterOpenCountSequence { get; set; }

        [DataMember(Name = "register_open_time")]
        public string RegisterOpenTime { get; set; }

        [DataMember(Name = "register_close_time")]
        public string RegisterCloseTime { get; set; }
    }
}
