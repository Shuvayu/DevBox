using ConsumingAWebServiceDemo.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using ConsumingAWebServiceDemo.BAL;

namespace ConsumingAWebServiceDemo.Controllers
{
    public class WeatherController : Controller
    {
        // GET: Weather
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Weather weatherCountry) 
        {
            if (ModelState.IsValid)
            {
                GetWeatherData data = new GetWeatherData();

                // Calling a function to retrive the data from the web Service
                List<string> cityNames = data.GetAllCities(weatherCountry.country);

                if (cityNames != null)
                {
                    //Transfering Data between Actions
                    TempData["cityList"] = cityNames;
                    Session["Country"] = weatherCountry.country;

                    return RedirectToAction("GetTheWeatherOfCity");
                }

                return RedirectToAction("ErrorPage");
            }
           
            return View();
        }


        [HttpGet]
        public ActionResult GetTheWeatherOfCity() 
        {
            var cityList = (List<string>)TempData["cityList"];

            // creating a dropdown select list item to display a drop down list
            List<SelectListItem> dropDownList = new List<SelectListItem>(cityList.Select(s =>
                new SelectListItem { Text = s.ToString(), Value = s.ToString() }));
            var country = Session["Country"];


            ViewBag.cityList = dropDownList;
            ViewBag.country = country;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetTheWeatherOfCity(FormCollection form) 
        {
            if (ModelState.IsValid)
            {
                var country = Session["Country"] as string;
                var city = form["dropDownList"].ToString();

                GetWeatherData data = new GetWeatherData();

                // Calling a function to get the weather of the city and country selected
                WeatherReport obj = data.GetTheWeather(city,country);

                // Proceed futher only if the object of the model is not null else direct to the error page. 
                if (obj != null)
                {
                    TempData["DataSet"] = obj;

                    return RedirectToAction("WeatherReport");
                }
                return RedirectToAction("ErrorPage");
            }
            return View();
        }
        public ActionResult WeatherReport()
        {
            var dataSetModel = TempData["DataSet"];
            return View(dataSetModel);
        }

        public ActionResult ErrorPage() 
        {
            return View();
        }
        
    }
}