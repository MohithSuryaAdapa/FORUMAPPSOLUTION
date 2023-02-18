using AutoMapper;
using FORUMAPPLICATION.DATAMODELS;
using FORUMAPPLICATION.REPOSITORIES;
using FORUMAPPLICATION.ServiceLayer;
using FORUMAPPLICATION.VIEWMODEL;
using System.Collections.Generic;
using System.Linq;

namespace FORUMAPPLICATION.SERVICELAYER
{
    public interface IUserService
    {
        int InsertUser(RegisterViewModel rvm);
        void UpdateUserDetails(EditUserDetailsViewModel uvm);
        void UpdateUserPassword(EditUserPasswordViewModel uvm);
        void DeleteUser(int uid);
        List<UserViewModel>GetUsers();
        UserViewModel GetUserByEmailAndPassword(string email, string password);
        UserViewModel GetUserByEmail(string email);

        UserViewModel GetUserByUserID(int UserID);
    }
    public class UserService : IUserService
    {
        IUserRepository _ur;
        public UserService()
        {
            _ur = new UserRepository();
        }
        public void DeleteUser(int uid)
        {
            _ur.DeleteUser(uid);
        }
        public UserViewModel GetUserByEmail(string email)
        {
            User u= _ur.GetUserByEmail(email).FirstOrDefault();
            var config = new MapperConfiguration(cfg => 
            {
                cfg.CreateMap<User, UserViewModel>();
                cfg.IgnorUnmapped();
            });
            IMapper mapper = config.CreateMapper();
            UserViewModel uvm=mapper.Map<User, UserViewModel>(u);
            return uvm;
        }
        public UserViewModel GetUserByEmailAndPassword(string email, string password) 
        {
            User u = _ur.GetUserByEmailAndPassword(email, SHA256HashGenerator.GenerateHash(password)).FirstOrDefault();
            UserViewModel uvm = null;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserViewModel>();
                cfg.IgnorUnmapped();
            });
            IMapper mapper= config.CreateMapper();
             uvm=mapper.Map<User , UserViewModel>(u);
            return uvm;
        }

        public UserViewModel GetUserByUserID(int  UserID)
        {
            User u = _ur.GetUserByUserID(UserID).FirstOrDefault();
            UserViewModel uvm = null;
            if (u!=null)
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<User, UserViewModel>();
                    cfg.IgnorUnmapped();
                });
                     IMapper mapper = config.CreateMapper();
                      uvm= mapper.Map<User , UserViewModel>(u);
            }
            return uvm;
        }

        public List<UserViewModel>GetUsers() 
        {
            List<User> u = _ur.GetUsers();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserViewModel>();
                cfg.IgnorUnmapped();
            });
            IMapper mapper= config.CreateMapper();
            List<UserViewModel> users = Mapper.Map<List<User>, List<UserViewModel>>(u);
            return users;
        }
        public int InsertUser(RegisterViewModel rvm)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<RegisterViewModel, User>();
                cfg.IgnorUnmapped();
            });
            IMapper mapper=config.CreateMapper();
            User u = mapper.Map<RegisterViewModel, User>(rvm);
            _ur.InsertUser(u);
            return _ur.GetLatestUserID();
        }
        public void UpdateUserDetails(EditUserDetailsViewModel uvm)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<EditUserDetailsViewModel, User>();
                cfg.IgnorUnmapped();
            });    
            IMapper mapper=config.CreateMapper();
            User u = mapper.Map<EditUserDetailsViewModel, User>(uvm);
            _ur.UpdateUserDetails(u);
        }

        public void UpdateUserPassword(EditUserPasswordViewModel uvm)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<EditUserPasswordViewModel, User>();
                cfg.IgnorUnmapped();
            });
            IMapper mapper=config.CreateMapper();
            User u = mapper.Map<EditUserPasswordViewModel,User>(uvm);
            _ur.UpdateUserPassword(u);
        }


        
    }

}
