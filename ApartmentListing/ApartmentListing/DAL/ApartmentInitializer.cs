using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using ApartmentListing.DAL.Models;

namespace ApartmentListing.DAL
{
    public class ApartmentInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<ApartmentDataContext>
    {
        protected override void Seed(ApartmentDataContext dbContext) 
        {
            var apartments = new List<Apartment>
            {
                new Apartment{Address="7 Main Street, Bondi, 2026",City="Sydney",Suburb="Bondi",Rooms=2,Bathrooms=1,Carports=1},
                new Apartment{Address="14 Balmain Road, Lilyfield, 2040",City="Sydney",Suburb="Lilyfield",Rooms=1,Bathrooms=1,Carports=0},
                new Apartment{Address="21 Kent Street, Sydney, 2000",City="Sydney",Suburb="Sydney",Rooms=2,Bathrooms=1.5,Carports=1},
                new Apartment{Address="5 Oakley Road, Bondi, 2028",City="Sydney",Suburb="Bondi",Rooms=1,Bathrooms=1,Carports=0},
                new Apartment{Address="7 Ocean Lane, St Kilda, 3182",City="Melbourne",Suburb="St Kilda",Rooms=3,Bathrooms=2,Carports=1},
                new Apartment{Address="52 Blair Street, Bondi, 2028",City="Sydney",Suburb="Bondi",Rooms=4,Bathrooms=2,Carports=2},
                new Apartment{Address="66 Pitt Street, Sydney, 2000",City="Sydney",Suburb="Sydney",Rooms=1,Bathrooms=1,Carports=1},
                new Apartment{Address="4 Hill Street, St Kilda, 3182",City="Melbourne",Suburb="St Kilda",Rooms=2,Bathrooms=1,Carports=1},
                new Apartment{Address="2 Glover Street, Lilyfield, 2040",City="Sydney",Suburb="Lilyfield",Rooms=3,Bathrooms=2,Carports=1},
                new Apartment{Address="100 Elizabeth Street, Sydney, 2000",City="Sydney",Suburb="Sydney",Rooms=1,Bathrooms=1,Carports=0},
                new Apartment{Address="309 Kent Street, Sydney, 2000",City="Sydney",Suburb="Sydney",Rooms=1,Bathrooms=1,Carports=2}
            };

            apartments.ForEach(s => dbContext.Apartments.Add(s));
            dbContext.SaveChanges();
        }
    }
}