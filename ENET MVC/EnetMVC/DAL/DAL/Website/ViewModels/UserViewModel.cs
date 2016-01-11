using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using DAL.Models;

namespace Website.ViewModels
{
    /// <summary>
    ///     Model User
    /// </summary>
    public class UserViewModel
    {
      
        [Required]
        public string UserName { get; set; }
        public int UserId { get; set; }
        [Required][PasswordPropertyText]
        public string Password { get; set; }
        [Required][EmailAddress]
        public string Email { get; set; }
        [Required]
        public Role Role { get; set; }
        public int RoleId { get; set; }
        [Required]
        public string FullName { get; set; }

        public virtual DistributionCenterViewModel DistributionCenterViewModel { get; set; }

        public int DistributionCenterId { get; set; }




    }
}