using ConsumingAWebServiceDemo.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;

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
                // Calling a function to retrive the data from the web Service
                List<string> cityNames = GetAllCities(weatherCountry.country);

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

                // Calling a function to get the weather of the city and country selected
                WeatherReport obj = GetTheWeather(city,country);

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

        [NonAction]
        private List<string> GetAllCities(string country)
        {
            List<string> cityNames = new List<string>();

            // Creating object of WeatherService proxy class and calling GlobalWeatherSoap12 endpoint
            WeatherService.GlobalWeatherSoapClient client = new WeatherService.GlobalWeatherSoapClient("GlobalWeatherSoap12");
            // Invoke service method through service proxy
            var allCountryCities = client.GetCitiesByCountry(country);

            if (allCountryCities.ToString() == "Data Not Found")
            {
                return null;
            }

            DataSet ds = new DataSet();

            //Creating a stringReader object with Xml Data
            StringReader stringReader = new StringReader(allCountryCities);

            // Xml Data is read and stored in the DataSet object
            ds.ReadXml(stringReader);

            //Adding all city names to the List objects
            foreach (DataRow item in ds.Tables[0].Rows)
            {
                cityNames.Add(item["City"].ToString());
            }

            stringReader.Dispose();

            return cityNames;
        }

        [NonAction]
        private WeatherReport GetTheWeather(string city,string country) 
        {
            // Creating object of WeatherService proxy class and calling GlobalWeatherSoap12 endpoint
            WeatherService.GlobalWeatherSoapClient clientObject = new WeatherService.GlobalWeatherSoapClient("GlobalWeatherSoap12");

            // Invoke service method through service proxy
            var weatherReportXmlObject = clientObject.GetWeather(city, country);

            if (weatherReportXmlObject.ToString() == "Data Not Found")
            {
                return null;
            }
            DataSet ds = new DataSet();

            //Creating a stringReader object with Xml Data
            StringReader stringReader = new StringReader(weatherReportXmlObject);

            // Xml Data is read and stored in the DataSet object
            ds.ReadXml(stringReader);

            // Assigning the data to an object of the model class and checking if the Dataset contains the perticular field. 
            WeatherReport obj = new WeatherReport();

            if (ds.Tables[0].Columns.Contains("Location"))
            {
                obj.location = ds.Tables[0].Rows[0]["Location"].ToString();   
            }
            if (ds.Tables[0].Columns.Contains("Time"))
            {
                obj.time = ds.Tables[0].Rows[0]["Time"].ToString();
            }
            if (ds.Tables[0].Columns.Contains("Wind"))
            {
                obj.wind = ds.Tables[0].Rows[0]["Wind"].ToString();
            }
            if (ds.Tables[0].Columns.Contains("Visibility"))
            {
                obj.visibility = ds.Tables[0].Rows[0]["Visibility"].ToString();
            }
            if (ds.Tables[0].Columns.Contains("SkyConditions"))
            {
                obj.skyConditions = ds.Tables[0].Rows[0]["SkyConditions"].ToString();
            }
            if (ds.Tables[0].Columns.Contains("Temperature"))
            {
                obj.temperature = ds.Tables[0].Rows[0]["Temperature"].ToString();
            }
            if (ds.Tables[0].Columns.Contains("Dewpoint"))
            {
                obj.dewpoint = ds.Tables[0].Rows[0]["Dewpoint"].ToString();
            }
            if (ds.Tables[0].Columns.Contains("RelativeHumidity"))
            {
                obj.relativeHumidity = ds.Tables[0].Rows[0]["RelativeHumidity"].ToString();
            }
            if (ds.Tables[0].Columns.Contains("Pressure"))
            {
                obj.pressure = ds.Tables[0].Rows[0]["Pressure"].ToString();
            }
            if (ds.Tables[0].Columns.Contains("Status"))
            {
                obj.status = ds.Tables[0].Rows[0]["Status"].ToString();
            }
            
            stringReader.Dispose();

            return obj;
        } 
    }
}