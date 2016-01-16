using ConsumingAWebServiceDemo.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ConsumingAWebServiceDemo.BAL
{
    public class GetWeatherData
    {

        public List<string> GetAllCities(string country)
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


        public WeatherReport GetTheWeather(string city, string country)
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