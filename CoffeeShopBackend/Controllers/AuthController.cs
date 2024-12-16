using Microsoft.AspNetCore.Mvc;
using CoffeeShopBackend.Services;
using CoffeeShopBackend.Models;
using BCrypt.Net;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;  // For logging

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserService _userService;
    private readonly IConfiguration _configuration;
    private readonly ILogger<AuthController> _logger;  // Inject logger

    // Constructor to initialize user service and configuration
    public AuthController(UserService userService, IConfiguration configuration, ILogger<AuthController> logger)
    {
        _userService = userService;
        _configuration = configuration;
        _logger = logger;  // Initialize logger
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] User user)
    {
        if (user == null)
        {
            return BadRequest("Invalid user data.");
        }

        // Ensure the model is valid
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState); // Return validation errors
        }

        // Check if username already exists
        var existingUser = await _userService.GetUserByUsernameAsync(user.Username);
        if (existingUser != null)
        {
            return BadRequest("Username already exists.");
        }

        // Hash the password using BCrypt (Ensure the plain password is being hashed)
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);  // Hash the plain password
        user.PasswordHash = hashedPassword; // Store the hashed password in the database

        try
        {
            // Add user to the database
            await _userService.AddUserAsync(user);
            return Ok("User registered successfully.");
        }
        catch (Exception ex)
        {
            // Log the error (you can also log it in a logging service)
            _logger.LogError(ex, "Error occurred while registering the user.");
            return StatusCode(500, "An error occurred while processing your request. Please try again later.");
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] User loginDetails)
    {
        // Retrieve the user from the database by username
        var user = await _userService.GetUserByUsernameAsync(loginDetails.Username);
        if (user == null)
        {
            return Unauthorized("Invalid username.");
        }

        // Verify the provided password against the stored password hash
        if (!BCrypt.Net.BCrypt.Verify(loginDetails.Password, user.PasswordHash))  // Compare plain password with hashed password
        {
            return Unauthorized("Invalid password.");
        }

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
