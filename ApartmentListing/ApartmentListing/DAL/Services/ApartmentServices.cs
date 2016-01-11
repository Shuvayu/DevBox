using ApartmentListing.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApartmentListing.DAL.Services
{
    public class ApartmentServices 
    {
        private ApartmentDataContext db = new ApartmentDataContext();

        public ICollection<Apartment> GetAllFormated() 
        {
            var apartments = from a in db.Apartments
                             select a;
            apartments = apartments.OrderBy(a => a.City).ThenBy(b => b.Suburb);

            return apartments.ToList();
        }

        public  Apartment Details(long Id)
        {
            var apartment = db.Apartments.Find(Id);
            return apartment;
        }

        public ICollection<Apartment> GetAllSearched(Apartment apartment) 
        {
            var apartments = from a in db.Apartments
                             select a;

            if (apartment.Address != null)
            {
                apartments = apartments.Where(s => s.Address.Contains(apartment.Address));
            }

            if (apartment.City != null)
            {
                apartments = apartments.Where(s => s.City.Contains(apartment.City));
            }

            if (apartment.Suburb != null)
            {
                apartments = apartments.Where(s => s.Suburb.Contains(apartment.Suburb));
            }

                apartments = apartments.Where(s => s.Rooms.ToString().Contains(apartment.Rooms.ToString()));

                apartments = apartments.Where(s => s.Bathrooms.ToString().Contains(apartment.Bathrooms.ToString()));
 
                apartments = apartments.Where(s => s.Carports.ToString().Contains(apartment.Carports.ToString()));
                                 
            return apartments.ToList();
        }
    }
}