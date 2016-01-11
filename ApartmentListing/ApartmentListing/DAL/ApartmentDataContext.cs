using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using ApartmentListing.DAL.Models;

namespace ApartmentListing.DAL
{
    public class ApartmentDataContext : DbContext
    {
        public DbSet<Apartment> Apartments { get; set; }

        public ApartmentDataContext() : base("ApartmentContext")
        { }

    }
}