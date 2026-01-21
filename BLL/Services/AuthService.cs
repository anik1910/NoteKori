using BCrypt.Net;
using BLL.DTOs;
using DAL;
using DAL.EF.Models;
using DAL.Interfaces;
using DAL.Repos;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Services
{
    public class AuthService
    {
        DataAccessFactory factory;
        public AuthService(DataAccessFactory factory)
        {
            this.factory = factory;
        }
        public (bool Success, string Error) Register(RegisterDTO dto)
        {
            var existing = factory.UserData().GetByEmail(dto.Email);
            if (existing != null)
            {
                return (false, "Email already registered");
            }

            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password)
            };

            var created = factory.UserData().Create(user);
            if (!created)
            {
                return (false, "Failed to create user");
            }

            return (true, null);
        }
        public UserDTO Login(LoginDTO dto)
        {
            var user = factory.UserData().GetByEmail(dto.Email);
            if (user == null)
            {
                return null;
            }
                
            if (string.IsNullOrEmpty(user.PasswordHash))
            {
                return null;
            }

            var valid = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);
            if (!valid)
            {
                return null;
            }

            var mapper = MapperConfig.GetMapper();
            var userDto = mapper.Map<UserDTO>(user);
            return userDto;
        }
    }
}
