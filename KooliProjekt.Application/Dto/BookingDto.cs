using System;
using System.Collections.Generic;
using System.Text;

namespace KooliProjekt.Application.Dto
{
    public class BookingDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CarId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public decimal StartingOdometer { get; set; }
        public decimal EndingOdometer { get; set; }
        public decimal TimeRateAtBooking { get; set; }
        public decimal KilometerRateAtBooking { get; set; }
        public string Status { get; set; }
    }
}
