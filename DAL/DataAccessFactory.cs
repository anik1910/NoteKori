using DAL.EF;
using DAL.EF.Models;
using DAL.Interfaces;
using DAL.Repos;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class DataAccessFactory
    {
        NMSContext db;
        public DataAccessFactory(NMSContext db)
        {
            this.db = db;
        }
        public INoteRepository NoteData()
        {
            return new NoteRepo(db);
        }

        public IUserRepository UserData()
        {
            return new UserRepo(db);
        }
        public IReviewRepository ReviewData()
        {
            return new ReviewRepo(db);
        }
        public IRepository<Note> NoteRepository()
        {
            return new NoteRepo(db);
        }

        public IRepository<User> UserRepository()
        {
            return new UserRepo(db);
        }
    }
}
