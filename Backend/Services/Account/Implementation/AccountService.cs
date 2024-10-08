using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Backend.DAO;
using Backend.Helper;
using Backend.Services.Student;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SharedLibrary.DTO.Account;
using SharedLibrary.DTO.Account.SignIn;

namespace Backend.Services.Account.Implementation;

public class AccountService : IAccountService
{
    private readonly IStudentReadService _studentReadService;
    private readonly IStudentCreateService _studentCreationService;
    private readonly JWTSettings _jWTSettings;
    public AccountService(IStudentReadService studentReadService, IStudentCreateService studentCreateService, IOptions<JWTSettings> jwtSettings)
    {
        _studentReadService = studentReadService;
        _studentCreationService = studentCreateService;
        _jWTSettings = jwtSettings.Value;
    }



    public async Task<SharedLibrary.DTO.Account.SignIn.Response> RegisterStudent(SharedLibrary.DTO.Account.Request request)
    {
        try
        {
            var checkStudent = await _studentReadService.GetStudentByEmailOrStudentNumber(request.EmailAddress);

            if (checkStudent != null)
            {
                return new Response() { Error = "This account already exists" };
            }

            string Password = request.Password;
            var salt = GenerateSalt();
            request.Password = HashPassword(request.Password,salt);
            var student = GlobalHelper.MapperFields(request, new SharedLibrary.DTO.Student.Request());

            var response = await _studentCreationService.CreateStudent(student);

            if (!response)
            {
                return new Response() { Error = "Failed to create a student" };
            }

            request.Password = Password;
            var login = await SignIn(GlobalHelper.MapperFields(request, new SharedLibrary.DTO.Account.SignIn.Request()));

            return login;

        }
        catch (Exception e)
        {
            return new Response() { Error = e.Message };
        }
    }

    public async Task<SharedLibrary.DTO.Account.SignIn.Response> SignIn(SharedLibrary.DTO.Account.SignIn.Request request)
    {
        try
        {
            var student = await _studentReadService.GetStudentByEmailOrStudentNumber(request.EmailAddress);
            if (student == null)
            {
                return new Response() { Error = "Invalid Credentials" };
            }

            if (!VerifyPassword(student.Password, request.Password))
            {
                return new Response() { Error = "Invalid Credentials" };
            }

            var response = GlobalHelper.MapperFields(student, new Response());

            response.SecurityToken = GenerateToken(GlobalHelper.MapperFields(student, new DAO.Student()));

            return response;


        }
        catch (Exception e)
        {
            return new Response() { Error = e.Message };
        }
    }


    private string GenerateToken(DAO.Student student)
    {
        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jWTSettings.Secret));

        var credentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>();
        claims.Add(new Claim("StudentNumber", student.StudentNumber));
        claims.Add(new Claim("Name", student.Name));
        claims.Add(new Claim("Surname", student.Surname));

        var securityToken = new JwtSecurityToken(
            signingCredentials: credentials,
            issuer: _jWTSettings.Issuer,
            audience: _jWTSettings.Audience,
            expires: DateTime.Now.AddHours(1),
            claims: claims
        );

        return new JwtSecurityTokenHandler().WriteToken(securityToken);

    }

    private string HashPassword(string password, byte[] salt)
    {
        string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
           password: password,
           salt: salt,
           prf: KeyDerivationPrf.HMACSHA256,
           iterationCount: 100000,
           numBytesRequested: 256 / 8));

        return $"{Convert.ToBase64String(salt)}:{hashed}";
        
    }

    private byte[] GenerateSalt()
    {
        byte[] salt = new byte[128 / 8];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
        }
        return salt;
    }

    private bool VerifyPassword(string hashedPasswordWithSalt, string password)
    {
        var parts = hashedPasswordWithSalt.Split(':');
        var salt = Convert.FromBase64String(parts[0]);
        var hashedPassword = parts[1];

        var hashedToCheck = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 256 / 8));

        return hashedPassword == hashedToCheck;
    }

}
