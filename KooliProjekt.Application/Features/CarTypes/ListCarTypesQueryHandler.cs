using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.CarTypes
{
    public class ListCarTypesQueryHandler : IRequestHandler<ListCarTypesQuery, IEnumerable<CarType>>
    {
        private readonly ApplicationDbContext _dbContext;

        public ListCarTypesQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<CarType>> Handle(ListCarTypesQuery request, CancellationToken cancellationToken)
        {
            var results = await _dbContext.CarTypes.ToListAsync(cancellationToken);

            return results;
        }
    }
}
