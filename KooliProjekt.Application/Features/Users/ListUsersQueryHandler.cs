using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Users
{
    public class ListUsersQueryHandler : IRequestHandler<ListUsersQuery, OperationResult<PagedResult<UserDto>>>
    {
        private readonly ApplicationDbContext _dbContext;

        public ListUsersQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<PagedResult<UserDto>>> Handle(ListUsersQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<PagedResult<UserDto>>();

            result.Value = await _dbContext.Users
                .AsNoTracking()
                .OrderBy(user => user.user_id)
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
                .GetPagedAsync(request.Page, request.PageSize);

            return result;
        }
    }
}