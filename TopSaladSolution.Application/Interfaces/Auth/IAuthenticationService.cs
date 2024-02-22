using Microsoft.AspNetCore.Identity;
using TopSaladSolution.Application.ViewModels;

namespace TopSaladSolution.Application.Interfaces.Auth
{
    public interface IAuthenticationService
    {
        Task<IdentityResult> SignUpAsync(SignUpModel model);
        Task<string> SignInAsync(SignInModel model);
    }
}
