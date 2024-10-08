using System;
using System.ComponentModel.DataAnnotations;

namespace SharedLibrary.DTO.Account.SignIn;

public class Request
{
    [Required(ErrorMessage = "This field is required.")]
    public string? EmailAddress { get; set; }
    [Required(ErrorMessage = "Password is required.")]
    public string? Password { get; set; }
}
