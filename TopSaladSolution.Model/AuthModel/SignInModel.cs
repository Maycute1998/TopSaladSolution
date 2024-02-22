using System.ComponentModel.DataAnnotations;

namespace TopSaladSolution.Model.AuthModel
{
    public class SignInModel
    {
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
