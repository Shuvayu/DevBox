using ApartmentListing.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ApartmentListing.DAL.Services;

namespace ApartmentListing.Controllers
{
    public class ApartmentController : Controller
    {
        private ApartmentServices apartmentServices;

        public ApartmentController() 
        { 
           apartmentServices = new ApartmentServices();
        }

        // GET: /Apartment/
        [OutputCache(Duration = 1)]
        public ActionResult Index()
        {
            var values = Session["urlLog"] as List<string>;
            var url = Request.Url.ToString();
            values.Add(url);
            Session["urlLog"] = values;
            var apartments = apartmentServices.GetAllFormated();
            return View(apartments);
        }

        [OutputCache(Duration = 1)]
        public ActionResult FindByID(long Id) 
        {
            var values = Session["urlLog"] as List<string>;
            var url = Request.Url.ToString();
            values.Add(url);
            Session["urlLog"] = values;
            var apartment = apartmentServices.Details(Id);
            return View(apartment);
        }
	}
}