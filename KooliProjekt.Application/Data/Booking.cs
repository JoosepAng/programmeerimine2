using System;
using System.ComponentModel.DataAnnotations;

namespace KooliProjekt.Application.Data
{
    public class Booking
    {
        [Key]
        public int booking_id { get; set; }

        public int user_id { get; set; }
        public int car_id { get; set; }

        public DateTime start_time { get; set; } = DateTime.UtcNow;
        public DateTime end_time { get; set; } = DateTime.UtcNow;

        public decimal starting_odometer { get; set; }
        public decimal ending_odometer { get; set; }
        public decimal time_rate_at_booking { get; set; }
        public decimal kilometer_rate_at_booking { get; set; }

        public string status { get; set; } = "active";
    }
}