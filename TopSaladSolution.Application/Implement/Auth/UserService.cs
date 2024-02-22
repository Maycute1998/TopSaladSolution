using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopSaladSolution.Application.Interfaces.Auth;
using TopSaladSolution.Infrastructure.Repositories;

namespace TopSaladSolution.Application.Implement.Auth
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _uow;
        private readonly ILogger<IUserService> _logger;

        public UserService(IUnitOfWork uow, ILogger<IUserService> logger)
        {
            _uow = uow;
            _logger = logger;
        }
        public string GetToken => throw new NotImplementedException();

        public Task<int> GetUserId()
        {
            throw new NotImplementedException();
        }
    }
}
