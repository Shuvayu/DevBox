using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ApartmentListing.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            List<string> values = new List<string>();
            var url = Request.Url.ToString();
            values.Add(url);
            Session["urlLog"] = values;
            return View();
        }

        public ActionResult Contact()
        {
            var values = Session["urlLog"] as List<string>;
            var url = Request.Url.ToString();
            values.Add(url);
            Session["urlLog"] = values;

            ViewBag.Message = "Please provide feedback on the application";
            return View();
        }

        public ActionResult UrlLog()
        {
            var values = Session["urlLog"] as List<string>;
            var url = Request.Url.ToString();
            values.Add(url);
            Session["urlLog"] = values;
            return View(values);
        }
    }
}