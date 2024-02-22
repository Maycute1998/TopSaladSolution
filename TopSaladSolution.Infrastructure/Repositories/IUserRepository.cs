using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopSaladSolution.Infrastructure.Entities;
using TopSaladSolution.Model.AuthModel;

namespace TopSaladSolution.Infrastructure.Repositories
{
    public interface IUserRepository
    {
        Task<object> GetUserIdByUserNameAsync(string id);
        Task<SignUpModel> CreateUserAsync(SignUpModel model);

    }
}
