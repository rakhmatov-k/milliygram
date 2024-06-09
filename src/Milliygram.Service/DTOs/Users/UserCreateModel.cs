using System.ComponentModel.DataAnnotations;

namespace Milliygram.Service.DTOs.Users;

public class UserCreateModel
{
    [Required]
    [Display(Name = "FirstName")]
    public string FirstName { get; set; }

    [Required]
    [Display(Name = "LastName")]
    public string LastName { get; set; }

    [Required]
    [Display(Name = "UserName")]
    public string UserName { get; set; }

    [Required]
    [EmailAddress]
    [Display(Name = "Email")]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; }

    [Required(ErrorMessage = "You must accept the terms and conditions.")]
    [Display(Name = "I have read and agree to the terms")]
    public bool TermsAccepted { get; set; }
}