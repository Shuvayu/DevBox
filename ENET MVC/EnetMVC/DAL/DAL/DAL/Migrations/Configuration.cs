using System.Runtime.Remoting.Contexts;
using DAL.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DAL.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DAL.EnetContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

       

        protected override void Seed(DAL.EnetContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            context.Roles.AddOrUpdate(
              p => p.RoleName,
              new Role { RoleName = "Admin" },
              new Role { RoleName = "Agent" },
              new Role { RoleName = "Manager" },
              new Role { RoleName = "Doctor" }
           
            );
            context.PackageStatuses.AddOrUpdate(
               p => p.PackageStatusId,
               new PackageStatus { PackageStatusId = 1, TransitState = "Stock" },
               new PackageStatus { PackageStatusId = 2, TransitState = "Discarded" },
               new PackageStatus { PackageStatusId = 3, TransitState = "Lost" },
               new PackageStatus { PackageStatusId = 4, TransitState = "Distributed" },
               new PackageStatus { PackageStatusId = 5, TransitState = "InTransit" }
               );


          
        }
    }
}
