using System;
using System.Collections.Generic;
using System.Text;

namespace KooliProjekt.Application.Dto
{
    public class CarDto
    {
        public int Id { get; set; }
        public string RegistrationNo { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int CarTypeId { get; set; }
        public decimal TimeRate { get; set; }
        public decimal KilometerRate { get; set; }
        public string Status { get; set; }
    }
}