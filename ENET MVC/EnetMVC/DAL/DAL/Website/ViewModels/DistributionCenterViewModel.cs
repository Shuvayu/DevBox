using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Website.ViewModels
{
    /// <summary>
    ///     DC Details
    /// </summary>
    public class DistributionCenterViewModel
    {
        public int DistributionCenterId { get; set; }
        [Required]
        public int PhoneNumber { get; set; }
        [Required]
        public string Name { get; set; }
        public bool IsHead { get; set; }
        [Required]
        public string Address { get; set; }


        public virtual ICollection<UserViewModel> UsersViewModels { get; set; }


    }
}