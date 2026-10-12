using System;
using System.Collections.Generic;
using System.Text;

namespace KooliProjekt.Application.Data
{
    public class Car
    {
        public int car_id { get; set; }

        public string registration_no { get; set; } = string.Empty;

        public string make { get; set; } = string.Empty;

        public string model { get; set; } = string.Empty;

        public int car_type_id { get; set; }

        public Decimal time_rate { get; set; }

        public Decimal kilometer_rate { get; set; }

        public string status { get; set; } = "available";
    }

    
}
