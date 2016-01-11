using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    /// <summary>
    ///     Model User
    /// </summary>
    public class User
    {
        public int UserId { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required][EmailAddress]
        public string Email { get; set; }
        [Required]
        public string FullName { get; set; }
        public virtual DistributionCenter DistributionCenter { get; set; }
        public int DistributionCenterId { get; set; }
        public virtual Role Role { get; set; }
        public int RoleId { get; set; }
    

    }
}