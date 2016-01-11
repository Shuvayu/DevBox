using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using DAL.Models;

namespace DAL.Services
{
    public class UserServices : BaseService
    {
        public UserServices()
            : base()
        {
        }

        public UserServices(EnetContext context) : base(context) { }


        public virtual int Add(User user)
        {
            try
            {

            
            user.DistributionCenter =
                Context.DistributionCenter.First(x => x.DistributionCenterId == user.DistributionCenterId);
            user.Role = Context.Roles.First(x => x.RoleId == user.RoleId);
            Context.Users.Add(user);
            Context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                var sb = new StringBuilder();
                var st = ex.StackTrace;
                foreach (var failure in ex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }
            }
            return user.UserId;
        }

        public virtual ICollection<User> GetAll()
        {

            var users = Context.Users.AsEnumerable().ToList();
           
            return users;
        }

        public virtual int Delete(int  userId)
        {
            var user = Context.Users.First(x => x.UserId == userId);
            Context.Users.Remove(user);
            Context.SaveChanges();
            return user.UserId;
        }


        public virtual int Update(User user)
        {
            var curr_user = Context.Users.First(x => x.UserId == user.UserId);
            curr_user.DistributionCenter.DistributionCenterId = user.DistributionCenter.DistributionCenterId;
            curr_user.Password = user.Password;
            curr_user.Email = user.Email;
            Context.SaveChanges();
            return user.UserId;
        }


        public virtual User Details(int userId)
        {
            return Context.Users.First(x => x.UserId == userId);

        }

        public virtual ICollection<Role> GetRoles()
        {
         
            return Context.Roles.ToList();
        }
    }
}
