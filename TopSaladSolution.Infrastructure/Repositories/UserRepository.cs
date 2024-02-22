using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using TopSaladSolution.Infrastructure.EF;
using TopSaladSolution.Infrastructure.Entities;
using TopSaladSolution.Model.AuthModel;

namespace TopSaladSolution.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IUnitOfWork _uow;
        private readonly ILogger<UserRepository> _logger;
        private readonly TopSaladDbContext _context;

        public UserRepository(IUnitOfWork uow, ILogger<UserRepository> logger, TopSaladDbContext context)
        {
            _uow = uow;
            _logger = logger;
            _context = context;
        }

        public Task<SignUpModel> CreateUserAsync(SignUpModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<object> GetUserIdByUserNameAsync(string userName)
        {
            try
            {
                var user = await _uow.GetRepository<AppUser>().GetSingleAsync(x => x.UserName == userName);
                if(user is not null)
                {
                    return user;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error : [IdentityCommon] {ex.Message}");
            }
            return 0;
        }
    }
}
