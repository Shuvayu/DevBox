using ConsumingAWebServiceDemo.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ConsumingAWebServiceDemo.Tests.Controllers
{
    [TestClass]
    public class WeatherControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            WeatherController controller = new WeatherController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void WeatherReport()
        {
            // Arrange
            WeatherController controller = new WeatherController();

            // Act
            ViewResult result = controller.WeatherReport() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void ErrorPage()
        {
            // Arrange
            WeatherController controller = new WeatherController();

            // Act
            ViewResult result = controller.ErrorPage() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
