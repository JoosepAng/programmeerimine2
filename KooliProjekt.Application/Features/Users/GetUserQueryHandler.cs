using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Users
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, OperationResult<UserDto>>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetUserQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<UserDto>> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<UserDto>();

            result.Value = await _dbContext.Users
                 .AsNoTracking()
                 .Where(user => user.user_id == request.Id)
                 .Select(user => new UserDto
                 {
                     Id = user.user_id,
                     UserName = user.user_name,
                     FirstName = user.first_name,
                     LastName = user.last_name,
                     Email = user.email,
                     Role = user.role,
                     RegisteredAt = user.registered_at
                 })
                 .FirstOrDefaultAsync(cancellationToken);

            return result;
        }
    }
}
