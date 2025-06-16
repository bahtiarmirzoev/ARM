using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using ARM.Common.Exceptions;
using ARM.Core.Abstractions.Services.Auth;
using ARM.Core.Abstractions.Services.Main;
using ARM.Core.Dtos.Read;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ARM.Application.Services.Auth;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;
    private readonly IRoleService _roleService;

    public TokenService(IConfiguration configuration, IRoleService roleService)
    {
        _configuration = configuration;
        _roleService = roleService;
    }

    public string GenerateAccessToken(UserDto user)
    {
        var claims = new List<Claim>
        {
            new("userId", user.Id),
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.Role, user.Role.Name),
        };

        switch (user.Role.Name)
        {
            case "Admin":
            case "Manager":
                if (!string.IsNullOrWhiteSpace(user.BrandId))
                {
                    claims.Add(new Claim("autoServiceId", user.BrandId));
                    claims.Add(new Claim("panel", user.Role.Name));

                }
                break;
            case "SuperAdmin":
                claims.Add(new Claim("panel", "superadmin"));
                break;
        }

        return GenerateToken(claims);
    }

    public string GenerateAccessToken(CustomerDto customer)
    {
        var claims = new List<Claim>
        {
            new("customerId", customer.Id),
            new(ClaimTypes.Email, customer.Email),
            new(ClaimTypes.Role, "customer"),
            new("panel", "customer"),
        };

        return GenerateToken(claims);
    }

    private string GenerateToken(IEnumerable<Claim> claims)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]))
            ?? throw new AppException(ExceptionType.NotFound, "SecretKeyNotFound");

        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["JWT:Issuer"],
            audience: _configuration["JWT:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(3),
            signingCredentials: signingCredentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GenerateRefreshToken()
        => Guid.NewGuid().ToString();

    public string GenerateRandomPassword(int length = 12)
    {
        const string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        StringBuilder password = new();
        using var rng = new RNGCryptoServiceProvider();

        byte[] buffer = new byte[sizeof(uint)];

        while (length-- > 0)
        {
            rng.GetBytes(buffer);
            uint num = BitConverter.ToUInt32(buffer, 0);
            password.Append(validChars[(int)(num % (uint)validChars.Length)]);
        }

        return password.ToString();
    }
}