using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Bookings
{
    public class ListBookingsQueryHandler : IRequestHandler<ListBookingsQuery, IEnumerable<Booking>>
    {
        private readonly ApplicationDbContext _context;
        public ListBookingsQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Booking>> Handle(ListBookingsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Bookings.ToListAsync(cancellationToken);
        }
    }
}
