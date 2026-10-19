using System.ComponentModel.DataAnnotations;

namespace KooliProjekt.Application.Data
{
    public class InvoiceLine
    {
        [Key]
        public int invoice_line_id { get; set; }

        public int invoice_id { get; set; }
        public string description { get; set; } = string.Empty;
        public decimal quantity { get; set; }
        public decimal unit_price { get; set; }
        public decimal amount { get; set; }
    }
}