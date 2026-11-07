using System;
using System.Collections.Generic;
using System.Text;

namespace KooliProjekt.Application.Dto
{
    public class InvoiceDto
    {
        public int Id { get; set; }
        public int BookingId { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public decimal TotalAmount { get; set; }
        public string PaymentStatus { get; set; }
        public int MarkedPaidByUserId { get; set; }
        public DateTime MarkedPaidAt { get; set; }
    }
}