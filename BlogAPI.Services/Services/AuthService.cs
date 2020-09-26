using BlogAPI.Models.Models;
using Edu.Repository;
using Edu.UnitOfWork;
using Edu.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogAPI.Services.Services
{
    public class AuthService
    {
        private readonly IRepository<User, DbContext> _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IUnitOfWork<DbContext> _unitOfWork;

        public AuthService(IRepository<User, DbContext> userRepository, IPasswordHasher<User> passwordHasher, IUnitOfWork<DbContext> unitOfWork)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _unitOfWork = unitOfWork;
        }

        public ServiceResponse<int> Login(string username, string password)
        {
            try
            {
                var user = _userRepository.Find(x => x.UserName == username);
                if (user == null)
                {
                    return new ServiceResponse<int> { Success = false, Message = "El usuario no fue encontrado" };
                }

                var isValidPassword = _passwordHasher.VerifyHashedPassword(user, user.Password, password);
                if (isValidPassword == PasswordVerificationResult.Failed)
                {
                    return new ServiceResponse<int> { Success = false, Message = "El usuario o la contraseña son incorrectos" };
                }

                return new ServiceResponse<int> { Success = true, Data = user.Id };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<int> { Success = true, Message = ex.Message };
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
    }
}
