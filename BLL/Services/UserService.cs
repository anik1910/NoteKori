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
    public class UserService
    {
        DataAccessFactory factory;
        public UserService(DataAccessFactory factory)
        {
            this.factory = factory;
        }
        public List<UserDTO> Get()
        {
            var data = factory.UserData().Get();
            var mapper = MapperConfig.GetMapper();
            var ret = mapper.Map<List<UserDTO>>(data);
            return ret;
        }
        public UserDTO Get(int id)
        {
            var data = factory.UserData().Get(id);
            if (data == null)
            {
                return null;
            }
            var mapper = MapperConfig.GetMapper();
            var ret = mapper.Map<UserDTO>(data);
            return ret;
        }
        public bool Delete(int id)
        {
            return factory.UserData().Delete(id);
        }
        public (bool Success, string Error) Recharge(int id, RechargeDTO dto)
        {
            if (dto == null || dto.Amount <= 0)
            {
                return (false, "Invalid amount");
            }

            var user = factory.UserData().Get(id);
            if (user == null)
            {
                return (false, "User not found");
            }

            user.Balance = user.Balance + dto.Amount;
            var ok = factory.UserData().Update(user);
            if (ok)
            {
                return (true, null);
            }
            else
            {
                return (false, "Failed to update balance");
            }
        }
        public decimal? GetBalance(int id)
        {
            var user = factory.UserData().Get(id);
            if (user == null)
            {
                return null;
            }
            return user.Balance;
        }
    }
}
