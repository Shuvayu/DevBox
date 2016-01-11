using System.Collections.Generic;
using System.Linq;
using DAL.Models;
using DAL.Services;

namespace Domain.Contracts
{
    public class UserContracts 
    {
        public User UserModel { get; set; }
        public UserServices UserServices { get; set; }

        public UserContracts()
        {
            UserModel= new User();
            UserServices =new UserServices();
        }

        public int Add(User user)
        {
            UserModel = (User) user;
            UserServices.Add(UserModel);
            return UserModel.UserId;
        }

     

        public int Delete(int id)
        {
            UserServices.Delete(id);
            return id;
        }

        public ICollection<User> GetAll()
        {
            return  UserServices.GetAll();
        }

        public User Get(int id)
        {
            return UserServices.GetAll().FirstOrDefault(x => x.UserId == id);
        }

        public int Update(User obj)
        {
            UserModel = (User) obj;
            UserServices.Update(UserModel);
            return UserModel.UserId;
        }

        public ICollection<Role> GetAllRoles()
        {
          return UserServices.GetRoles();
        }

        public User GetcurrentUserDetails(string username)
        {
            return  UserServices.GetAll().FirstOrDefault(x => x.UserName == username);

        }


        public string UserRole(string username)
        {
            var firstOrDefault = UserServices.GetAll().FirstOrDefault(x => x.UserName == username);
            if (firstOrDefault != null)
                return firstOrDefault.Role.RoleName;
            else
            {
                return "";
            }
        }
    }
}
