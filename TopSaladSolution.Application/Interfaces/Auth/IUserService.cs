using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopSaladSolution.Application.Interfaces.Auth
{
    public interface IUserService
    {
        Task<int> GetUserId();
        string GetToken { get; }
    }
}
