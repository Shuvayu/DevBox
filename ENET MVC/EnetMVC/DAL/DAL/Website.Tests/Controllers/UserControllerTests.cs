using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Website;
using Website.Controllers;
using DAL.Models;
using Domain;
using Website.ViewModels;
using Domain.Contracts;
using AutoMapper;

namespace Website.Tests.Controllers
{
    [TestClass]
    public class UserControllerTests
    {

        UsersController userController = new UsersController();
        public static UserViewModel userViewModel = new UserViewModel();
        UserContracts userContracts = new UserContracts();
        User user = new User();
        DistributionCenterViewModel distributionCenter = new DistributionCenterViewModel();
        Role role = new Role();
        DistributionCentersContracts dcContracts = new DistributionCentersContracts();


        [TestMethod]
        public void CreateDistributionCenterTest()
        {
            distributionCenter.DistributionCenterId = 100;
            distributionCenter.PhoneNumber = 0415990571;
            distributionCenter.Name = "New york";
            distributionCenter.IsHead = true;
            distributionCenter.Address = "Parramatta";
            userController.CreateDistributionCenter(distributionCenter);
        }

        [TestMethod]
        public void CreateUserTest()
        {
            role.RoleId = 1;
            role.RoleName = "CEO";


            userViewModel.DistributionCenterViewModel = distributionCenter;
            userViewModel.DistributionCenterId = 1;
            userViewModel.Email = "Ravi.bhavsar@hotmail.com";
            userViewModel.FullName = "Bhavsar";
            userViewModel.Password = "Test1234";
            userViewModel.Role = role;
            userViewModel.RoleId = 1;
            userViewModel.UserName = "Enet";
        

            userController.Create(userViewModel);

        }

        [TestMethod]
        public void ListOfDCsTest()
        {
            ViewResult allDcs = userController.ListOfDCs() as ViewResult;
            Assert.IsNotNull(allDcs);
        }

        [TestMethod]
        public void UpdateUserTest()
        {
            var userFirst = userContracts.GetAll().FirstOrDefault();
            var user = Enumerable.First(userContracts.GetAll(), x => x.UserId == userFirst.UserId);
            user.UserName = "UpdatedUser";
            userContracts.Update(user);

        }

        [TestMethod]
        public void GetUserWithIdTest()
        {
            var userFirst = userContracts.GetAll().FirstOrDefault();
            user = userContracts.Get(userFirst.UserId);
            Assert.IsNotNull(user);
        }

     




    }
}
