using BlogAPI.Models.Models;
using Edu.Repository;
using Edu.UnitOfWork;
using Edu.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BlogAPI.Services.Services
{
    public class AuthService
    {
        private readonly IRepository<User, DbContext> _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IUnitOfWork<DbContext> _unitOfWork;
        private readonly IConfiguration _configuration;

        public AuthService(
            IRepository<User, DbContext> userRepository, 
            IPasswordHasher<User> passwordHasher, 
            IConfiguration configuration, 
            IUnitOfWork<DbContext> unitOfWork
            )
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }

        public ServiceResponse<string> Login(string username, string password)
        {
            try
            {
                var user = _userRepository.Find(x => x.UserName == username);
                if (user == null)
                {
                    return new ServiceResponse<string> { Success = false, Message = "El usuario no fue encontrado" };
                }

                var isValidPassword = _passwordHasher.VerifyHashedPassword(user, user.Password, password);
                if (isValidPassword == PasswordVerificationResult.Failed)
                {
                    return new ServiceResponse<string> { Success = false, Message = "El usuario o la contraseña son incorrectos" };
                }

                var jsonToken = CreateToken(user);
                return new ServiceResponse<string> { Success = true, Data = jsonToken };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<string> { Success = true, Message = ex.Message };
            }
        }

        public ServiceResponse<string> CreateUser(User user, string password)
        {
            try
            {
                var hashedPassword = _passwordHasher.HashPassword(user, password);
                var newUser = user;
                user.Password = hashedPassword;
                _userRepository.Add(newUser);
                _unitOfWork.Save();
                return new ServiceResponse<string> { Success = true, Message = "Usuario creado con exito!" };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<string> { Success = false, Message = ex.Message.ToString() };
            }

        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value)
            );

            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credentials
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            
            return tokenHandler.WriteToken(token);
        }
    }
}
