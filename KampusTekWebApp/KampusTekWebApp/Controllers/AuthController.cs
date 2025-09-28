using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using KampusTek.Data;
using KampusTek.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace KampusTekWebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly KampusTekDbContext _dbContext;
        private readonly IConfiguration _configuration;

        public AuthController(KampusTekDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        public class LoginRequest
        {
            public string Email { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;
        }

        public class LoginResponse
        {
            public string AccessToken { get; set; } = string.Empty;
            public DateTime ExpiresAtUtc { get; set; }
        }

        public class RegisterRequest
        {
            public string FirstName { get; set; } = string.Empty;
            public string LastName { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public string CellNumber { get; set; } = string.Empty;
            public int UserTypeId { get; set; }
            public string Password { get; set; } = string.Empty;
        }

        public class RegisterResponse
        {
            public int UserId { get; set; }
            public string AccessToken { get; set; } = string.Empty;
            public DateTime ExpiresAtUtc { get; set; }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
            {
                return BadRequest("Email and password are required.");
            }

            var user = await _dbContext.Users
                .Include(u => u.UserType)
                .FirstOrDefaultAsync(u => u.Email == request.Email);

            if (user == null || user.PasswordHash == null || user.PasswordSalt == null)
            {
                return Unauthorized("Invalid credentials.");
            }

            if (!VerifyPassword(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return Unauthorized("Invalid credentials.");
            }

            var token = GenerateJwt(user);
            return Ok(new LoginResponse
            {
                AccessToken = token.token,
                ExpiresAtUtc = token.expiresAtUtc
            });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.FirstName) ||
                string.IsNullOrWhiteSpace(request.LastName) ||
                string.IsNullOrWhiteSpace(request.Email) ||
                string.IsNullOrWhiteSpace(request.CellNumber) ||
                string.IsNullOrWhiteSpace(request.Password) ||
                request.UserTypeId <= 0)
            {
                return BadRequest("All fields are required.");
            }

            var emailExists = await _dbContext.Users.AnyAsync(u => u.Email == request.Email);
            if (emailExists)
            {
                return Conflict("Email already in use.");
            }

            var userTypeExists = await _dbContext.UserTypes.AnyAsync(ut => ut.Id == request.UserTypeId);
            if (!userTypeExists)
            {
                return BadRequest("Invalid user type.");
            }

            CreatePasswordHash(request.Password, out var passwordHash, out var passwordSalt);

            var user = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                CellNumber = request.CellNumber,
                UserTypeId = request.UserTypeId,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();

            var token = GenerateJwt(user);
            return Ok(new RegisterResponse
            {
                UserId = user.Id,
                AccessToken = token.token,
                ExpiresAtUtc = token.expiresAtUtc
            });
        }

        private static bool VerifyPassword(string password, byte[] storedHash, byte[] storedSalt)
        {
            using var hmac = new HMACSHA512(storedSalt);
            var computed = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return computed.SequenceEqual(storedHash);
        }

        private (string token, DateTime expiresAtUtc) GenerateJwt(User user)
        {
            var jwtSection = _configuration.GetSection("Jwt");
            var issuer = jwtSection["Issuer"]!;
            var audience = jwtSection["Audience"]!;
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSection["Key"]!));

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                new Claim(ClaimTypes.Role, user.UserType?.Name ?? user.UserTypeId.ToString())
            };

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiresMinutes = int.TryParse(jwtSection["AccessTokenMinutes"], out var m) ? m : 15;
            var expires = DateTime.UtcNow.AddMinutes(expiresMinutes);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return (tokenString, expires);
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }
    }
}


