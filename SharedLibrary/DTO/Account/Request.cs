using System;
using System.ComponentModel.DataAnnotations;

namespace SharedLibrary.DTO.Account;

public class Request
{
    [Required(ErrorMessage = "Name is required.")]
    public string? Name { get; set; }
    [Required(ErrorMessage = "Surname is required.")]
    public string? Surname { get; set; }
    [Required(ErrorMessage = "Email address is required.")]
    [EmailAddress(ErrorMessage = "Invalid email address.")]
    public string? EmailAddress { get; set; }

    [Required(ErrorMessage = "Contact number is required.")]
    [RegularExpression(@"^\d{10}$", ErrorMessage = "Contact number must be exactly 10 digits.")]
    public string? ContactNumber { get; set; }
    [Required(ErrorMessage = "Password is required.")]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters.")]
    public string? Password { get; set; }
    [Compare("Password", ErrorMessage = "Passwords do not match")]
    [Required]
    public string? ConfirmPassword { get; set; }
}
