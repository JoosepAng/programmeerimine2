using System.ComponentModel.DataAnnotations;

namespace KooliProjekt.Application.Data
{
    public class Car
    {
        [Key]
        public int car_id { get; set; }

        public string registration_no { get; set; } = string.Empty;
        public string make { get; set; } = string.Empty;
        public string model { get; set; } = string.Empty;
        public int car_type_id { get; set; }
        public decimal time_rate { get; set; }
        public decimal kilometer_rate { get; set; }
        public string status { get; set; } = "available";
    }
}