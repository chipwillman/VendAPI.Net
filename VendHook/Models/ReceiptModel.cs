namespace VendHook.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;

    public class ReceiptContext : DbContext
    {
        public ReceiptContext() : base("DefaultConnection")
        {
            
        }

        public DbSet<ReceiptHeader> ReceiptHeaders { get; set; }
        public DbSet<ReceiptLine> ReceiptLines { get; set; }
    }

    [Table("ReceiptHeader")]
    public class ReceiptHeader
    {
        [Key]
        public Guid Id { get; set; }
        
        public string ReceiptName { get; set; }
    }

    [Table("ReceiptLine")]
    public class ReceiptLine
    {
        [Key]
        public Guid Id { get; set; }
        
        public Guid ReceiptHeaderId { get; set; }

        public string Type { get; set; }

        public string Text { get; set; }

        public string Field { get; set; }

        public int Left { get; set; }

        public int Top { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }
    }
}