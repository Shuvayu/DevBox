using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNet.Identity;

using Microsoft.Owin.Security;
using Website;
using Website.Models;
using Website.Controllers;
using DAL.Models;
using Domain;
using Website.ViewModels;
using Domain.Contracts;
using AutoMapper;

namespace Website.Tests.Controllers
{
    [TestClass]
    public class AccountControllerTests
    {
        AccountController accountController = new AccountController();
        RegisterViewModel registerViewModel = new RegisterViewModel();
        LoginViewModel loginViewModel = new LoginViewModel();


        [TestMethod]
        public void RegisterUserTest()
        {
            registerViewModel.UserName = "RegisterTest";
            registerViewModel.Password = "RegisterPass";
            registerViewModel.ConfirmPassword = "RegisterTest";
            registerViewModel.FullName = "RegisterFull";
            registerViewModel.EmailAddress = "register@gmail.com";
            registerViewModel.RoleId = 1;
            registerViewModel.DistributionCenterId = 1;

            accountController.Register(registerViewModel);
        }

        [TestMethod]
        public void IsUserLogInTest()
        {
            loginViewModel.UserName = "RegisterTest";
            loginViewModel.Password = "ResgisterPass";
            loginViewModel.RememberMe = false;
            var returnUrl = "index";

            accountController.Login(loginViewModel, returnUrl);

        }
    }
}
