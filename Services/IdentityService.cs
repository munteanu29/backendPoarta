﻿using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using itec_mobile_api_final.Entities;
using itec_mobile_api_final.Models;
using itec_mobile_api_final.Models.Requests;
using itec_mobile_api_final.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace itec_mobile_api_final.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<User> _userManager;
        private readonly JwtOptions _jwtOptions;

        public IdentityService(UserManager<User> userManager, JwtOptions jwtOptions)
        {
            _userManager = userManager;
            _jwtOptions = jwtOptions;
        }

        public async Task<AuthenticationResult> RegisterAsync(UserRegistrationRequest request)
        {
            var existingUser = await _userManager.FindByEmailAsync(request.Email);
            if (existingUser != null)
            {
                return new AuthenticationResult
                {
                    Errors = new[] {"User with this email address already exists."}
                };
            }
            
            var newUser = new User
            {
                Email = request.Email,
                UserName = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
            };

            var createdUser = await _userManager.CreateAsync(newUser, request.Password);

            if (!createdUser.Succeeded)
            {
                return new AuthenticationResult
                {
                    Errors = createdUser.Errors.Select(x => x.Description)
                };
            }

            return GenerateAuthenticationResult(newUser);
        }

        public async Task<AuthenticationResult> LoginAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user is null)
            {
                return new AuthenticationResult
                {
                    Errors = new[] {"User does not exist"}
                };
            }

            var isValidPassword = await _userManager.CheckPasswordAsync(user, password);
            if (!isValidPassword)
            {
                return new AuthenticationResult
                {
                    Errors = new[] {"User/password combination is wrong"}
                };
            }

            return GenerateAuthenticationResult(user);
        }

        public async Task<AuthenticationResult> UpdateAsync(User currentUser ,UserUpdateRequest request)
        {
            currentUser.FirstName = request.FirstName;
            currentUser.LastName = request.LastName;
            
            await _userManager.UpdateAsync(currentUser);
            
            return GenerateAuthenticationResult(currentUser);
        }

        private AuthenticationResult GenerateAuthenticationResult(User newUser)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtOptions.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, newUser.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, newUser.Email),
                    new Claim("id", newUser.Id),
                }),
                Expires = DateTime.UtcNow.AddDays(60),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new AuthenticationResult
            {
                Success = true,
                Token = tokenHandler.WriteToken(token)
            };
        }
    }
}