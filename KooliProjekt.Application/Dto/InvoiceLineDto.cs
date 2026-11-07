using System;
using System.Collections.Generic;
using System.Text;

namespace KooliProjekt.Application.Dto
{
    public class InvoiceLineDto
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public string Description { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Amount { get; set; }
    }
}