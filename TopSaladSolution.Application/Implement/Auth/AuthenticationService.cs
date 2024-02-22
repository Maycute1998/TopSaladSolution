using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TopSaladSolution.Application.Interfaces.Auth;
using TopSaladSolution.Application.ViewModels;
using TopSaladSolution.Infrastructure.Entities;

namespace TopSaladSolution.Application.Implement.Auth
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<AppRole> _roleManager;

        //private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IConfiguration configuration;

        public AuthenticationService(UserManager<AppUser> userManager,
            //SignInManager<ApplicationUser> signInManager,
            RoleManager<AppRole> roleManager,
            IConfiguration iconfiguration)
        {
            this.userManager = userManager;
            //this.signInManager = signInManager;
            _roleManager = roleManager;
            this.configuration = iconfiguration;
        }
        public async Task<TokenResult> SignInAsync(SignInModel model)
        {
            var user = await userManager.FindByNameAsync(model.UserName);
            if (user is not null && await userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>()
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                //foreach (var userRole in userRoles)
                //{
                //    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                //}

                var authKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["JWT:SecretKey"]));

                var token = new JwtSecurityToken(
                    issuer: configuration["JWT:ValidIssuer"],
                    audience: configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(int.Parse(configuration["JWT:Expiry"])),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authKey, SecurityAlgorithms.HmacSha256));
                return new TokenResult
                {
                    Status = true,
                    Message = "Login Success",
                    Token = new JwtSecurityTokenHandler().WriteToken(token)
                };
                //return new JwtSecurityTokenHandler().WriteToken(token);
            }
            return new TokenResult
            {
                Status = false,
                Message = "Login Failed",
                Token = string.Empty
            };
        }

        public async Task<RegisterResult> SignUpAsync(SignUpModel model)
        {
            var user = new AppUser()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                PasswordHash = model.Password,
                UserName = model.UserName,
                Dob = DateTime.Now,
                PhoneNumber = "0965832841",
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnabled = false,
                AccessFailedCount = 0
            };
            var result = await userManager.CreateAsync(user, model.Password);
            var errors = result.Errors.Select(e => new RegisterErrorResult
            {
                Code = e.Code,
                Description = e.Description
            });
            if (result.Succeeded)
            {
                return new RegisterResult
                {
                    Status = true,
                    Message = "Register Success"
                };
            }
            return new RegisterResult
            {
                Status = false,
                Message = "Register Failed",
                Errors = errors.ToList()
            };
        }
    }
}
