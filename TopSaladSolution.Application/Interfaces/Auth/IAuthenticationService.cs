using Microsoft.AspNetCore.Identity;
using TopSaladSolution.Application.ViewModels;

namespace TopSaladSolution.Application.Interfaces.Auth
{
    public interface IAuthenticationService
    {
        Task<RegisterResult> SignUpAsync(SignUpModel model);
        Task<TokenResult> SignInAsync(SignInModel model);
    }
}
