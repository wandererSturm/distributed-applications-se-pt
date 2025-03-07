using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PS.ApplicationServices.Interfaces;
using PS.ApplicationServices.Messaging;
using PS.Data.Entities;
using PS.Infrastructure.Messaging.Requests;
using PS.Infrastructure.Messaging.Responses;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PS.ApplicationServices.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AuthService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager,
            IConfiguration configuration) 
        { 
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        private string GenerateToken(IEnumerable<Claim> claims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Authentication:TokenKey"]));
            var _TokenExpiryTimeInHour = Convert.ToInt64(_configuration["Authentication:TokenExpiryTimeInHour"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _configuration["Authentication:Issuer"],
                Audience = _configuration["Authentication:Audience"],
                Expires = DateTime.UtcNow.AddHours(_TokenExpiryTimeInHour),
                SigningCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256),
                Subject = new ClaimsIdentity(claims),
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<LoginResponse> Login(string userName, string password)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                return new()
                {
                    Id = "",
                    StatusCode = BusinessStatusCodeEnum.InvalidUserName,
                    Token = ""
                };
            }
            if (await _userManager.CheckPasswordAsync(user, password) == false)
            {
                return new()
                {
                    Id = "",
                    StatusCode = BusinessStatusCodeEnum.InvalidPassword,
                    Token = ""
                };
            }

            var userRoles = await _userManager.GetRolesAsync(user);
            var authClaims = new List<Claim> {
                new Claim(ClaimTypes.Name, userName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole) );
            }
            string token = GenerateToken(authClaims);

            return new()
            {
                StatusCode = BusinessStatusCodeEnum.Success,
                Id = user.Id,
                Email = user.Email,
                Token = token
            };

        }

        public async Task<RegistrationResponse> RegistrationAsync(RegistrationRequest model, string role)
        {
            var userExists = await _userManager.FindByNameAsync(model.UserName);
            if (userExists != null)
            {
                return new()
                {
                    IsOk = true,
                    Message = "User already exists"
                };
            }

            ApplicationUser user = new()
            { 
                UserName = model.UserName,
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),                
            };

            var creatUserResult = await _userManager.CreateAsync(user, model.Password);
            if (creatUserResult.Succeeded == false)
            {
                string errors = "";
                foreach(var error in creatUserResult.Errors)
                {
                    errors+=$" (error.Description)";
                }
                return new()
                {
                    IsOk = false,
                    Message = $"User creation failed! Please check user details and try again.{errors}"
                };
            }

            if(await _roleManager.RoleExistsAsync(role) == false)
            {
                await _roleManager.CreateAsync(new IdentityRole(role));
            }

            if(await _roleManager.RoleExistsAsync(role))
            {
                await _userManager.AddToRoleAsync(user, role);
            }
            return new()
            {
                IsOk = true,
                Message = "User created successfully."
            };            
        }
    }
}
