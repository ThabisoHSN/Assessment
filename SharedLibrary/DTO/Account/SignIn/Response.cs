using System;

namespace SharedLibrary.DTO.Account.SignIn;

public class Response
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string StudentNumber {  get; set; }
    public string SecurityToken { get; set; }
    public string Error{ get; set; }
}
