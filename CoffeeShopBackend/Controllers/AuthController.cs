using Microsoft.AspNetCore.Mvc;
using CoffeeShopBackend.Services;
using CoffeeShopBackend.Models;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserService _userService;
    private readonly IConfiguration _configuration;

    public AuthController(UserService userService, IConfiguration configuration)
    {
        _userService = userService;
        _configuration = configuration;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] User user)
    {
        if (await _userService.GetUserByUsernameAsync(user.Username) != null)
            return BadRequest("Username already exists.");

        // Hash the password
        using var hmac = new HMACSHA256();
        user.PasswordHash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(user.PasswordHash)));

        await _userService.AddUserAsync(user);
        return Ok("User registered successfully.");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] User loginDetails)
    {
        var user = await _userService.GetUserByUsernameAsync(loginDetails.Username);
        if (user == null)
            return Unauthorized("Invalid username.");

        // Verify password
        using var hmac = new HMACSHA256();
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDetails.PasswordHash));
        if (Convert.ToBase64String(computedHash) != user.PasswordHash)
            return Unauthorized("Invalid password.");

        // Generate JWT
        var token = GenerateJwtToken(user);
        return Ok(new { Token = token });
    }

    private string GenerateJwtToken(User user)
    {
        var claims = new[]
        {
            new System.Security.Claims.Claim("Id", user.Id),
            new System.Security.Claims.Claim("Username", user.Username)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
