using System;
using KooliProjekt.Application.Behaviors;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Bookings
{
    public class SaveBookingCommand : IRequest<OperationResult>, ITransactional
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