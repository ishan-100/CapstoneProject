using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IdentityWebAPI.Services   // ✅ ADD THIS
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AuthService(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        public async Task<(int status, string message)> RegisterAsync(RegisterModel model)
        {
            var existingUser = await _userManager.FindByEmailAsync(model.Email);
            if (existingUser != null)
                return (0, "User already exists");

            var user = new ApplicationUser
            {
                UserName = model.UserName,
                Email = model.Email,
                PhoneNumber = model.MobileNumber,
                UserPreference = model.UserPreference
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return (0, string.Join(", ", result.Errors.Select(e => e.Description)));

            if (!await _roleManager.RoleExistsAsync(model.Role))
                await _roleManager.CreateAsync(new IdentityRole(model.Role));

            await _userManager.AddToRoleAsync(user, model.Role);

            return (1, "User registered successfully");
        }

        public async Task<(int status, string token)> LoginAsync(LoginModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return (0, "");

            if (!await _userManager.CheckPasswordAsync(user, model.Password))
                return (0, "");

            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName ?? ""),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var secret = _configuration["Jwt:Secret"]
                         ?? throw new Exception("JWT Secret missing");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:ValidIssuer"],
                audience: _configuration["Jwt:ValidAudience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(3),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

            return (1, new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}