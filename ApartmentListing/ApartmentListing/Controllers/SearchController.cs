using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ApartmentListing.DAL.Models;
using ApartmentListing.DAL.Services;

namespace ApartmentListing.Controllers
{
    public class SearchController : Controller
    {
        private ApartmentServices apartmentServices = new ApartmentServices();

        //
        // GET: /Search/
        [HttpGet]
        public ActionResult Index()
        {
            var values = Session["urlLog"] as List<string>;
            var url = Request.Url.ToString();
            values.Add(url);
            Session["urlLog"] = values;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Apartment apartment)
        {
          
            if (ModelState.IsValid)
            {
                var allSearchedApartments = apartmentServices.GetAllSearched(apartment);
                TempData["searchedList"] = allSearchedApartments;
                TempData["searchCriteriaAddress"] = apartment.Address;
                TempData["searchCriteriaCity"] = apartment.City;
                TempData["searchCriteriaSuburb"] = apartment.Suburb;
                return RedirectToAction("ViewSearchResult");
            }
           
            return View();
        }

        public ActionResult ViewSearchResult() 
        {
            var values = Session["urlLog"] as List<string>;
            var url = Request.Url.ToString();
            values.Add(url);
            Session["urlLog"] = values;
            var searchResults = TempData["searchedList"];
            ViewBag.searchedAddress = TempData["searchCriteriaAddress"];
            ViewBag.searchedCity = TempData["searchCriteriaCity"];
            ViewBag.searchedSuburb = TempData["searchCriteriaSuburb"];
            return View(searchResults);
        }
	}
}