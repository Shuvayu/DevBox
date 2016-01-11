using System.Collections.Generic;


namespace DAL.Models
{
    /// <summary>
    ///     DC Details
    /// </summary>
    public class DistributionCenter
    {
        public int DistributionCenterId { get; set; }
        public int PhoneNumber { get; set; }
        public string Name { get; set; }
        public bool IsHead { get; set; }
        public string Address { get; set; }


        public virtual ICollection<User> Users { get; set; }


    }
}