using System;
using System.Collections.Generic;
using System.Text;

namespace KooliProjekt.Application.Data
{
    public class Invoice
    {
        public int invoice_id { get; set; }
        public int booking_id { get; set; }
        public int user_id { get; set; }

        public DateTime created_at { get; set; } = DateTime.UtcNow;
        public Decimal total_amount { get; set; }

        public string payment_status { get; set; } = "unpaid";

        public int marked_paid_by_user_id { get; set; } = 0;

        public DateTime marked_paid_at { get; set; } = DateTime.MinValue;
    }
}
