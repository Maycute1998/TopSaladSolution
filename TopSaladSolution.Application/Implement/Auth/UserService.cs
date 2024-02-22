using Microsoft.Extensions.Logging;
using TopSaladSolution.Application.Interfaces.Auth;

namespace TopSaladSolution.Application.Implement.Auth
{
    public class UserService : IUserService
    {
        private readonly ILogger<IUserService> _logger;

        public UserService(ILogger<IUserService> logger)
        {
            _logger = logger;
        }
        public string GetToken => throw new NotImplementedException();

        public Task<int> GetUserId()
        {
            throw new NotImplementedException();
        }
    }
}
