using System;
using System.Collections.Generic;
using System.Linq;
using FORUMAPPLICATION.DATAMODELS;

namespace FORUMAPPLICATION.REPOSITORIES
{
    public interface IUserRepository
    {
        void InsertUser(User u);
        void UpdateUserDetails(User u);
        void UpdateUserPassword(User u);
        void DeleteUser(int userid);    
        List<User>GetUsers();   
       List<User> GetUserByEmailAndPassword(string email, string password);
        List<User> GetUserByEmail(string email);
        List<User> GetUserByUserID(int UserID);
        int GetLatestUserID();      
    }
    public class UserRepository :IUserRepository 
    {
        private ForumAppDbContext _DbContext;
        public UserRepository()
        {
            _DbContext = new ForumAppDbContext();
        }
        public void InserUser(User u)
        {
            _DbContext.users.Add(u);
            _DbContext.SaveChanges();
        }
        public void UpdateUserDetails(User u)
        {
            try
            {
                if(u!=null&&u.UserID>0)
                {
                    User user= _DbContext.users.Where(x =>x.UserID==u.UserID).FirstOrDefault();
                    if (user != null) 
                    {
                        user.Name=u.Name;
                        user.Mobile=u.Mobile;
                        _DbContext.SaveChanges();
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public void UpdateUserPassword(User u)
        {
            User user = _DbContext.users.Find(u.UserID);
            user.PasswordHash=u.PasswordHash;
            _DbContext.SaveChanges();
        }
        public void DeleteUser(int uid)
        {
            _DbContext.users.Remove(_DbContext.users.Find(uid));
            _DbContext.SaveChanges();
        }
        public List<User>GetUsers() 
        {
            return _DbContext.users.ToList();
        }
        public User GetUserByEmailANdPassword(String email, string password)
        {
            User u= _DbContext.users.Where(x=>x.Email==email&&x.PasswordHash==password).FirstOrDefault();
            return u;
        }
        public User GetUserByEmail(string email)
        {
            User u= _DbContext.users.Where(x=>x.Email==email).FirstOrDefault();  
            return u;
        }
        public int GetLatestUserID()
        {
            User user= _DbContext.users.OrderByDescending(x=>x.UserID).FirstOrDefault();
            return user.UserID;
        }

        public void InsertUser(User u)
        {
            _DbContext.users.Add(u);
            _DbContext.SaveChanges();
        }

        List<User> IUserRepository.GetUserByEmail(string email)
        {
            throw new NotImplementedException();
        }

        

        public List<User> GetUserByUserID(int UserID)
        {
            
            List<User> us = _DbContext.users.Where(temp => temp.UserID == UserID).ToList();
            return us;
            
        }

        public List<User> GetUserByEmailAndPassword(string email, string passwordHash)
        {
            List<User> us = _DbContext.users.Where(temp => temp.Email == email && temp.PasswordHash == passwordHash).ToList();
            return us;
        }
    }
}
