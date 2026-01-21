using DAL.EF;
using DAL.EF.Models;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repos
{
    public class UserRepo : IUserRepository
    {
        NMSContext db;
        public UserRepo(NMSContext db)
        {
            this.db = db;
        }
        public bool Create(User u)
        {
            db.Users.Add(u);
            return db.SaveChanges() > 0;
        }
        public List<User> Get()
        {
            return db.Users.ToList();
        }
        public User Get(int id)
        {
            return db.Users.Find(id);
        }
        public User GetByEmail(string email)
        {
            return db.Users.FirstOrDefault(u => u.Email == email);
        }
        public bool Update(User entity)
        {
            db.Users.Update(entity);
            return db.SaveChanges() > 0;
        }
        public bool Delete(int id)
        {
            var u = db.Users.Find(id);
            if (u == null)
            {
                return false;
            }
            db.Users.Remove(u);
            return db.SaveChanges() > 0;
        }
    }
}
