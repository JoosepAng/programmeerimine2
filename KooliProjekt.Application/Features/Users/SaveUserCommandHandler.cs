using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Users
{
    public class SaveUserCommandHandler : IRequestHandler<SaveUserCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;

        public SaveUserCommandHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult> Handle(
            SaveUserCommand request,
            CancellationToken cancellationToken)
        {
            var result = new OperationResult();

            var usernameExists = await _dbContext.Users.AnyAsync(
                user => user.user_name == request.UserName
                     && user.user_id != request.Id,
                cancellationToken);

            if (usernameExists)
            {
                return result.AddPropertyError(
                    "UserName",
                    "See kasutajanimi on juba kasutusel.");
            }

            User user;

            if (request.Id == 0)
            {
                user = new User();
                _dbContext.Users.Add(user);
            }
            else
            {
                user = await _dbContext.Users.FindAsync(
                    new object[] { request.Id }, cancellationToken);

                if (user == null)
                {
                    return result.AddError($"User ID-ga {request.Id} ei leitud.");
                }
            }

            user.user_name = request.UserName;
            user.first_name = request.FirstName;
            user.last_name = request.LastName;
            user.email = request.Email;
            user.password_hash = request.PasswordHash;
            user.registered_at = request.RegisteredAt;
            user.role = request.Role;

            await _dbContext.SaveChangesAsync(cancellationToken);
            return result;
        }
    }
}