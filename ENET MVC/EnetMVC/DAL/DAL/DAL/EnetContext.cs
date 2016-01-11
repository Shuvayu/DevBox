using System;
using System.Data.Entity;

namespace DAL
{
    public class EnetContext : DbContext
    {

        public EnetContext()
            : base("Data Source=localhost;Initial Catalog=enetMVC;Integrated Security=True;MultipleActiveResultSets=True")
        {
            //Data Source=119.9.22.28;Initial Catalog=enetMVC;User ID=enet;Password=Galileo1
        }


        
        public DbSet<Models.Package> Packages { get; set; }

         public DbSet<Models.Medicine> Medicines { get; set; }

         public DbSet<Models.User> Users { get; set; }

         public DbSet<Models.PackageTransactions> PackageTransactionses { get; set; }
         public DbSet<Models.Distribution> Distributions { get; set; }
         public DbSet<Models.DistributionCenter> DistributionCenter { get; set; }
         public DbSet<Models.PackageStatus> PackageStatuses { get; set; }
         public DbSet<Models.Role> Roles { get; set; }
         

    }
}
