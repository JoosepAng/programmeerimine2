using System;
using System.Collections.Generic;
using System.Text;

namespace KooliProjekt.Application.Data
{
    public class Booking
    {
        public int booking_id { get; set; }
        public int user_id { get; set; }
        public int car_id { get; set; }

        public DateTime start_time { get; set; } = DateTime.UtcNow;
        public DateTime end_time { get; set; } = DateTime.UtcNow;

        public Decimal starting_odometer { get; set; }

        public Decimal ending_odometer { get; set; }

        public Decimal time_rate_at_booking { get; set; }

        public Decimal kilometer_rate_at_booking { get; set; }

        public string status { get; set; } = "active";
    }
}
