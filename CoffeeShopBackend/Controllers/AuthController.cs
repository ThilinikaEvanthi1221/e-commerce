using Microsoft.AspNetCore.Mvc;
using CoffeeShopBackend.Services;
using CoffeeShopBackend.Models;
using BCrypt.Net;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Configuration;

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
        // Check if username already exists
        if (await _userService.GetUserByUsernameAsync(user.Username) != null)
            return BadRequest("Username already exists.");

        // Hash the password using BCrypt
        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);

        // Add user to the database
        await _userService.AddUserAsync(user);
        return Ok("User registered successfully.");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] User loginDetails)
    {
        // Retrieve the user from the database by username
        var user = await _userService.GetUserByUsernameAsync(loginDetails.Username);
        if (user == null)
            return Unauthorized("Invalid username.");

        // Verify the provided password against the stored password hash
        if (!BCrypt.Net.BCrypt.Verify(loginDetails.PasswordHash, user.PasswordHash))
            return Unauthorized("Invalid password.");

        // Generate JWT token upon successful login
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
