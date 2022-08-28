using CleanArchitecture.Applicattion.Constants;
using CleanArchitecture.Applicattion.Contracts.Identity;
using CleanArchitecture.Applicattion.Models.Identity;
using CleanArchitecture.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CleanArchitecture.Identity.Services
{
    internal class AuthServices : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JwtSettings _jwtSettings;

        public AuthServices(UserManager<ApplicationUser> userManager,
                                        SignInManager<ApplicationUser> signInManager,
                                        JwtSettings jwtSettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtSettings = jwtSettings;
        }

        public async Task<AuthResponse> Login(AuthRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user is null)
                throw new Exception($"El usuario con el email {request.Email} no existe");

            var resultado = await _signInManager.PasswordSignInAsync(user.UserName,
                                                                     request.Password,
                                                                     false,
                                                                     lockoutOnFailure: false);
            if (!resultado.Succeeded)
                throw new Exception($"Las credenciales son incorrectas");

            var token = await GeneratoToken(user);

            var authResponse = new AuthResponse
            {
                Id = user.Id,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Email = request.Email,
                UserName = user.UserName
            };

            return authResponse;
        }

        public async Task<RegistrationResponse> Register(RegistrationRequest request)
        {
            var existingUser = await _userManager.FindByNameAsync(request.UserName);

            if (existingUser is not null)
                throw new Exception($"El username ya se encuentra registrado");

            var existingEmail = await _userManager.FindByEmailAsync(request.Email);

            if (existingEmail is not null)
                throw new Exception($"El email ya se encuentra registrado");

            var user = new ApplicationUser
            {
                Email = request.Email,
                Nombre = request.Nombre,
                Apellidos = request.Apellidos,
                UserName = request.UserName,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user);

            var token = await GeneratoToken(user);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Operator");
                return new RegistrationResponse
                {
                    Email = user.Email,
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    UserName = user.UserName,
                    Id = user.Id
                };
            }

            throw new Exception($"{result.Errors}");
        }

        private async Task<JwtSecurityToken> GeneratoToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);

            var roles = await _userManager.GetRolesAsync(user);

            var roleClaims = new List<Claim>();

            foreach (var role in roles)
            {
                roleClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(CustomClaimTypes.Uid, user.Id),
            }.Union(userClaims).Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey( Encoding.UTF8.GetBytes(_jwtSettings.Key));

            var signingCredentials = new SigningCredentials(symmetricSecurityKey,SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                                       issuer: _jwtSettings.Issuer,
                                       audience: _jwtSettings.Audience,
                                       claims: claims,
                                       expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                                       signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }
    }
}