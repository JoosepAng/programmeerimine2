using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.InvoiceLines
{
    public class ListInvoiceLinesQueryHandler : IRequestHandler<ListInvoiceLinesQuery, IEnumerable<InvoiceLine>>
    {
        private readonly ApplicationDbContext _dbContext;

        public ListInvoiceLinesQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<InvoiceLine>> Handle(ListInvoiceLinesQuery request, CancellationToken cancellationToken)
        {
            var results = await _dbContext.InvoiceLines.ToListAsync(cancellationToken);

            return results;
        }
    }
}
