using PSE.WebApp.MVC.Extensions;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PSE.WebApp.MVC.Models;

public class RegisterUser
{
    [Required(ErrorMessage = "The {0} field is mandatory")]
    [DisplayName("Name")]
    public string Name { get; set; }

    [Required(ErrorMessage = "The {0} field is mandatory")]
    [DisplayName("CPF")]
    [Cpf]
    public string Cpf { get; set; }

    [Required(ErrorMessage = "The {0} field is mandatory")]
    [EmailAddress(ErrorMessage = "The {0} field is in an invalid format")]
    public string Email { get; set; }

    [Required(ErrorMessage = "The {0} field is mandatory")]
    [StringLength(100, ErrorMessage = "The {0} field must be between {2} and {1} characters", MinimumLength = 6)]
    public string Password { get; set; }

    [Compare("Password", ErrorMessage = "Passwords do not match.")]
    public string ConfirmPassword { get; set; }
}

public class LoginUser
{
    [Required(ErrorMessage = "The {0} field is mandatory")]
    [EmailAddress(ErrorMessage = "The {0} field is in an invalid format")]
    public string Email { get; set; }

    [Required(ErrorMessage = "The {0} field is mandatory")]
    [StringLength(100, ErrorMessage = "The {0} field must be between {2} and {1} characters", MinimumLength = 6)]
    public string Password { get; set; }
}